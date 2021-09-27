using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBehaviourToWeapon : InteractableFather
{
    private BaseBehaviour behaviour;

    [SerializeField]
    private BehaviourType m_behavToSearch;

    [SerializeField]
    private Sprite m_bookUsedSprite;

    private bool used;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < LevelManager.Instance.BehavioursTypes.Count; i++)
        {
            if (m_behavToSearch == LevelManager.Instance.BehavioursTypes[i].BehavType)
            {
                behaviour = LevelManager.Instance.BehavioursTypes[i];
                break;
            }
        }

        GetComponent<SpriteRenderer>().sprite = behaviour.BookSprite;
    }

    public override void Interact(PlayerInteract player)
    {
        if (!used)
        {
            UIManager.Instance.ShowExchangeMods(true, behaviour, this);
        }
    }

    public void Used()
    {
        GetComponent<SpriteRenderer>().sprite = m_bookUsedSprite;
        base.Interact(null);
        used = true;
    }

    public override void NearInteract(PlayerInteract player)
    {
        if (!used)
        {
            base.NearInteract(player);
        }
    }

    public override void NotLongerNear(PlayerInteract player)
    {
        if (!used)
        {
            base.NotLongerNear(player);
        }
    }
}
