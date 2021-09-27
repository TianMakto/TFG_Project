using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum UpgradeType
{
    wand,
    life
}

public class PlatformUpgrade : InteractableFather
{
    [SerializeField]
    Sprite[] m_spriteUpgrades;

    [SerializeField]
    float[] m_upgrades;

    [SerializeField]
    float[] m_costs;

    [SerializeField]
    SpriteRenderer m_spriteForUpgrade;

    [SerializeField]
    UpgradeType m_upgradeType;

    [SerializeField]
    Animator m_vanessaAnimator;

    private static int indexLife = 0;

    private static int indexWand = 0;

    private float m_cost;

    private float m_upgrade;

    [SerializeField]
    private TextMeshProUGUI m_moneyCost;

    protected override void Start()
    {
        base.Start();
        UpdateUpgrade();
    }

    public override void Interact(PlayerInteract player)
    {
        if (player.Money >= m_cost)
        {

            if (m_upgradeType == UpgradeType.life)
            {
                if (indexLife < m_upgrades.Length)
                {
                    player.GetComponent<Life>().UpgradeLife(m_upgrades[indexLife]);
                    indexLife++;

                    player.WasteMoney(m_cost);
                    m_vanessaAnimator.SetTrigger("Jump");
                    UpdateUpgrade();
                }
            }
            else if (m_upgradeType == UpgradeType.wand)
            {
                if (indexWand < m_upgrades.Length)
                {
                    player.GetComponent<PlayerCombat>().ExtraDamage = m_upgrades[indexWand];
                    player.GetComponent<PlayerCombat>().UpdateWand(m_spriteUpgrades[indexWand]);
                    indexWand++;

                    player.WasteMoney(m_cost);
                    m_vanessaAnimator.SetTrigger("Jump");
                    UpdateUpgrade();
                }
            }
        }
    }

    private void UpdateUpgrade()
    {
        if (m_upgradeType == UpgradeType.life)
        {
            if (indexLife < m_upgrades.Length)
            {
                m_text.text = "Max Life: " + (m_upgrades[indexLife] + LevelManager.Instance.Player.GetComponent<Life>().GetBaseMaxLife());
                m_cost = m_costs[indexLife];
                m_spriteForUpgrade.sprite = m_spriteUpgrades[indexLife];
                m_moneyCost.text = m_cost.ToString();
            }
            else
            {
                m_text.text = "Maxed Life";
                m_spriteForUpgrade.sprite = null;
                m_moneyCost.text = "-";
                base.Interact(null);
            }
        }
        else if (m_upgradeType == UpgradeType.wand)
        {
            if (indexWand < m_upgrades.Length)
            {
                m_text.text = "Damage: " + (m_upgrades[indexWand] + 7);
                m_cost = m_costs[indexWand];
                m_spriteForUpgrade.sprite = m_spriteUpgrades[indexWand];
                m_moneyCost.text = m_cost.ToString();
            }
            else
            {
                m_text.text = "Maxed Damage";
                m_spriteForUpgrade.sprite = null;
                m_moneyCost.text = "-";
                base.Interact(null);
            }
        }

    }
}
