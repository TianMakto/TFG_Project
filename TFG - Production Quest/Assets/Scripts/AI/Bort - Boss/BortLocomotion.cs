using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BortLocomotion : MonoBehaviour
{
    private WaypointInfo[] m_waypoints;
    private WaypointInfo m_lastWaypoint;
    private WaypointInfo waypointTarget = null;
    private AIDestinationSetter m_AIDestinationSetter;
    private AIPath m_AI;
    private Animator m_animator;
    private bool m_goingToWaypoint;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        m_AI = GetComponent<AIPath>();
        m_animator = GetComponent<Animator>();
        m_waypoints = FindObjectsOfType<WaypointInfo>();
        m_AIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetFloat("Speed", m_AI.velocity.magnitude);

        if (timer <= 0)
        {
            if (!m_goingToWaypoint)
            {
                float newDis = Mathf.Infinity;
                WaypointInfo newTarget = null;

                for (int i = 0; i < m_waypoints.Length; i++)
                {
                    if (!m_waypoints[i].Catched && m_waypoints[i] != m_lastWaypoint)
                    {
                        float currentDis = Vector2.Distance(transform.position, m_waypoints[i].transform.position);
                        if (currentDis < newDis)
                        {
                            newDis = currentDis;
                            newTarget = m_waypoints[i];
                        }
                    }
                }

                if(newTarget == null)
                {
                    for (int i = 0; i < m_waypoints.Length; i++)
                    {
                        if (m_waypoints[i].Catched && m_waypoints[i] != m_lastWaypoint)
                        {
                            m_waypoints[i].Catched = false;
                        }
                    }
                    newTarget = m_waypoints[Random.Range(0, m_waypoints.Length)];
                }
                if (newTarget != null)
                {
                    if (waypointTarget)
                    {
                        m_lastWaypoint = waypointTarget;
                        //m_lastWaypoint.Catched = false;
                    }

                    waypointTarget = newTarget;
                    waypointTarget.Catched = true;

                    m_goingToWaypoint = true;
                    m_AIDestinationSetter.target = waypointTarget.transform;
                }
            }
            else
            {
                if (Vector2.Distance(waypointTarget.transform.position, transform.position) <= 0.3f)
                {
                    m_goingToWaypoint = false;
                    timer = Random.Range(1, 3);
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
