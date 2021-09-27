using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    Bounce,
    Explosive,
    Multi,
    Poison,
    LifeSteal,
    Overheat,
    NotAMod,
    PassCovers,
    FireTrail
}

public class BaseBehaviour : MonoBehaviour
{
    protected GameObject m_bullet;
    protected GameObject m_player;
    protected PlayerCombat m_pCombat;

    [SerializeField]
    private BehaviourType m_behavType;

    [SerializeField]
    private Sprite m_bookSprite;

    [SerializeField]
    private bool m_conventionalMod = true;

    public BehaviourType BehavType { get => m_behavType; }
    public Sprite BookSprite { get => m_bookSprite; }

    public virtual void DisableBehav()
    {
        
    }

    public virtual void EnableBehav()
    {

    }

    protected virtual void Awake()
    {
        if (m_conventionalMod)
        {
            LevelManager.Instance.BehavioursTypes.Add(this);
        }
    }

    protected virtual void Start()
    {
        if (m_conventionalMod)
        {
            m_bullet = LevelManager.Instance.Bullet;
            m_player = LevelManager.Instance.Player;
            m_pCombat = m_player.GetComponent<PlayerCombat>();
        }
    }

    protected virtual void BehavEffect(GameObject bullet)
    {

    }

    public virtual void BortUsesMod(BortBulletBehaviuour bortBehav)
    {
        
    }
}
