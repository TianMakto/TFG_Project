using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableFather
{
    [SerializeField]
    private float m_minCoinNumber;

    [SerializeField]
    private float m_maxCoinNumber;

    [SerializeField]
    private GameObject m_coinPrefab;

    [SerializeField]
    private PuzzlePunishment m_puzzlePunishment;

    [SerializeField]
    AudioClip m_chestOpenSound;

    private bool m_opened = false;

    public override void Interact(PlayerInteract player)
    {
        if (m_opened == false)
        {
            m_opened = true;

            base.Interact(player);
            GetComponent<Animator>().SetTrigger("Open");
            float totalCoins = Mathf.Ceil(Random.Range(m_minCoinNumber - 1, m_maxCoinNumber));
            for (int i = 0; i < totalCoins; i++)
            {
                GameObject newItem = Instantiate(m_coinPrefab, transform.position, transform.rotation);
                newItem.GetComponent<Rigidbody2D>().AddForce((Vector2.up * Random.Range(-60, 60)) + (Vector2.right * Random.Range(-50, 50)));
            }

            if (m_chestOpenSound)
            {
                EffectsAudioManager.Instance.AudioOneshot(m_chestOpenSound);
            }

            if (m_puzzlePunishment)
            {
                m_puzzlePunishment.Punish();
            }
        }
    }
}
