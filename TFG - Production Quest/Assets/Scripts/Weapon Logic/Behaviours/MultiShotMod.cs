using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotMod : BaseBehaviour
{
    private float m_bulletsToInstantiate = 2;

    [SerializeField]
    private float m_angle = 60;

    public override void EnableBehav()
    {
        print("Multishot enabled");
        BehaviourManager.Instance.OnCreated += BehavEffect;
        //LevelManager.Instance.Bullet.GetComponent<AmmoFather>()
    }

    public override void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        bortBehav.OnCreated += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("Multishot disabled");
        BehaviourManager.Instance.OnCreated -= BehavEffect;
    }

    protected override void BehavEffect(GameObject bulletCreated)
    {
        bool canReplicate = false;
        float damage = 0;

        if (bulletCreated.GetComponent<AmmoFather>())
        {
            canReplicate = bulletCreated.GetComponent<AmmoFather>().CanReplicate;
            damage = Mathf.Ceil(bulletCreated.GetComponent<AmmoFather>().Damage / 2);
        }
        else if (bulletCreated.GetComponent<BortBullet>())
        {
            canReplicate = bulletCreated.GetComponent<BortBullet>().CanReplicate;
            damage = Mathf.Ceil(bulletCreated.GetComponent<BortBullet>().Damage / 2);
        }

        if (canReplicate)
        {
            //Transform shootingPointPos = m_pCombat.ShootingPoint;
            //float damage = Mathf.Ceil(m_pCombat.Damage / 2);

            GameObject bullet1 = Instantiate(bulletCreated, bulletCreated.transform.position, bulletCreated.transform.rotation);
            bullet1.transform.RotateAround(bullet1.transform.position, bulletCreated.transform.forward, m_angle / 2);

            GameObject bullet2 = Instantiate(bulletCreated, bulletCreated.transform.position, bulletCreated.transform.rotation);
            bullet2.transform.RotateAround(bullet1.transform.position, bulletCreated.transform.forward, -m_angle / 2);

            if (bulletCreated.GetComponent<AmmoFather>())
            {
                bullet1.GetComponent<AmmoFather>().CanReplicate = false;
                bullet1.GetComponent<AmmoFather>().Damage = damage;
                bullet1.GetComponent<SpriteRenderer>().color = Color.yellow;

                bullet2.GetComponent<AmmoFather>().CanReplicate = false;
                bullet2.GetComponent<AmmoFather>().Damage = damage;
                bullet2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (bulletCreated.GetComponent<BortBullet>())
            {
                bullet1.GetComponent<BortBullet>().CanReplicate = false;
                bullet1.GetComponent<BortBullet>().Damage = damage;

                bullet2.GetComponent<BortBullet>().CanReplicate = false;
                bullet2.GetComponent<BortBullet>().Damage = damage;
            }
        }
    }
}
