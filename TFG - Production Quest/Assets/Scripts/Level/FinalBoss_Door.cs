using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Door : MonoBehaviour
{
    [SerializeField]
    private Transform m_startingPos;

    [SerializeField]
    private Transform m_finalPos;

    [SerializeField]
    private float m_speed;

    private float lerpPos;

    [System.NonSerialized]
    public bool closing;

    private void Start()
    {
        lerpPos = 0;
        transform.position = Vector3.Lerp(m_startingPos.position, m_finalPos.position, lerpPos);
    }

    private void Update()
    {
        if(lerpPos < 1 && closing)
        {
            lerpPos += Time.deltaTime * m_speed;
            transform.position = Vector3.Lerp(m_startingPos.position, m_finalPos.position, lerpPos);
        }
    }
}
