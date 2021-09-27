using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum DamageType
{
    normal,
    fire,
    poison
}

public class Life : MonoBehaviour
{
    [SerializeField]
    private float[] MaxLife;

    private float m_maxLife;

    [SerializeField]
    private GameObject m_damageText;

    [SerializeField]
    private float m_maxPoisonTime = 2;

    [SerializeField]
    private float m_poisonTick = 0.5f;

    [SerializeField]
    private ParticleSystem m_blood;

    [SerializeField]
    private AudioClip m_hurtSound;

    [Space(10)]
    [Header("Enemies colliders")]
    [SerializeField]
    private Collider2D m_worldCollider;

    [SerializeField]
    private Collider2D m_holeCollider;

    [SerializeField]
    private GameObject m_kennyLight;

    [Space(10)]
    [Header("Player colliders")]
    [SerializeField]
    GameObject m_deathLight;

    [System.NonSerialized]
    public static float extraLife;

    private float olderExtraLife = 0;

    private float m_currentLife;
    private float m_currentPoisonTime;
    private float m_poisonedTime;
    private float m_poisonDamage;

    private Color m_poisonousColor;

    private bool m_isDead;
    private UIManager ui;

    #region Counters
    private float m_redDelay;
    #endregion

    public bool isDead { get => m_isDead; }

    private void Start()
    {
        ui = UIManager.Instance;
        if (GetComponent<PlayerCombat>())
        {
            UpdateMaxLife();
            m_deathLight.SetActive(false);

            m_currentLife = m_maxLife;
            ui.UpdateLifeBar(m_currentLife, m_maxLife);
        }
        else
        {
            m_maxLife = MaxLife[0];
            m_currentLife = m_maxLife;
        }
    }

    private void Update()
    {
        Counters();
    }

    public void SufferDamage(float damage, DamageType dType)
    {
        if (!m_isDead && GetComponent<SpriteRenderer>().isVisible)
        {
            m_currentLife -= damage;

            if (tag == "Player")
            {
                ui.UpdateLifeBar(m_currentLife, m_maxLife);
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.15f, .15f));
            }
            else
            {
                BehaviourManager.Instance.DamagedEnemy(gameObject);
            }

            if (GetComponent<BortCombat>())
            {
                GetComponent<BortCombat>().CheckLife(m_maxLife, m_currentLife);
            }

            SpawnDamageText(damage, dType);

            if (m_currentLife <= 0)
            {
                m_currentLife = 0;
                m_isDead = true;
                if (tag == "Enemies")
                {
                    GetComponent<Animator>().SetBool("Death", true);
                    GetComponent<SpriteRenderer>().color = new Color(0.3f, 0.3f, 0.3f, 1);
                    Destroy(m_worldCollider);
                    Destroy(m_holeCollider);
                    Destroy(GetComponent<DynamicGridObstacle>());
                    Destroy(GetComponent<Collider2D>());
                    Destroy(m_kennyLight);

                    if (GetComponent<BortCombat>())
                    {
                        GetComponent<BortCombat>().BortDies();
                    }

                    transform.localScale *= 0.75f;

                    if (GetComponent<AI_Distance>())
                    {
                        GetComponent<AI_Distance>().FinishReloadInDeath();
                    }

                    GetComponent<AIPath>().isStopped = true;
                }
                else
                {
                    m_deathLight.SetActive(true);
                    UIManager.Instance.ShowDeathMenu(true);
                    GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 0.4f, 1);
                    GetComponent<Animator>().SetBool("Death", true);
                    transform.GetChild(0).gameObject.SetActive(false); //This should cacth Weapon Father
                    transform.GetChild(1).gameObject.SetActive(false); //This should cacth Laser Aim
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.red;
                m_redDelay = 0.15f;
            }

            if (m_hurtSound)
            {
                EffectsAudioManager.Instance.AudioOneshot(m_hurtSound);
            }
        }
    }

    private void SpawnDamageText(float damage, DamageType dType)
    {
        float randomizer = 0.3f;
        float randomX = Random.Range(transform.position.x - randomizer, transform.position.x + randomizer);
        float randomY = Random.Range(transform.position.y - randomizer / 2, transform.position.y + randomizer / 2);
        Vector3 posRandom = new Vector3(randomX, randomY, transform.position.z);
        DamageText bro = Instantiate(m_damageText, posRandom, Quaternion.identity).GetComponent<DamageText>();
        bro.m_damageAmount = damage;

        if (dType == DamageType.fire)
        {
            bro.FireText();
        }
        else if (dType == DamageType.poison)
        {
            bro.PoisonText();
        }
    }

    private void SpawnHealText(float healAmount)
    {
        float randomizer = 0.3f;
        float randomX = Random.Range(transform.position.x - randomizer, transform.position.x + randomizer);
        float randomY = Random.Range(transform.position.y - randomizer / 2, transform.position.y + randomizer / 2);
        Vector3 posRandom = new Vector3(randomX, randomY, transform.position.z);
        DamageText bro = Instantiate(m_damageText, posRandom, Quaternion.identity).GetComponent<DamageText>();
        bro.m_damageAmount = healAmount;
        bro.HealText();
    }

    public void SpawnBloddEffect(Vector3 bulletImpact)
    {
        Vector3 dir = (transform.position - bulletImpact).normalized;
        m_blood.transform.up = dir;
        m_blood.Play();
    }

    private void Counters()
    {
        if (m_redDelay > 0)
        {
            m_redDelay -= Time.deltaTime;
            if (m_redDelay <= 0 && !m_isDead)
            {
                if (m_currentPoisonTime > 0)
                {
                    GetComponent<SpriteRenderer>().color = m_poisonousColor;
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

        if (m_currentPoisonTime > 0)
        {
            m_currentPoisonTime -= Time.deltaTime;
            m_poisonedTime += Time.deltaTime;

            if (m_poisonedTime >= (m_poisonTick * 0.95f))
            {
                m_poisonedTime = 0;
                SufferDamage(m_poisonDamage, DamageType.poison);
            }

            if (m_currentPoisonTime <= 0 && !m_isDead)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    public void Poison(float poisonStrenght, Color poisonousColor)
    {
        m_poisonDamage = poisonStrenght;
        m_currentPoisonTime = m_maxPoisonTime;
        m_poisonousColor = poisonousColor;
        GetComponent<SpriteRenderer>().color = poisonousColor;
    }

    public void Heal(float healAmount)
    {
        m_currentLife += healAmount;
        if (m_currentLife > m_maxLife)
        {
            m_currentLife = m_maxLife;
        }

        SpawnHealText(healAmount);
        if (tag == "Player")
        {
            ui.UpdateLifeBar(m_currentLife, m_maxLife);
        }
    }

    public void UpgradeLife(float upgrade)
    {
        olderExtraLife = extraLife;
        extraLife = upgrade;
        UpdateMaxLife();
    }

    private void UpdateMaxLife()
    {     
        if (LevelManager.CurrentDifficulty == Difficulty.Easy)
        {
            m_maxLife = MaxLife[0] + extraLife;
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.Normal)
        {
            m_maxLife = MaxLife[1] + extraLife;
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.Hard)
        {
            m_maxLife = MaxLife[2] + extraLife;
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.VeryHard)
        {
            m_maxLife = MaxLife[3] + extraLife;
        }

        m_currentLife += extraLife - olderExtraLife;

        ui.UpdateLifeBar(m_currentLife, m_maxLife);
    }

    public float GetBaseMaxLife()
    {
        float maxLife = 0;

        if (LevelManager.CurrentDifficulty == Difficulty.Easy)
        {
            maxLife = MaxLife[0];
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.Normal)
        {
            maxLife = MaxLife[1];
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.Hard)
        {
            maxLife = MaxLife[2];
        }
        else if (LevelManager.CurrentDifficulty == Difficulty.VeryHard)
        {
            maxLife = MaxLife[3];
        }

        return maxLife;
    }
}
