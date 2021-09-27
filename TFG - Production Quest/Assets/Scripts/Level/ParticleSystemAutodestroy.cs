using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutodestroy : MonoBehaviour
{
    ParticleSystem self;

    void Start()
    {
        self = GetComponent<ParticleSystem>();   
    }

    void Update()
    {
        if (!self.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
