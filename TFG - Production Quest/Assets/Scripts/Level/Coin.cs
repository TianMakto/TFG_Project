using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float m_minValue;
    public float m_maxValue;

    [SerializeField]
    private GameObject m_coinEffect;

    [SerializeField]
    private GameObject m_cointText;

    [SerializeField]
    private float m_speed = 3;

    [SerializeField]
    AudioClip m_pickUpCoinSound;

    private float justSpawnedTime;
    private GameObject m_player;

    private void Start()
    {
        m_player = LevelManager.Instance.Player;
        justSpawnedTime = 1;
    }

    private void Update()
    {
        if (justSpawnedTime > 0)
        {
            justSpawnedTime -= Time.deltaTime;
        }
        else
        {
            transform.position += ((m_player.transform.position - transform.position).normalized * m_speed) * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInteract>() && justSpawnedTime <= 0)
        {
            float actualValue = Mathf.Ceil(Random.Range(m_minValue - 1, m_maxValue));
            other.GetComponent<PlayerInteract>().EarnMoney(actualValue);
            Instantiate(m_coinEffect, transform.position, Quaternion.identity);

            float randomizer = 0.7f;
            float randomX = Random.Range(other.transform.position.x - randomizer, other.transform.position.x + randomizer);
            float randomY = Random.Range(other.transform.position.y - randomizer / 2, other.transform.position.y + randomizer / 2);
            Vector3 posRandom = new Vector3(randomX, randomY, other.transform.position.z);
            CoinText coin = Instantiate(m_cointText, posRandom, Quaternion.identity).GetComponent<CoinText>();
            coin.m_moneyAmount = actualValue;

            if (m_pickUpCoinSound)
            {
                EffectsAudioManager.Instance.AudioOneshot(m_pickUpCoinSound);
            }

            Destroy(gameObject);
        }
    }
}
