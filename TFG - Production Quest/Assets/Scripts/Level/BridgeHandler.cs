using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bridgeParts = new List<GameObject>();

    [SerializeField]
    private bool m_startOpen;

    [SerializeField]
    private bool m_takeChilds = true;

    private void Start()
    {
        if (m_takeChilds)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                bridgeParts.Add(transform.GetChild(i).gameObject);
            }
        }


        if (m_startOpen)
        {
            for (int i = 0; i < bridgeParts.Count; i++)
            {
                bridgeParts[i].GetComponent<Animator>().SetBool("Open", true);
            }
        }
        else
        {
            for (int i = 0; i < bridgeParts.Count; i++)
            {
                bridgeParts[i].GetComponent<Animator>().SetBool("Open", false);
            }
        }
    }

    public void Open()
    {
        for (int i = 0; i < bridgeParts.Count; i++)
        {
            bridgeParts[i].GetComponent<Animator>().SetBool("Open", true);
        }
    }

    public void Close()
    {
        for (int i = 0; i < bridgeParts.Count; i++)
        {
            bridgeParts[i].GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
