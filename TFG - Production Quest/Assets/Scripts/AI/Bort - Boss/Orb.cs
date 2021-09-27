using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField]
    private float m_bulletDamage = 3;

    [SerializeField]
    private float m_maxShootCooldown;

    [SerializeField]
    private float m_maxReloadingTime = 3;

    [SerializeField]
    private GameObject m_bulletPrefab;

    [SerializeField]
    private BehaviourType m_orbType = BehaviourType.NotAMod;

    [SerializeField]
    private ParticleSystem m_reloadEffect;

    private bool m_shooting;
    private float m_currentShootCooldown;

    private GameObject m_bort;
    private GameObject m_player;

    public bool Shooting
    {
        get => m_shooting;
        set 
        {
            transform.GetChild(0).gameObject.SetActive(value);
            m_shooting = value;
        }
    }

    public BehaviourType OrbType { get => m_orbType; }

    private void Start()
    {
        m_bort = transform.parent.gameObject;
        m_currentShootCooldown = 1 + Random.Range(0f, 1f);
        m_player = LevelManager.Instance.Player;
    }

    private void Update()
    {
        transform.RotateAround(m_bort.transform.position, Vector3.forward, 72 * Time.deltaTime);

        if (m_shooting)
        {
            Shoot();
        }

        if(m_currentShootCooldown > 0)
        {
            m_currentShootCooldown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        if(m_currentShootCooldown <= 0)
        {
            GameObject bullet = Instantiate(m_bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BortBullet>().Damage = m_bulletDamage;
            Vector2 bulletAimDir = (Vector2)m_player.transform.position - (Vector2)transform.position;
            bullet.transform.up = bulletAimDir;

            m_currentShootCooldown = m_maxShootCooldown;
        }
    }
}
