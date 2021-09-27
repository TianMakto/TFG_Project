using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableFather : MonoBehaviour
{
    [SerializeField]
    GameObject m_highlight;

    [SerializeField]
    protected GameObject m_canvas;

    [SerializeField]
    protected TextMeshProUGUI m_text;

    [SerializeField]
    protected string m_textToWrite;

    protected virtual void Start()
    {
        LevelManager.Instance.AddInteractable(this);

        if (m_canvas)
        {
            m_canvas.SetActive(false);
        }

        if (m_highlight)
        {
            m_highlight.SetActive(false);
        }

        if (m_text)
        {
            m_text.text = m_textToWrite;
        }
    }

    public virtual void Interact(PlayerInteract player)
    {
        LevelManager.Instance.RemoveInteractable(this);

        if (m_canvas)
        {
            m_canvas.SetActive(false);
        }

        if (m_highlight)
        {
            m_highlight.SetActive(false);
        }
    }

    public virtual void NearInteract(PlayerInteract player)
    {
        if (m_canvas)
        {
            m_canvas.SetActive(true);
        }

        if (m_highlight)
        {
            m_highlight.SetActive(true);
        }
    }

    public virtual void NotLongerNear(PlayerInteract player)
    {
        if (m_canvas)
        {
            m_canvas.SetActive(false);
        }

        if (m_highlight)
        {
            m_highlight.SetActive(false);
        }
    }
}
