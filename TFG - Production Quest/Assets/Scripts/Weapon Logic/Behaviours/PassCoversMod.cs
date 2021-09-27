using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassCoversMod : BaseBehaviour
{
    [SerializeField]
    private LayerMask m_newPlayerCollisions;

    [SerializeField]
    private LayerMask m_newEnemyCollisions;

    private LayerMask m_oldCollisions;

    [SerializeField]
    private Color m_newColor;

    private Color m_originalColor;

    protected override void Start()
    {
        base.Start();
        m_oldCollisions = m_bullet.GetComponent<AmmoFather>().CollisionLayers;
    }

    public override void EnableBehav()
    {
        print("Pass Covers Mod enabled");
        BehaviourManager.Instance.OnCreated += BehavEffect;

        m_originalColor = m_bullet.GetComponent<SpriteRenderer>().color;
        m_bullet.GetComponent<SpriteRenderer>().color = m_newColor;
    }

    public override void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        bortBehav.OnCreated += BehavEffect;
    }

    public override void DisableBehav()
    {
        print("Pass Covers Mod disabled");
        BehaviourManager.Instance.OnCreated -= BehavEffect;

        m_bullet.GetComponent<SpriteRenderer>().color = m_originalColor;
    }

    protected override void BehavEffect(GameObject bulletCreated)
    {
        if (bulletCreated.GetComponent<AmmoFather>())
        {
            bulletCreated.GetComponent<AmmoFather>().
                SetNewCollisions(m_newPlayerCollisions, m_newEnemyCollisions);
        }
        else
        {
            bulletCreated.GetComponent<BortBullet>().
                SetNewCollisions(m_newPlayerCollisions, m_newEnemyCollisions);
        }
    }
}
