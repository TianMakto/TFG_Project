using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealMod : BaseBehaviour
{
    public override void EnableBehav()
    {
        print("LifeSteal enable");
        BehaviourManager.Instance.OnEnemyDamaged += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("LifeSteal disable");
        BehaviourManager.Instance.OnEnemyDamaged -= BehavEffect;
    }

    protected override void BehavEffect(GameObject enemy)
    {
        m_player.GetComponent<Life>().Heal(Mathf.Ceil(m_pCombat.Damage/8));
    }
}
