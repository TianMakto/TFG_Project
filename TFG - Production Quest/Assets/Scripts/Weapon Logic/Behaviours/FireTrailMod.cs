using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrailMod : BaseBehaviour
{
    [SerializeField]
    private GameObject m_playerFireTrail;

    [SerializeField]
    private GameObject m_enemyFireTrail;

    public override void EnableBehav()
    {
        print("FireTrail enable");
        BehaviourManager.Instance.OnCreated += BehavEffect;
    }

    public override void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        bortBehav.OnCreated += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("FireTrail disable");
        BehaviourManager.Instance.OnCreated -= BehavEffect;
    }

    protected override void BehavEffect(GameObject bulletCreated)
    {
        GameObject ft = null;
        if (bulletCreated.GetComponent<AmmoFather>())
        {
            if (bulletCreated.GetComponent<AmmoFather>().BelongsPlayer)
            {
                ft = Instantiate(m_playerFireTrail, bulletCreated.transform.position, bulletCreated.transform.rotation);
            }
            else
            {
                ft = Instantiate(m_enemyFireTrail, bulletCreated.transform.position, bulletCreated.transform.rotation);
            }

            bulletCreated.GetComponent<AmmoFather>().SetFireTrail(ft.GetComponent<FireTrailManager>());
        }
        else if((bulletCreated.GetComponent<BortBullet>()))
        {
            ft = Instantiate(m_enemyFireTrail, bulletCreated.transform.position, bulletCreated.transform.rotation);

            bulletCreated.GetComponent<BortBullet>().SetFireTrail(ft.GetComponent<FireTrailManager>());
        }        
    }
}
