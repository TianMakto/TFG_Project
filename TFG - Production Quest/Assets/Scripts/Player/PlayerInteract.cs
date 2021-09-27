using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private float m_interactDistance;

    [SerializeField]
    private static float m_money;

    private InteractableFather m_objectToInteract;

    private LevelManager m_lm;
    private UIManager m_ui;
    private Life m_life;
    private InputMaster m_inputs;

    public static float StaticMoney { get => m_money; set => m_money = value; }
    public float Money { get => m_money; }

    private void Start()
    {
        m_ui = UIManager.Instance;
        m_lm = LevelManager.Instance;
        m_life = GetComponent<Life>();
        m_inputs = GetComponent<PlayerCombat>().Inputs;

        m_ui.UpdateMoney(m_money);
    }

    private void Update()
    {
        if (!m_life.isDead && !m_ui.OnMenus)
        {
            if (m_inputs.Player.Interact.triggered && m_objectToInteract)
            {
                m_objectToInteract.Interact(this);
                m_objectToInteract = null;
            }
            checkInteractables();
        }   
    }

    private void checkInteractables()
    {
        if(m_objectToInteract == null)
        {
            InteractableFather interactableNear = null;
            float currentDis = m_interactDistance;
            for (int i = 0; i < m_lm.InteractablesList.Count; i++)
            {
                float newDis = Vector2.Distance(transform.position, m_lm.InteractablesList[i].transform.position);

                if (currentDis > newDis)
                {
                    interactableNear = m_lm.InteractablesList[i];
                    currentDis = newDis;
                }
            }

            if (interactableNear != m_objectToInteract)
            {
                ChangeObjectNear(interactableNear);
            }
        }
        else
        {
            float newDis = Vector2.Distance(transform.position, m_objectToInteract.transform.position);
            if (m_interactDistance < newDis)
            {
                ChangeObjectNear(null);
            }
        }
    }

    private void ChangeObjectNear(InteractableFather item)
    {
        if (m_objectToInteract != null)
        {
            m_objectToInteract.NotLongerNear(this);
        }

        m_objectToInteract = item;

        if (m_objectToInteract != null)
        {
            m_objectToInteract.NearInteract(this);
        }
    }

    public void EarnMoney(float earnedMoney)
    {
        m_money += earnedMoney;
        m_ui.UpdateMoney(m_money);
    }

    public void WasteMoney(float wastedMoney)
    {
        m_money -= wastedMoney;
        m_ui.UpdateMoney(m_money);
    }
}
