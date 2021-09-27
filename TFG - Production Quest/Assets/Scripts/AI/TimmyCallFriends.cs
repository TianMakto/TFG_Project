using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyCallFriends : MonoBehaviour
{
    [SerializeField]
    private float m_distanceToCall = 5;

    [SerializeField]
    LayerMask m_enemiesLayer;

    RaycastHit2D[] m_objects;

    public void CallForFriends()
    {
        m_objects = Physics2D.CircleCastAll(transform.position, m_distanceToCall, transform.forward, 0, m_enemiesLayer);

        for (int i = 0; i < m_objects.Length; i++)
        {
            if (m_objects[i].transform.GetComponent<TimmyCallFriends>())
            {
                if (!m_objects[i].transform.GetComponent<AI_Father>().Chasing)
                {
                    m_objects[i].transform.GetComponent<AI_Father>().TimmyAdvisesTimmy();
                }
            }
        }
    }
}
