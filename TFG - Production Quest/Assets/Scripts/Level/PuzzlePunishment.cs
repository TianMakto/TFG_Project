using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePunishment : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_GOToInstantiate;

    [SerializeField]
    List<Transform> m_positions;

    [SerializeField]
    private bool m_cathChilds = true;



    private void Start()
    {
        if (m_cathChilds)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                m_positions.Add(transform.GetChild(i));
            }
        }
    }

    public void Punish()
    {
        for (int i = 0; i < m_positions.Count; i++)
        {
            float higestNum = 0;
            GameObject chosen = null;

            for (int t = 0; t < m_GOToInstantiate.Count; t++)
            {
                float newNum = Random.Range(1, 100);

                if (newNum > higestNum)
                {
                    higestNum = newNum;
                    chosen = m_GOToInstantiate[t];
                }
            }

            Instantiate(chosen, m_positions[i].position, m_positions[i].rotation);
        }
    }
}
