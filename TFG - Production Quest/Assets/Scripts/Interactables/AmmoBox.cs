using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : InteractableFather
{
    [SerializeField]
    private AmmoFather m_bulletType;

    [SerializeField]
    private float m_ammoCount;

    public override void Interact(PlayerInteract player)
    {
        player.GetComponent<PlayerCombat>().TakeAmmo(m_bulletType.gameObject, m_ammoCount);

        base.Interact(player);
        Destroy(this);
    }
}
