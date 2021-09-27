using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeactivate : MonoBehaviour
{
    [SerializeField]
    private float mintimer;

    [SerializeField]
    private float maxtimer;

    private float timer;

    private Collider2D m_holeCollider;

    private void Start()
    {
        timer = Random.Range(mintimer, maxtimer);

        /*if (transform.GetChild(0))
        {
            print(1);
            transform.GetChild(0).gameObject.SetActive(false);
            m_holeCollider = transform.GetChild(0).GetComponent<Collider2D>();
            m_holeCollider = GetComponent<Collider2D>();
            m_holeCollider.enabled = false;
        }*/
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if(GetComponent<Collider2D>().enabled == true)
        {
            GetComponent<Collider2D>().enabled = false;

            /*if (m_holeCollider)
            {
                print(2);
                m_holeCollider.enabled = true;
                m_holeCollider.gameObject.SetActive(true);
            }*/
        }
    }
}
