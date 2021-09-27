using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Melee : AI_Father
{
    [SerializeField]
    private ParticleSystem m_attackParticles;

    protected override void AttackLogic() //This should be an animation Event
    {
        if (playerInDistance(m_attackDistance))
        {

            m_currentCooldown = m_attackCooldown;
            m_attackParticles.Play();
            m_pLife.SufferDamage(m_attackDamage, DamageType.normal);

        }
    }
}
