using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_modsToInstantiate = new List<GameObject>();

    [SerializeField]
    private static List<GameObject> m_mods = new List<GameObject>();

    private void Awake()
    {
        m_mods = m_modsToInstantiate;
    }

    void Start()
    {
        int index = Random.Range(0, m_mods.Count);

        Instantiate(m_mods[index], transform.position, transform.rotation);
        m_mods.Remove(m_mods[index]);
        gameObject.SetActive(false);
    }
}
