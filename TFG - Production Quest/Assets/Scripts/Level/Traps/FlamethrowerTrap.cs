using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerTrap : MonoBehaviour
{
    [SerializeField]
    private float m_delay;

    [SerializeField]
    private float m_timer;

    [SerializeField]
    private float m_damage;

    [SerializeField]
    ParticleSystem m_flamethower;

    [SerializeField]
    ParticleSystem m_adviseSparks;

    private float m_currentTimer;
    //private bool m_active;

    private void Start()
    {
        m_currentTimer = m_timer;
        m_flamethower.GetComponent<ExplosionByParticles>().TotalDamage = m_damage;

        //m_active = false;
    }

    private void Update()
    {
        if (m_delay <= 0)
        {
            if (m_currentTimer > 0)
            {
                m_currentTimer -= Time.deltaTime;
            }
            else
            {
                m_flamethower.Play();
                m_adviseSparks.Play();
                m_currentTimer = m_timer;
            }
        }
        else
        {
            m_delay -= Time.deltaTime;
        }
    }
}
