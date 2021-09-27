using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AI_Father : MonoBehaviour
{
    [SerializeField]
    protected float m_speed;

    [SerializeField]
    protected float m_attackDistance;

    [SerializeField]
    protected float m_attackDamage = 1;

    [SerializeField]
    protected float m_CheckPlayerDistance;

    [SerializeField]
    protected float m_goWaypointDistance;

    [SerializeField]
    protected float m_stopDisPlayer = 1.2f;

    [SerializeField]
    protected float m_stopDisWayPoint = 0.5f;

    [SerializeField]
    protected float m_attackCooldown;

    [SerializeField]
    protected Color m_calmColor;

    [SerializeField]
    protected Color m_angryColor;

    [SerializeField]
    protected GameObject m_angrySignal;

    protected float m_angrySignalTime = 1;
    protected float m_currentAngryST;

    [SerializeField]
    protected AudioClip m_angrySound;

    [SerializeField]
    protected LayerMask m_obsatclesMask;

    [SerializeField]
    protected WaypoinType[] m_acceptedTypes;

    protected float m_currentCooldown;
    protected bool m_goingToWaypoint;
    protected bool m_patroling;
    protected bool m_chasing;

    protected GameObject m_player;
    protected Life m_pLife;
    protected Animator m_animator;
    protected Rigidbody2D m_rigBody;

    protected List<WaypointInfo> m_waypoints = new List<WaypointInfo>();
    protected WaypointInfo m_lastWaypoint;
    protected WaypointInfo waypointTarget = null;

    protected BoxCollider2D m_playerHitBox;

    protected AIDestinationSetter m_AIDestinationSetter;
    protected AIPath m_AI;

    protected Life m_life;

    [System.NonSerialized]
    public bool OnHole;

    public bool Chasing { get => m_chasing; }

    protected delegate void AiEvent();
    protected event AiEvent onPlayerAlive;
    protected event AiEvent onSelfAlive;

    protected bool playerInDistance(float distance)
    {
        if (Vector2.Distance(m_playerHitBox.bounds.center, transform.position) < distance)
            return true;
        else
            return false;
    }

    protected virtual void Start()
    {
        m_life = GetComponent<Life>();
        m_AI = GetComponent<AIPath>();
        m_rigBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_player = LevelManager.Instance.Player;
        m_pLife = m_player.GetComponent<Life>();
        m_playerHitBox = LevelManager.Instance.PlayerHitBox;
        m_AIDestinationSetter = GetComponent<AIDestinationSetter>();

        GetComponent<SpriteRenderer>().color = m_calmColor;
        m_angrySignal.SetActive(false);

        m_patroling = true;

        WaypointInfo[] waypoints = FindObjectsOfType<WaypointInfo>();
        for (int i = 0; i < waypoints.Length; i++)
        {
            for (int j = 0; j < m_acceptedTypes.Length; j++)
            {
                if (m_acceptedTypes[j] == waypoints[i].m_type)
                {
                    m_waypoints.Add(waypoints[i]);
                    break;
                }
            }
        }
        m_AI.maxSpeed = m_speed;
    }

    protected virtual void Update()
    {
        if (!m_life.isDead)
        {
            if (m_patroling)
            {
                Patrol();
            }

            if (!m_pLife.isDead && !OnHole)
            {
                Chase();
                if (onPlayerAlive != null)
                {
                    onPlayerAlive();
                }

                if (m_player.transform.position.x <= transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }

            if(onSelfAlive != null)
            {
                onSelfAlive();
            }

            m_animator.SetFloat("Speed", m_AI.velocity.magnitude);
        }

        if(m_currentAngryST > 0)
        {
            m_currentAngryST -= Time.deltaTime;
            if(m_currentAngryST <= 0)
            {
                m_angrySignal.SetActive(false);
            }
        }
    }

    protected virtual void AttackLogic()
    {
        
    }

    protected virtual void Patrol()
    {
        if (!m_goingToWaypoint)
        {
            float newDis = m_goWaypointDistance;
            WaypointInfo newTarget = null;

            for (int i = 0; i < m_waypoints.Count; i++)
            {
                if (!m_waypoints[i].Catched && m_waypoints[i] != m_lastWaypoint)
                {
                    float currentDis = Vector2.Distance(transform.position, m_waypoints[i].transform.position);
                    if (currentDis < newDis)
                    {
                        newDis = currentDis;
                        newTarget = m_waypoints[i];
                    }
                }
            }

            if (newTarget != null)
            {
                if (waypointTarget)
                {
                    m_lastWaypoint = waypointTarget;
                    m_lastWaypoint.Catched = false;
                }

                waypointTarget = newTarget;
                waypointTarget.Catched = true;

                m_goingToWaypoint = true;
                m_AIDestinationSetter.target = waypointTarget.transform;
            }
        }
        else
        {
            if (Vector2.Distance(waypointTarget.transform.position, transform.position) <= m_stopDisWayPoint)
            {
                m_goingToWaypoint = false;
            }
        }
    }

    bool once = false;

    protected virtual void Chase()
    {
        if (playerInDistance(m_CheckPlayerDistance) && GetComponent<SpriteRenderer>().isVisible)
        {

            if (!m_chasing)
            {
                Debug.DrawRay(transform.position, m_playerHitBox.bounds.center - transform.position, Color.green, Time.deltaTime);
                RaycastHit2D rhit = Physics2D.Raycast(transform.position, m_playerHitBox.bounds.center - transform.position, m_CheckPlayerDistance, m_obsatclesMask);

                if (rhit.transform.gameObject == m_player)
                {
                    m_AIDestinationSetter.target = m_player.transform;

                    m_patroling = false;
                    m_AI.endReachedDistance = m_stopDisPlayer;
                    m_chasing = true;
                    CheckChasingState();
                }
            }
            else
            {
                if (playerInDistance(m_attackDistance) && !once) //once the enemy has reach, dont moves closer to the player
                {
                    //m_AI.canMove = false;
                    m_AIDestinationSetter.target = transform;
                    once = true;
                }
                else if (once)
                {
                    //m_AI.canMove = true;
                    m_AIDestinationSetter.target = m_player.transform;
                    once = false;
                }
            }
        }
        else if (m_chasing)
        {
            m_patroling = true;
            //m_AI.endReachedDistance = m_stopDisWayPoint;

            m_chasing = false;
            CheckChasingState();
        }
    }

    protected void CheckChasingState()
    {
        switch (m_chasing)
        {
            case true:
                GetComponent<SpriteRenderer>().color = m_angryColor;
                if (m_angrySignal)
                {
                    m_angrySignal.SetActive(true);

                    if (GetComponent<TimmyCallFriends>())
                    {
                        GetComponent<TimmyCallFriends>().CallForFriends();
                    }

                    m_currentAngryST = m_angrySignalTime;
                }
                if (m_angrySound)
                {
                    EffectsAudioManager.Instance.AudioOneshot(m_angrySound);
                }
                break;
            case false:
                GetComponent<SpriteRenderer>().color = m_calmColor;
                break;
        }
    }

    public void TimmyAdvisesTimmy()
    {
        m_AIDestinationSetter.target = m_player.transform;

        m_patroling = false;
        m_AI.endReachedDistance = m_stopDisPlayer;
        m_chasing = true;
        CheckChasingState();
    }
}
