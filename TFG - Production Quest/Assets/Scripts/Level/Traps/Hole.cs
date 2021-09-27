using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Hole : MonoBehaviour
{
    [SerializeField]
    private bool m_horizontal;

    [SerializeField]
    private bool m_vertical;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.parent.GetComponent<SpriteRenderer>().isVisible)
        {
            float targetScale = 0.25f;
            GameObject target = other.transform.parent.gameObject;

            if (target.GetComponent<PlayerLocomotion>())
            {
                target.GetComponent<PlayerLocomotion>().OnHole = true;
            }
            else if (target.GetComponent<AI_Father>())
            {
                target.GetComponent<AIPath>().canMove = false;
                target.GetComponent<AI_Father>().OnHole = true;
            }
            else if (target.GetComponent<ColliderDeactivate>())
            {
                target.GetComponent<ColliderDeactivate>().enabled = false;
            }

            if (!m_horizontal && m_vertical)
            {
                target.transform.position = Vector3.Lerp(target.transform.position, new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z), 0.1f);
            }
            else if (m_horizontal && !m_vertical)
            {
                target.transform.position = Vector3.Lerp(target.transform.position, new Vector3(this.transform.position.x, target.transform.position.y, target.transform.position.z), 0.1f);
            }
            else if (m_horizontal && m_vertical)
            {
                target.transform.position = Vector3.Lerp(target.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, target.transform.position.z), 0.1f);
            }

            if (target.gameObject.transform.localScale.y <= targetScale)
            {
                if (target.GetComponent<Life>())
                {
                    if (!target.GetComponent<Life>().isDead)
                    {
                        target.GetComponent<Life>().SufferDamage(Mathf.Infinity, DamageType.normal);
                    }

                    if (target.GetComponent<AI_Father>())
                    {
                        Destroy(target, 0.3f);
                    }
                   
                }
                else
                {
                    Destroy(target);
                }
            }
            else //if (!other.GetComponent<PlayerCombat>())
            {
                target.gameObject.transform.localScale *= 0.95f;
            }
        }
    }
}
