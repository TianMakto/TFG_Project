using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLogic : MonoBehaviour
{
    private float m_damage;
    private float m_timer;
    private bool m_once;
    public float explosionRadious;

    private void Start()
    {
        m_damage = Mathf.Ceil(LevelManager.Instance.Player.GetComponent<PlayerCombat>().Damage / 2);
    }

    private void Update()
    {
        if (m_timer > 0)
        {
            m_timer -= Time.deltaTime;
        }
        else if (!m_once)
        {
            m_once = true;
            Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explosionRadious);

            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].GetComponent<Life>())
                {
                    if (collisions[i].GetComponent<PlayerCombat>())
                    {
                        collisions[i].GetComponent<Life>().SufferDamage(Mathf.CeilToInt(m_damage/4), DamageType.fire);
                    }
                    else
                    {
                        collisions[i].GetComponent<Life>().SufferDamage(m_damage, DamageType.fire);
                    }
                }
                else if (collisions[i].GetComponent<DestructibleFather>())
                {
                    collisions[i].GetComponent<DestructibleFather>().Shatter();
                }
            }
        }
    }
}
