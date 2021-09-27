using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBort : MonoBehaviour
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
    private ParticleSystem m_particles;

    [Space(10)]

    [SerializeField]
    private GameObject m_bortPrefab;

    [SerializeField]
    MoveOnBossDeath[] m_deathDoors;

    private float m_lerpPos;

    private bool m_summoning;

    public bool Summoning { get => m_summoning; set => m_summoning = value; }

    private void Start()
    {
        m_startPos.parent = null;
        m_finalPos.parent = null;
        m_lerpPos = 0;

        m_body.transform.position = m_startPos.position;
    }

    private void Update()
    {
        if (m_summoning)
        {
            if(m_lerpPos < 1)
            {
                m_lerpPos += Time.deltaTime * (m_ascendingSpeed / 10);
                m_body.transform.position = Vector3.Lerp(m_startPos.position, m_finalPos.position, m_lerpPos);
                StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake(Time.deltaTime, .1f));
            }
            else
            {
                //Summoning Complete
                m_particles.transform.parent = null;
                m_particles.Stop();
                
                GameObject bort =  Instantiate(m_bortPrefab, transform.position, transform.rotation);

                for (int i = 0; i < m_deathDoors.Length; i++)
                {
                    m_deathDoors[i].m_bort = bort.GetComponent<Life>();
                }

                Destroy(gameObject);
            }
        }
    }
}
