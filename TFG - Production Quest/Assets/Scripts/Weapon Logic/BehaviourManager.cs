using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManager : MonoBehaviour
{
    static BehaviourManager m_instance;

    [SerializeField]
    AudioClip m_pickUpModSound;

    public static BehaviourManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<BehaviourManager>();
            }
            return m_instance;
        }
    }

    [Header("Behaviours")]
    public BaseBehaviour BehaviourSlot1;
    public BaseBehaviour BehaviourSlot2;
    //public BaseBehaviour BehaviourSlot3; lets begin for the moment with 2 behaviours

    public delegate void BehavAction(GameObject bullet);
    public event BehavAction OnHitEffect;
    public event BehavAction OnEnemyDamaged;
    public event BehavAction OnDestroy;
    public event BehavAction OnCreated;

    private void Start()
    {
        if (BehaviourSlot1 != null)
        {
            BehaviourSlot1.EnableBehav();
        }
        if (BehaviourSlot2 != null)
        {
            BehaviourSlot2.EnableBehav();
        }
    }

    public void BulletCreated(GameObject bulletCreated)
    {
        if (OnCreated != null)
        {
            OnCreated?.Invoke(bulletCreated);
        }
    }

    public void BulletHitEffect(GameObject bullet)
    {
        if (OnHitEffect != null)
        {
            OnHitEffect?.Invoke(bullet);
        }
    }

    public void DamagedEnemy(GameObject enemy)
    {
        if (OnEnemyDamaged != null)
        {
            OnEnemyDamaged?.Invoke(enemy);
        }
    }

    public void BulletDestroy(GameObject bullet)
    {
        if (OnDestroy != null)
        {
            OnDestroy?.Invoke(bullet);
        }
    }

    public void SetNewBehaviour(BaseBehaviour newBehaviour, int slot)
    {
        if (slot == 1)
        {
            if (BehaviourSlot1 != null)
            {
                BehaviourSlot1.DisableBehav();
            }
            BehaviourSlot1 = newBehaviour;
            BehaviourSlot1.EnableBehav();
        }
        else if (slot == 2)
        {
            if (BehaviourSlot2 != null)
            {
                BehaviourSlot2.DisableBehav();
            }
            BehaviourSlot2 = newBehaviour;
            BehaviourSlot2.EnableBehav();
        }
        else
        {
            Debug.LogWarning("The slot " + slot + " doesnt exist currently");
        }

        if (m_pickUpModSound)
        {
            EffectsAudioManager.Instance.AudioOneshot(m_pickUpModSound);
        }
    }
    
    private void OnDisable()
    {
        if (BehaviourSlot1 != null)
        {
            BehaviourSlot1.DisableBehav();
        }

        if (BehaviourSlot2 != null)
        {
            BehaviourSlot2.DisableBehav();
        }
    }
}
