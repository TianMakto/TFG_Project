using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Distance : AI_Father
{
    [SerializeField]
    private GameObject m_weapon;

    [SerializeField]
    private float m_maxAmmo;

    [SerializeField]
    private float m_maxShootCooldown;

    [SerializeField]
    private float m_maxReloadingTime = 6;

    [SerializeField]
    private Transform m_shootingPoint;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private ParticleSystem m_reloadEffect;

    private float m_currentShootCooldown;
    private float m_currentAmmo;
    private float m_currentReloadTime;

    private bool m_reloading;

    private Transform m_weaponPositionFather;

    protected override void Start()
    {
        base.Start();
        m_weaponPositionFather = m_weapon.transform.parent;
        m_reloadEffect.Stop();
        m_currentAmmo = m_maxAmmo;
        onPlayerAlive += AttackLogic;
        onSelfAlive += Reload;
        onSelfAlive += FinishReload;
    }

    protected override void AttackLogic()
    {
        if (playerInDistance(m_attackDistance) && m_chasing)
        {
            Vector2 weaponAimDir = (Vector2)m_player.transform.position - (Vector2)transform.position;
            m_weaponPositionFather.up = weaponAimDir;

            if (m_currentShootCooldown <= 0)
            {
                if (m_currentAmmo > 0 && !m_reloading) // SHOOT
                {
                    GameObject myBullet = Instantiate(m_bulletPrefab, m_shootingPoint.position, m_shootingPoint.rotation);
                    myBullet.GetComponent<AmmoFather>().Damage = m_attackDamage;

                    m_currentAmmo--;
                    m_currentShootCooldown = m_maxShootCooldown;
                }
            }
        }
        else
        {
            m_weaponPositionFather.up = transform.up;
        }

        if(m_currentShootCooldown > 0)
        {
            m_currentShootCooldown -= Time.deltaTime;
        }
    }

    private void Reload()
    {
        if (m_currentAmmo <= 0 && !m_reloading)
        {
            m_currentReloadTime = m_maxReloadingTime;
            m_reloadEffect.Play();
            m_reloading = true;
        }
    }

    private void FinishReload()
    {
        if(m_reloading)
        {
            if(m_currentReloadTime <= 0 )
            {
                m_currentAmmo = m_maxAmmo;
                m_reloadEffect.Stop();
                m_reloading = false;
            }
            else
            {
                m_currentReloadTime -= Time.deltaTime;
            }
        }
    }

    public void FinishReloadInDeath()
    {
        m_reloadEffect.Stop();
    }
}
