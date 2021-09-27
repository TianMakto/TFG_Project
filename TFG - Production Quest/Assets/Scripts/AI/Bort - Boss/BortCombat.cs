using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BortPhase
{
    phase1,
    phase2,
    phase3,
    phase4
}


public class BortCombat : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_orbs;

    [SerializeField]
    private GameObject[] m_prefabsOrbs;

    [SerializeField]
    private GameObject[] m_shields;

    [SerializeField]
    private GameObject[] m_strongShields;

    [SerializeField]
    private float m_shieldChangeTime;

    [SerializeField]
    private ParticleSystem m_changePhaseParticles;

    [SerializeField]
    private Transform m_directionKnower;

    private float m_shieldTimer;
    private int m_shieldIndex;

    private GameObject m_shieldActive1;
    private GameObject m_shieldActive2;
    private GameObject m_strongShieldActive;
    private Life m_life;

    BortPhase m_currentPhase;

    private List<GameObject> m_orbsToAdd = new List<GameObject>();

    private void Awake()
    {        
        Camera.main.transform.parent.GetComponent<CameraFollow>().m_secondObjective = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_orbsToAdd.AddRange(m_prefabsOrbs);
        Transform orbParent = m_orbs[0].transform.parent;

        m_orbs[0].GetComponent<Orb>().Shooting = true;
        m_orbs[0].GetComponent<ParticleSystem>().Play();

        int index = 1;
        if (BehaviourManager.Instance.BehaviourSlot1 != null)
        {
            BehaviourType objective = BehaviourManager.Instance.BehaviourSlot1.BehavType;

            if(objective != BehaviourType.LifeSteal)
            {
                for (int i = 0; i < m_orbsToAdd.Count; i++)
                {
                    if (objective == m_orbsToAdd[i].GetComponent<Orb>().OrbType)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else
            {
                index = Random.Range(0, m_orbsToAdd.Count);
            }
        }
        else
        {
            index = Random.Range(0, m_orbsToAdd.Count);
        }

        GameObject myOrb = Instantiate(m_orbsToAdd[index], m_orbs[1].transform.position, m_orbs[1].transform.rotation);
        GameObject orbToReplace = m_orbs[1];
        m_orbs[1] = myOrb;
        Destroy(orbToReplace);
        m_orbs[1].transform.parent = orbParent;
        m_orbsToAdd.Remove(m_orbsToAdd[index]);

        m_orbs[1].GetComponent<Orb>().Shooting = true;
        m_orbs[1].GetComponent<ParticleSystem>().Play();

        int index2 = -1;
        if (BehaviourManager.Instance.BehaviourSlot2 != null)
        {
            BehaviourType objective = BehaviourManager.Instance.BehaviourSlot2.BehavType;

            if (objective != BehaviourType.LifeSteal)
            {
                for (int i = 0; i < m_orbsToAdd.Count; i++)
                {
                    if (objective == m_orbsToAdd[i].GetComponent<Orb>().OrbType)
                    {
                        index2 = i;
                        break;
                    }
                }
            }
        }

        for (int i = 2; i < m_orbs.Length; i++)
        {
            if (i % 2 == 1)
            {
                if(index2 != -1)
                {
                    index = index2;
                    index2 = -1;
                }
                else
                {
                    index = Random.Range(0, m_orbsToAdd.Count);
                }
                myOrb = Instantiate(m_orbsToAdd[index], m_orbs[i].transform.position, m_orbs[i].transform.rotation);
                orbToReplace = m_orbs[i];
                m_orbs[i] = myOrb;
                Destroy(orbToReplace);
                m_orbs[i].transform.parent = orbParent;
                m_orbsToAdd.Remove(m_orbsToAdd[index]);
            }

            m_orbs[i].GetComponent<SpriteRenderer>().enabled = false;
            m_orbs[i].GetComponent<Orb>().Shooting = false;
        }

        for (int i = 0; i < m_shields.Length; i++)
        {
            m_shields[i].SetActive(false);
        }

        for (int i = 0; i < m_strongShields.Length; i++)
        {
            m_strongShields[i].SetActive(false);
        }

        m_life = GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_life.isDead)
        {
            Shields();
            OrbManagement();
        }
    }

    private void Shields()
    {
        if (m_shieldTimer > 0)
        {
            m_shieldTimer -= Time.deltaTime;
        }
        else
        {
            if (m_currentPhase == BortPhase.phase2) //In phase 2, activates 1 shield
            {
                if (m_shieldActive1)
                {
                    m_shieldActive1.SetActive(false);
                }

                Vector3 playerPos = LevelManager.Instance.Player.transform.position;
                Vector3 relativePos = m_directionKnower.InverseTransformPoint(playerPos);

                if (relativePos.x > 0 && relativePos.y > 0) //Player is Up
                {
                    m_shieldActive1 = m_shields[0];
                }
                else if (relativePos.x > 0 && relativePos.y <= 0) //Player is Right
                {
                    m_shieldActive1 = m_shields[1];
                }
                else if (relativePos.x <= 0 && relativePos.y <= 0) //Player is Down
                {
                    m_shieldActive1 = m_shields[2];
                }
                else if (relativePos.x <= 0 && relativePos.y > 0) //Player is Left
                {
                    m_shieldActive1 = m_shields[3];
                }

                m_shieldActive1.SetActive(true);
            }
            else if (m_currentPhase == BortPhase.phase3 || m_currentPhase == BortPhase.phase4) //In phase 3 and 4, activates 2 shields instead of 1
            {
                if (m_shieldActive1)
                {
                    m_shieldActive1.SetActive(false);
                }
                if (m_shieldActive2)
                {
                    m_shieldActive2.SetActive(false);
                }
                if (m_strongShieldActive)
                {
                    m_strongShieldActive.SetActive(false);
                }

                Vector3 playerPos = LevelManager.Instance.Player.transform.position;
                Vector3 relativePos = m_directionKnower.InverseTransformPoint(playerPos);
                int indexUsed = 5;

                if (relativePos.x > 0 && relativePos.y > 0) //Player is Up
                {
                    m_shieldActive1 = m_shields[0];
                    m_strongShieldActive = m_strongShields[0];
                    indexUsed = 0;
                }
                else if (relativePos.x > 0 && relativePos.y <= 0) //Player is Right
                {
                    m_shieldActive1 = m_shields[1];
                    m_strongShieldActive = m_strongShields[1];
                    indexUsed = 1;
                }
                else if (relativePos.x <= 0 && relativePos.y <= 0) //Player is Down
                {
                    m_shieldActive1 = m_shields[2];
                    m_strongShieldActive = m_strongShields[2];
                    indexUsed = 2;
                }
                else if (relativePos.x <= 0 && relativePos.y > 0) //Player is Left
                {
                    m_shieldActive1 = m_shields[3];
                    m_strongShieldActive = m_strongShields[3];
                    indexUsed = 3;
                }

                m_shieldIndex = indexUsed;
                float higestNum = 0;

                for (int i = 0; i < m_shields.Length; i++)
                {
                    if (i != m_shieldIndex)
                    {
                        float newNum = Random.Range(0, 100);

                        if (newNum > higestNum)
                        {
                            m_shieldActive2 = m_shields[i];
                            higestNum = newNum;
                            indexUsed = i;
                        }
                    }
                }

                m_shieldIndex = indexUsed;
                m_shieldActive1.SetActive(true);
                m_shieldActive2.SetActive(true);
                m_strongShieldActive.SetActive(true);
            }

            if (m_currentPhase == BortPhase.phase4)
            {
                m_shieldTimer = m_shieldChangeTime * 0.7f;
            }
            else
            {
                m_shieldTimer = m_shieldChangeTime;
            }
        }
    }

    private void OrbManagement()
    {
        if (m_currentPhase == BortPhase.phase2)
        {
            m_orbs[2].GetComponent<Orb>().Shooting = true;
            m_orbs[2].GetComponent<ParticleSystem>().Play();

            m_orbs[3].GetComponent<Orb>().Shooting = true;
            m_orbs[3].GetComponent<ParticleSystem>().Play();
        }
        else if (m_currentPhase == BortPhase.phase3)
        {
            m_orbs[4].GetComponent<Orb>().Shooting = true;
            m_orbs[4].GetComponent<ParticleSystem>().Play();

            m_orbs[5].GetComponent<Orb>().Shooting = true;
            m_orbs[5].GetComponent<ParticleSystem>().Play();
        }
        else if (m_currentPhase == BortPhase.phase4)
        {
            m_orbs[6].GetComponent<Orb>().Shooting = true;
            m_orbs[6].GetComponent<ParticleSystem>().Play();

            m_orbs[7].GetComponent<Orb>().Shooting = true;
            m_orbs[7].GetComponent<ParticleSystem>().Play();
        }
    }

    public void CheckLife(float max, float current)
    {
        if (current <= (max * 0.75f) && current > (max * 0.5f) && m_currentPhase == BortPhase.phase1)
        {
            m_currentPhase = BortPhase.phase2;
            m_changePhaseParticles.Play();
        }
        else if (current <= (max * 0.5f) && current > (max * 0.25f) && m_currentPhase == BortPhase.phase2)
        {
            m_currentPhase = BortPhase.phase3;
            m_changePhaseParticles.Play();
        }
        else if (current <= (max * 0.25f) && current > (max * 0) && m_currentPhase == BortPhase.phase3)
        {
            m_currentPhase = BortPhase.phase4;
            m_changePhaseParticles.Play();
        }

        UIManager.Instance.UpdateBortLifeBar(current, max);
    }

    public void BortDies()
    {
        for (int i = 0; i < m_orbs.Length; i++)
        {
            m_orbs[i].GetComponent<SpriteRenderer>().enabled = false;
            m_orbs[i].GetComponent<Orb>().Shooting = false;
        }

        for (int i = 0; i < m_shields.Length; i++)
        {
            m_shields[i].SetActive(false);
        }

        Camera.main.transform.parent.GetComponent<CameraFollow>().m_secondObjective = null;
    }
}
