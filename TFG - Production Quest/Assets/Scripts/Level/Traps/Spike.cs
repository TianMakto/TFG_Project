using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private float m_delay;

    [SerializeField]
    private float m_timer;

    [SerializeField]
    private float m_damage;

    Collider2D m_collider;
    Animator m_animator;
    private float m_currentTimer;
    private bool m_advised;

    private void Start()
    {
        m_collider = GetComponent<Collider2D>();
        m_animator = GetComponent<Animator>();
        m_collider.enabled = false;
        m_currentTimer = m_timer;
    }

    private void Update()
    {
        if (m_delay <= 0)
        {
            if (m_currentTimer > 0)
            {
                m_currentTimer -= Time.deltaTime;

                if(m_currentTimer < 1 && !m_advised)
                {
                    m_animator.SetTrigger("Advise");
                    m_advised = true;
                }
            }
            else
            {
                m_animator.SetTrigger("Activate");
                //m_currentTimer = m_timer;
            }
        }
        else
        {
            m_delay -= Time.deltaTime;
        }
    }

    public void ColliderON()
    {
        m_collider.enabled = true;
    }

    public void ColliderOff()
    {
        m_collider.enabled = false;
        m_currentTimer = m_timer;
        m_animator.ResetTrigger("Activate");
        m_animator.ResetTrigger("Advise");
        m_advised = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Life>())
        {
            other.GetComponent<Life>().SufferDamage(m_damage, DamageType.normal);
        }
    }
}
