using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrailManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_particleBase;

    [SerializeField]
    private LineRenderer m_lineRenderer;

    [SerializeField]
    private float m_particleOverSize = 10;

    [SerializeField]
    private float m_lifeTime;

    private List<ParticleSystem> m_particlesList = new List<ParticleSystem>();
    private bool m_particlesDeactivated;
    private GameObject m_particleToModify;
    private ParticleSystem.EmissionModule m_emmisionModule;

    private bool m_alive;

    private void Start()
    {
        m_lineRenderer.SetPosition(0, transform.position);
        m_alive = true;
    }

    private void Update()
    {
        if (m_lifeTime > 0)
        {
            m_lifeTime -= Time.deltaTime;
        }
        else
        {
            m_alive = false;
            for (int i = 0; i < m_particlesList.Count; i++)
            {
                ParticleSystem.MainModule mainMod = m_particlesList[i].main;
                mainMod.loop = false;
            }
            Destroy(gameObject);
        }
    }

    public void UpdateLastPoint(Vector2 pos)
    {
        if (m_alive)
        {
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, pos);

            if (m_particleToModify == null) //Instantiate a particle system if there isnt one for this segment of the trail
            {
                m_particleToModify = Instantiate(m_particleBase, m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 2), Quaternion.identity);
                m_particlesList.Add(m_particleToModify.GetComponent<ParticleSystem>());
            }

            ScaleLastparticle();
            m_particleToModify.transform.right = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1) - m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 2);
        }
    }

    public void AddNewPoint(Vector2 pos)
    {
        if (m_lineRenderer)
        {
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, pos); //We set the position of the position just added
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 2, pos); //We set the position of the old last one

            m_particleToModify = null;
        }
    }

    private void ScaleLastparticle()
    {
        Vector3 newScale = m_particleToModify.transform.lossyScale;
        newScale.x = Vector3.Distance(m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1), m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 2)) / 2;
        m_particleToModify.transform.localScale = newScale;

        //Modify the rateOverTime depending on how big we make the particleSystem
        m_emmisionModule = m_particleToModify.GetComponent<ParticleSystem>().emission;
        m_emmisionModule.rateOverTime = m_particleToModify.GetComponent<ParticleSystem>().emission.rateOverTime.constant + (m_particleOverSize * newScale.x);

        //We move the particleSystem to the center of the lastPosition and secondToLastPosition;
        Vector3 middlePosition;
        Vector3 lastPosition = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 1);
        Vector3 sencondLastPosition = m_lineRenderer.GetPosition(m_lineRenderer.positionCount - 2);
        middlePosition.x = lastPosition.x + (sencondLastPosition.x - lastPosition.x) / 2;
        middlePosition.y = lastPosition.y + (sencondLastPosition.y - lastPosition.y) / 2;
        middlePosition.z = lastPosition.z + (sencondLastPosition.z - lastPosition.z) / 2;
        m_particleToModify.transform.position = middlePosition;
    }
}
