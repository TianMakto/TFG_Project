using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BortSummoningTrigger : MonoBehaviour
{
    [SerializeField]
    SpawningBort m_bort;

    [SerializeField]
    FinalBoss_Door m_door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.GetComponent<PlayerCombat>())
        {
            m_bort.Summoning = true;
            m_door.closing = true;
            UIManager.Instance.SetBortLifeBar(true);
            Camera.main.transform.parent.GetComponent<CameraFollow>().m_secondObjective = m_bort.transform;
        }
    }
}
