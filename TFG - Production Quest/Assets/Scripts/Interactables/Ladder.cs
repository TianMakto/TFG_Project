using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ladder : InteractableFather
{
    [SerializeField]
    private bool m_vanessaMiracles = true;

    [SerializeField]
    private bool m_bossLevel = false;

    public override void Interact(PlayerInteract player)
    {
        base.Interact(player);

        if (!m_bossLevel)
        {
            if (!m_vanessaMiracles)
            {
                LevelManager.Instance.GoNextLevel();
            }
            else
            {
                LevelManager.NextLevelIndex = SceneManager.GetActiveScene().buildIndex +1;
                LevelManager.Instance.GoVanessaMiracles();
            }
        }
        else
        {
            LevelManager.Instance.GoMainMenu();
        }
    }
}
