using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BortBulletBehaviuour : MonoBehaviour
{
    BaseBehaviour m_mod;

    public delegate void BortBehavAction(GameObject bullet);
    public event BortBehavAction OnHitEffect;
    public event BortBehavAction OnPlayerDamaged;
    public event BortBehavAction OnDestroy;
    public event BortBehavAction OnCreated;

    private void Awake()
    {
        if (GetComponent<BaseBehaviour>())
        {
            m_mod = GetComponent<BaseBehaviour>();
            m_mod.BortUsesMod(this);
        }
    }

    public void BulletCreated(GameObject bulletCreated)
    {
        if (OnCreated != null)
        {
            OnCreated?.Invoke(bulletCreated);
        }
    }

    public void BulletHitEffect(GameObject bullet)
    {
        if (OnHitEffect != null)
        {
            OnHitEffect?.Invoke(bullet);
        }
    }

    public void DamagedPlayer(GameObject player)
    {
        if (OnPlayerDamaged != null)
        {
            OnPlayerDamaged?.Invoke(player);
        }
    }

    public void BulletDestroy(GameObject bullet)
    {
        if (OnDestroy != null)
        {
            OnDestroy?.Invoke(bullet);
        }
    }
}
