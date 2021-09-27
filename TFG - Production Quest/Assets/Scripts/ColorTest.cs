using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTest : MonoBehaviour
{
    [SerializeField]
    private Color DesiredColor;

    private Color m_originalColor;

    [SerializeField]
    float timeToChange;

    float step;

    private void Start()
    {
        m_originalColor = GetComponent<SpriteRenderer>().color;
        step = 0;
    }

    private void Update()
    {
        step += Time.deltaTime * 5;
        Color nextColor = Color.Lerp(m_originalColor, DesiredColor, (1/timeToChange)*step);
        GetComponent<SpriteRenderer>().color = nextColor;
    }
}
