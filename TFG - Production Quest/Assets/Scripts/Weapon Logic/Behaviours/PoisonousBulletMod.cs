using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonousBulletMod : BaseBehaviour
{
    [SerializeField]
    private float m_poisonDamage;

    [SerializeField]
    private Color m_poisonousColor;

    public override void EnableBehav()
    {
        print("PoisonousBullet enable");
        BehaviourManager.Instance.OnEnemyDamaged += BehavEffect;

        m_bullet.GetComponent<SpriteRenderer>().color = m_poisonousColor;
    }

    public override void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        bortBehav.OnPlayerDamaged += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("PoisonousBullet disable");
        BehaviourManager.Instance.OnEnemyDamaged -= BehavEffect;

        m_bullet.GetComponent<SpriteRenderer>().color = m_bullet.GetComponent<AmmoFather>().OriginalColor;
    }

    protected override void BehavEffect(GameObject enemy)
    {
        enemy.GetComponent<Life>().Poison(m_poisonDamage, m_poisonousColor);
    }
}
