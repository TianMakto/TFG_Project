using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    float m_speed;

    [SerializeField]
    float m_existanceTime;

    [SerializeField]
    TextMeshProUGUI m_text;

    [SerializeField]
    private Color m_healColorText;

    [SerializeField]
    private Color m_fireColorText;

    [SerializeField]
    private Color m_poisonColorText;

    [System.NonSerialized]
    public float m_damageAmount;

    private string m_beforeText;
    private float m_timeToFade;
    private float m_fadeSpeed;

    private void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;

        m_text.text = m_beforeText + m_damageAmount.ToString();

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

    public void HealText()
    {
        m_beforeText = "+";
        m_text.color = m_healColorText;
    }

    public void FireText()
    {
        m_text.color = m_fireColorText;
    }

    public void PoisonText()
    {
        m_text.color = m_poisonColorText;
    }
}
