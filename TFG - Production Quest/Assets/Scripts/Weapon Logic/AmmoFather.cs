using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoFather : MonoBehaviour
{
    [SerializeField]
    private float m_speed;

    [SerializeField]
    private float m_lifeSpawn = 5;

    [SerializeField]
    private GameObject m_hitParticles;

    [SerializeField]
    private bool m_belongsPlayer;

    [SerializeField]
    private LayerMask m_collisionLayers;

    [SerializeField]
    private Color originalColor;

    [System.NonSerialized]
    public float Damage;

    [Space(10)]
    [Header("Behaviours Variables")]
    [System.NonSerialized]
    public float ExtraSpeed;
    [System.NonSerialized]
    public float ExtraDamage;
    [System.NonSerialized]
    public float ExtraLifeSpawn;
    [System.NonSerialized]
    public Color DesiredColor;
    public float HitsToDestroy;

    private float m_maxHitsToDestroy;
    private Color m_originalColor;
    private FireTrailManager m_fTrail;
    private bool m_hasFT;
    public bool CanReplicate = true;

    public LayerMask CollisionLayers { get => m_collisionLayers; }
    public Color OriginalColor { get => originalColor; }
    public bool BelongsPlayer { get => m_belongsPlayer; }

    protected virtual void Start()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
        m_lifeSpawn += ExtraLifeSpawn;

        m_originalColor = GetComponent<SpriteRenderer>().color;
        transform.GetChild(0).GetComponent<TrailRenderer>().startColor = m_originalColor;

        BehaviourManager.Instance.BulletCreated(gameObject);

        m_maxHitsToDestroy = HitsToDestroy;
    }

    protected virtual void Update()
    {
        transform.position += transform.up * (m_speed + ExtraSpeed) * Time.deltaTime;

        if (m_lifeSpawn > 0)
        {
            m_lifeSpawn -= Time.deltaTime;
        }
        else
        {
            TriggerEffect(null);
        }

        if (m_hasFT)
        {
            m_fTrail.UpdateLastPoint(transform.position);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (m_collisionLayers == (m_collisionLayers | (1 << other.gameObject.layer)))
        {
            TriggerEffect(other.gameObject);
        }
    }

    bool firstHit = true;
    protected virtual void TriggerEffect(GameObject go)
    {
        BehaviourManager.Instance.BulletHitEffect(gameObject);
        Instantiate(m_hitParticles, transform.position, transform.rotation);

        if (go != null)
        {
            if (m_belongsPlayer && firstHit)
            {
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(.15f, .15f));
                firstHit = false;
            }

            if (go.GetComponent<Life>())
            {
                //BehaviourManager.Instance.DamagedEnemy(go);
                go.GetComponent<Life>().SufferDamage(Damage + ExtraDamage, DamageType.normal);
                go.GetComponent<Life>().SpawnBloddEffect(transform.position);
            }
            else if (go.GetComponent<DestructibleFather>())
            {
                go.GetComponent<DestructibleFather>().Shatter();
            }
        }

        if (m_hasFT)
        {
            m_fTrail.AddNewPoint(transform.position);
        }

        if (HitsToDestroy > 0)
        {
            HitsToDestroy--;

            if (HitsToDestroy <= 0)
            {
                BehaviourManager.Instance.BulletDestroy(gameObject);
                /*if (m_hasFT)
                {
                    m_fTrail.TrailEnabled(false);
                }*/
                Destroy(this.gameObject);
            }
            else
            {
                //Change Color Gradually to white
                float colorStep = (m_maxHitsToDestroy - HitsToDestroy) / m_maxHitsToDestroy;
                Color nextColor = Color.Lerp(m_originalColor, DesiredColor, colorStep);
                nextColor.a = 1;
                GetComponent<SpriteRenderer>().color = nextColor;
                transform.GetChild(0).GetComponent<TrailRenderer>().startColor = nextColor;

                //Bounce
                RaycastHit2D[] rhit = new RaycastHit2D[1]; //It needs to be an array by force 
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(m_collisionLayers);
                filter.useLayerMask = true;

                Vector2 direction = transform.up;
                Vector2 previousPos = transform.position; //* (-direction * 2);
                Physics2D.Raycast(previousPos, direction, filter, rhit);
                Debug.DrawRay(previousPos, direction, Color.red, Mathf.Infinity);

                //Debug.DrawLine(pos, rhit[0].point, Color.blue, Mathf.Infinity);

                Vector2 impactNormal = rhit[0].normal;
                Debug.DrawRay(rhit[0].point, rhit[0].normal, Color.yellow, Mathf.Infinity);
                Vector2 newVelocity = Vector2.Reflect(direction, impactNormal);

                transform.up = newVelocity;                
            }
        }
    }

    public void SetNewCollisions(LayerMask newPlayerCollisions, LayerMask newEnemyCollisions)
    {
        if (m_belongsPlayer)
        {
            m_collisionLayers = newPlayerCollisions;
        }
        else
        {
            m_collisionLayers = newEnemyCollisions;
        }
    }

    public void SetFireTrail(FireTrailManager fireTrail)
    {
        if (fireTrail != null)
        {
            m_hasFT = true;
        }
        else
        {
            m_hasFT = false;
        }
        m_fTrail = fireTrail;
    }
}
