using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum CurrentDevice
{
    XboxController,
    PsController,
    KeyboardAndMouse
}

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject m_weapon;

    [SerializeField]
    private Sprite m_spriteWeapon;

    [SerializeField]
    private float m_damage;

    [SerializeField]
    private float m_maxAmmo;

    [SerializeField]
    private float m_maxReloadingTime;

    [SerializeField]
    private float m_heatGain = 6.5f;

    [SerializeField]
    private float m_maxShootCooldown;

    [SerializeField]
    private float m_gamepadAimSpeed;

    [SerializeField]
    private Sprite m_gamepadAimSprite;

    [SerializeField]
    private Transform m_shootingPoint;

    [SerializeField]
    private GameObject m_bulletPrefab;
    
    [SerializeField]
    private LineRenderer m_laserAim;

    [SerializeField]
    private ParticleSystem m_reloadEffect;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip m_shootSound;

    private AmmoFather m_currentAmmoType;
    private float m_currentShootCooldown;
    private float m_currentAmmo;
    private float m_currentReloadTime;
    private float m_overHeat;
    private float m_overHeatDrop;

    [System.NonSerialized]
    public static float staticExtraDamage;

    [System.NonSerialized]
    public static Sprite staticWeaponSprite;

    private bool m_reloading;
    private bool m_overHeatMode;

    private CurrentDevice m_myDevice;

    private Transform m_weaponPositionFather;
    private GameObject m_AimGO;

    private Life m_life;
    private SpriteRenderer m_sRenderer;
    private UIManager ui;
    private InputMaster m_inputs;
    private Vector2 m_centerScreen;
    private Vector2 m_CursorGamepadPos;

    public GameObject BulletPrefab { get => m_bulletPrefab; }
    public Transform ShootingPoint { get => m_shootingPoint; }
    public float Damage { get => m_damage; }
    public InputMaster Inputs { get => m_inputs; }
    public float OverHeat { get => m_overHeat; }
    public float CurrentAmmo { get => m_currentAmmo; }
    public float MaxAmmo { get => m_maxAmmo; }
    public bool OverHeatMode { get => m_overHeatMode; set => m_overHeatMode = value; }
    public CurrentDevice MyDevice { get => m_myDevice; }
    public float ExtraDamage { get => staticExtraDamage; set => staticExtraDamage = value; }

    private void Awake()
    {
        m_inputs = new InputMaster();
    }

    void Start()
    {
        m_life = GetComponent<Life>();
        m_sRenderer = GetComponent<SpriteRenderer>();
        ui = UIManager.Instance;
        m_centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        m_weaponPositionFather = m_weapon.transform.parent;

        if (staticWeaponSprite == null)
        {
            staticWeaponSprite = m_spriteWeapon;
        }

        m_weapon.GetComponent<SpriteRenderer>().sprite = staticWeaponSprite;
        m_reloadEffect.Stop();

        m_currentAmmoType = m_bulletPrefab.GetComponent<AmmoFather>();
        m_currentAmmo = m_maxAmmo;
        ui.UpdateAmmo(m_currentAmmo, m_maxAmmo);
        m_CursorGamepadPos = new Vector2(Screen.width / 2, Screen.height / 2);

        InputSystem.onActionChange += (obj, change) =>
        {

            if (change == InputActionChange.ActionPerformed)
            {
                var inputAction = (InputAction)obj;
                var lastControl = inputAction.activeControl;
                var lastDevice = lastControl.device;

                if (lastDevice.displayName == "Mouse" || lastDevice.displayName == "Keyboard")
                {
                    m_myDevice = CurrentDevice.KeyboardAndMouse;
                }
                else if (lastDevice.displayName == "Xbox Controller")
                {
                    m_myDevice = CurrentDevice.XboxController;
                    print("Playing with XboxController");
                }
                else if (lastDevice.displayName == "Wireless Controller")
                {
                    m_myDevice = CurrentDevice.PsController;
                    print("Playing with Dualshock Controller");
                }
            }
        };
    }

    private void InstantiateGamepadCursor()
    {
        m_AimGO = new GameObject();
        m_AimGO.name = "Aim GameObject";
        m_AimGO.transform.position = transform.position;
        m_AimGO.AddComponent<SpriteRenderer>();
        m_AimGO.GetComponent<SpriteRenderer>().sprite = m_gamepadAimSprite;
        m_AimGO.GetComponent<SpriteRenderer>().sortingOrder = 20;
    }

    void Update()
    {
        if (!m_life.isDead && !ui.OnMenus)
        {
            FireWeapon();
            StartReload();
            UpdateLaser();

            if (m_currentReloadTime > 0)
            {
                m_currentReloadTime -= Time.deltaTime;

                if(m_currentReloadTime <= 0)
                {
                    FinishReload();
                }
            }

            if (m_overHeat > 0)
            {
                m_overHeatDrop += Time.deltaTime / 1.5f;
                if (m_overHeatDrop > 0.5f)
                {
                    m_overHeat -= Time.deltaTime * m_overHeatDrop;
                    m_sRenderer.color = Color.Lerp(Color.white, Color.red, m_overHeat / 100);

                    if (m_overHeatMode)
                    {
                        ui.UpdateOverHeat(Mathf.Ceil(m_overHeat));
                    }
                }
            }
        }

        if (m_myDevice != CurrentDevice.KeyboardAndMouse)
        {            
            m_CursorGamepadPos += m_inputs.Player.GamepadAim.ReadValue<Vector2>() * Time.deltaTime * m_gamepadAimSpeed;

            if(m_inputs.Player.GamepadAim.ReadValue<Vector2>() != Vector2.zero)
            {
                m_AimGO.transform.position += (Vector3)m_inputs.Player.GamepadAim.ReadValue<Vector2>() * Time.deltaTime * m_gamepadAimSpeed;
            }
        }
    }

    private void FireWeapon()
    {
        Vector2 weaponAimDir = ((Vector2)Mouse.current.position.ReadValue() - (Vector2)(Camera.main.WorldToScreenPoint(transform.position))).normalized;
        m_weaponPositionFather.up = weaponAimDir;
        if (m_currentShootCooldown <= 0)
        {
            if (m_inputs.Player.Shoot.ReadValue<float>() > 0 && !m_reloading)
            {
                if (m_currentAmmo > 0 && m_overHeat < 100) // SHOOT
                {
                    GameObject myBullet = Instantiate(m_bulletPrefab, m_shootingPoint.position, m_shootingPoint.rotation);
                    //BehaviourManager.Instance.BulletCreated(myBullet);
                    myBullet.GetComponent<AmmoFather>().Damage = m_damage + staticExtraDamage;
                    if (!m_overHeatMode)
                    {
                        m_currentAmmo--;
                        m_currentShootCooldown = m_maxShootCooldown; //Ponemos un enfriamiento igual al maximo tiempo
                        ui.UpdateAmmo(m_currentAmmo, m_maxAmmo);
                    }
                    else
                    {
                        m_currentShootCooldown = m_maxShootCooldown/3; //Ponemos un enfriamiento igual a 1/3 del maximo
                        m_overHeat += Time.deltaTime * m_heatGain;
                        m_sRenderer.color = Color.Lerp(Color.white, Color.red, m_overHeat / 100);
                        ui.UpdateOverHeat(Mathf.Floor(m_overHeat));
                    }

                    m_overHeatDrop = 0;
                    EffectsAudioManager.Instance.AudioOneshot(m_shootSound);
                    StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.04f, .04f));
                }
                else
                {
                    m_currentReloadTime = m_maxReloadingTime;
                    m_reloadEffect.Play();
                    m_reloading = true;
                }
            }
        }
        else
        {
            m_currentShootCooldown -= Time.deltaTime;            
        }
    }

    private void StartReload()
    {
        if (m_inputs.Player.Reload.triggered)
        {
            m_currentReloadTime = m_maxReloadingTime;
            m_reloadEffect.Play();
            m_reloading = true;
        }
    }

    private void FinishReload()
    {
        m_currentAmmo = m_maxAmmo;
        ui.UpdateAmmo(m_currentAmmo, m_maxAmmo);
        m_reloadEffect.Stop();
        m_reloading = false;
    }

    private void UpdateLaser()
    {
        RaycastHit2D[] rhit = new RaycastHit2D[10];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(m_bulletPrefab.GetComponent<AmmoFather>().CollisionLayers);
        filter.useLayerMask = true;
        Physics2D.Raycast(m_shootingPoint.position, m_shootingPoint.up, filter, rhit);
        //Physics2D.BoxCast(m_shootingPoint.position, m_bulletPrefab.transform.lossyScale, ShootingPoint.rotation.z, m_shootingPoint.up, filter, rhit);

        m_laserAim.SetPosition(0, m_shootingPoint.position);

        if(rhit[0].point != Vector2.zero)
        {
            m_laserAim.SetPosition(1, rhit[0].point);
        }
        else
        {
            Vector3 newPos = m_shootingPoint.transform.position + m_shootingPoint.transform.up * 10;
            m_laserAim.SetPosition(1, newPos);
        }
    }

    public void TakeAmmo(GameObject bulleType, float ammoCount) // Probably will erase later
    {
        if (bulleType != m_bulletPrefab)
        {
            m_currentAmmo = ammoCount;
            m_bulletPrefab = bulleType;
            m_currentAmmoType = bulleType.GetComponent<AmmoFather>();
        }
        else
        {
            m_currentAmmo += ammoCount;
        }

        ui.UpdateAmmo(m_currentAmmo, m_maxAmmo);
    }

    // Necessary for the correct work  of InputMaster
    private void OnEnable()
    {
        m_inputs.Enable();
    }

    private void OnDisable()
    {
        m_inputs.Disable();
    }

    public void UpdateWand(Sprite newWand)
    {
        staticWeaponSprite = newWand;
        m_weapon.GetComponent<SpriteRenderer>().sprite = staticWeaponSprite;
    }
}
