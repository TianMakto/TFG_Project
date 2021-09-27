using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageOverTime : MonoBehaviour
{
    [SerializeField]
    private float m_timeBtwDamage;

    [SerializeField]
    private float m_damage;

    private float m_timer;
    private ParticleSystem m_ps;

    private void Start()
    {
        print(0);
        m_timer = 0;
        m_ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!m_ps.isPlaying)
        {
            Destroy(gameObject);
        }

        if (m_timer > 0)
        {
            m_timer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.GetComponent<Life>() && m_timer <= 0)
        {
            collision.GetComponent<Life>().SufferDamage(m_damage, DamageType.fire);
            m_timer = m_timeBtwDamage;
        }
    }
}
