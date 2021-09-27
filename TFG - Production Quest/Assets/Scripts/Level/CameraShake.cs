using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool m_returning;
    private bool m_inShakeDirection;
    private Vector3 m_destinationPos;
    private Vector3 m_originPos;

    private float m_currentInterpolate;

    private void Start()
    {
        //m_destinationPos = Vector3.zero;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);


            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private void Update()
    {
        /*Vector3 myZero = transform.parent.position;

        if (m_inShakeDirection && !m_returning)
        {
            m_currentInterpolate += Time.deltaTime * 40;
            m_currentInterpolate = Mathf.Clamp01(m_currentInterpolate);
            transform.position = Vector3.Lerp(m_originPos, m_destinationPos, m_currentInterpolate);
            if (Vector3.Distance(transform.position, m_destinationPos) < 0.07f)
            {
                transform.position = m_destinationPos;
                m_originPos = m_destinationPos;
                m_destinationPos = transform.InverseTransformDirection(Vector3.zero);
                m_returning = true;
                m_inShakeDirection = false;
                m_currentInterpolate = 0;
                print("llege a primer destino");
            }


        }
        else if(m_returning)
        {
            m_currentInterpolate += Time.deltaTime * 15;
            m_currentInterpolate = Mathf.Clamp01(m_currentInterpolate);
            transform.position = Vector3.Lerp(m_originPos, myZero, m_currentInterpolate);
            if (Vector3.Distance(transform.position, myZero) < 0.07f)
            {
                print("me reseteo");
                transform.position = myZero;
                m_returning = false;
            }
        }*/
    }

    /*public void ShakeInDirection(float duration, float magnitude, Vector3 dir)
    {
        m_originPos = transform.position;
        m_destinationPos = dir * magnitude; // (transform.position - dir)
        m_destinationPos.z = transform.parent.position.z;
        m_inShakeDirection = true;
        m_currentInterpolate = 0;
    }*/
}
