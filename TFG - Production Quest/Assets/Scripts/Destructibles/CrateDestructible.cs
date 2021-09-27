using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDestructible : DestructibleFather
{
    [SerializeField]
    private GameObject m_crateShattered;

    [SerializeField]
    private AudioClip m_crateBreakingSound;

    public override void Shatter()
    {
        if (m_crateBreakingSound && GetComponent<SpriteRenderer>().isVisible)
        {
            EffectsAudioManager.Instance.AudioOneshot(m_crateBreakingSound);
        }

        Instantiate(m_crateShattered, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
