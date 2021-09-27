using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBounceMod : BaseBehaviour
{
    [SerializeField]
    private float m_bounceTimes;

    public override void EnableBehav()
    {
        print("BulletBounce enable");
        m_bullet.GetComponent<AmmoFather>().HitsToDestroy = m_bounceTimes;
    }

    public override void DisableBehav()
    {
        print("BulletBounce disable");
        m_bullet.GetComponent<AmmoFather>().HitsToDestroy = 1;
    }
}
