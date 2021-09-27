using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionByParticles : MonoBehaviour
{
    [SerializeField]
    private bool severalDamage = false;

    [System.NonSerialized]
    public float TotalDamage;

    [System.NonSerialized]
    public float extraCrit;

    /*[System.NonSerialized]
    public Stats m_userStats;*/

    List<GameObject> collidedWith = new List<GameObject>();
    private float timer;

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<Life>() && !collidedWith.Contains(other)) //other != collidedWith.Find(x => x.gameObject == other))
        {
            other.GetComponent<Life>().SufferDamage(TotalDamage, DamageType.fire);
            collidedWith.Add(other);
        }
        if (other.GetComponent<DestructibleFather>())
        {
            other.GetComponent<DestructibleFather>().Shatter();
        }
    }

    private void Update()
    {
        if (severalDamage)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                collidedWith = new List<GameObject>();
                timer = 0.15f;
            }
        }
    }
}
