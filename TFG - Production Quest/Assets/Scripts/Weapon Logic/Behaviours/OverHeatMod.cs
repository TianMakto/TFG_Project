using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHeatMod : BaseBehaviour
{
    public override void EnableBehav()
    {
        print("OverHeat enable");
        m_pCombat.OverHeatMode = true;
        UIManager.Instance.UpdateOverHeat(m_pCombat.OverHeat);
    }

    public override void DisableBehav()
    {
        print("OverHeat disable");
        m_pCombat.OverHeatMode = false;
        UIManager.Instance.UpdateAmmo(m_pCombat.CurrentAmmo, m_pCombat.MaxAmmo);
    }
}
