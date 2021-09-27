using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ElementHighLight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image[] elements;

    [SerializeField]
    private Color highlightedColor;

    [SerializeField]
    private Color NothighlightedColor;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].color = highlightedColor;
        }
    }
    
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].color = NothighlightedColor;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].color = NothighlightedColor;
        }
    }
}
