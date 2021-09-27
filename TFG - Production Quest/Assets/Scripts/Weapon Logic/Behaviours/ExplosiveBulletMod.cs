using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletMod : BaseBehaviour
{
    [SerializeField]
    GameObject m_explosion;

    [SerializeField]
    private float m_Radious = 4;

    public override void EnableBehav()
    {
        print("ExplosiveBullet enable");
        BehaviourManager.Instance.OnHitEffect += BehavEffect;
    }

    public override void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        bortBehav.OnHitEffect += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("ExplosiveBullet disable");
        BehaviourManager.Instance.OnHitEffect -= BehavEffect;
    }

    protected override void BehavEffect(GameObject bullet)
    {
        GameObject explosion = Instantiate(m_explosion, bullet.transform.position, bullet.transform.rotation);
        explosion.GetComponent<ExplosionLogic>().explosionRadious = m_Radious;
    }
}
