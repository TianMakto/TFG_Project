using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinText : MonoBehaviour
{
    [SerializeField]
    float m_speed;

    [SerializeField]
    float m_existanceTime;

    [SerializeField]
    TextMeshProUGUI m_text;

    [System.NonSerialized]
    public float m_moneyAmount;

    private float m_timeToFade;
    private float m_fadeSpeed;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        m_text.text = "+" + m_moneyAmount.ToString();


        m_timeToFade = m_existanceTime / 2;
        m_fadeSpeed = (1 / (m_existanceTime - m_timeToFade));
    }

    private void Update()
    {
        if (m_existanceTime > 0)
        {
            m_existanceTime -= Time.deltaTime;
            transform.position += (Vector3)Vector2.up * (m_speed / 10);

            if (m_timeToFade <= 0)
            {
                Color fadeColor = m_text.color;
                fadeColor.a -= m_fadeSpeed * Time.deltaTime;
                m_text.color = fadeColor;
            }
            else
            {
                m_timeToFade -= Time.deltaTime;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
