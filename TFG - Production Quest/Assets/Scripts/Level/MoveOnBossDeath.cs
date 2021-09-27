using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnBossDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject m_body;

    [SerializeField]
    private Transform m_startPos;

    [SerializeField]
    private Transform m_finalPos;

    [SerializeField]
    private float m_ascendingSpeed;

    [SerializeField]
    private bool m_startDeactivated;

    [Space(10)]

    [SerializeField]
    public Life m_bort;

    [SerializeField]
    GameObject[] m_deactivateObjects;

    private float m_lerpPos;
    private bool once;

    private void Start()
    {
        m_startPos.parent = null;
        m_finalPos.parent = null;
        m_lerpPos = 0;

        m_body.transform.position = m_startPos.position;

        for (int i = 0; i < m_deactivateObjects.Length; i++)
        {
            m_deactivateObjects[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (m_bort)
        {
            if (m_bort.isDead)
            {
                if (m_lerpPos < 1)
                {
                    m_lerpPos += Time.deltaTime * (m_ascendingSpeed / 10);
                    m_body.transform.position = Vector3.Lerp(m_startPos.position, m_finalPos.position, m_lerpPos);
                    StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(Time.deltaTime, .01f));
                }

                if (!once)
                {
                    for (int i = 0; i < m_deactivateObjects.Length; i++)
                    {
                        m_deactivateObjects[i].SetActive(true);
                    }
                    once = true;
                }
            }
        }
    }
}
