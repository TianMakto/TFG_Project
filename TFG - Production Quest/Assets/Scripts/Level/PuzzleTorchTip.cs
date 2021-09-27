using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTorchTip : MonoBehaviour
{
    [SerializeField]
    GameObject m_blueTorch;

    [SerializeField]
    GameObject m_redTorch;

    [SerializeField]
    GameObject m_greenTorch;

    [SerializeField]
    Transform m_instantiatePos;

    public stoneColors m_stoneColor;

    private GameObject m_torch;

    public void Activate(stoneColors stone) 
    {
        if (stone == stoneColors.blue)
        {
            m_torch = Instantiate(m_blueTorch, m_instantiatePos.position, m_instantiatePos.rotation);
        }
        else if (stone == stoneColors.red)
        {
            m_torch = Instantiate(m_redTorch, m_instantiatePos.position, m_instantiatePos.rotation);
        }
        else if (stone == stoneColors.green)
        {
            m_torch = Instantiate(m_greenTorch, m_instantiatePos.position, m_instantiatePos.rotation);
        }

        m_stoneColor = stone;
    }

    public void ToggleTorch(bool value)
    {
        if (!value)
        {
            m_torch.transform.Find("Flametrhower ").GetComponent<ParticleSystem>().Stop();
        }
        else
        {
            m_torch.transform.Find("Flametrhower ").GetComponent<ParticleSystem>().Play();
        }
    }
}
