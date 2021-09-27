using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableFather
{
    [SerializeField]
    List<GameObject> m_bridgesToOpen;

    [SerializeField]
    List<GameObject> m_bridgesToClose;

    [SerializeField]
    List<GameObject> m_objectsToActivate;

    [SerializeField]
    List<GameObject> m_objectsToDeactivate;

    [SerializeField]
    List<Lever> m_leversToReactivate;

    [SerializeField]
    Sprite m_leverUsed;

    [Space(10)]
    [SerializeField]
    private PuzzlePunishment m_puzzlePunishment;

    Sprite m_leverNotUsed;

    bool m_used;

    PuzzleRandomizer m_puzzleManager;

    [System.NonSerialized]
    public bool Punish = false;

    public PuzzleRandomizer PuzzleManager { get => m_puzzleManager; set => m_puzzleManager = value; }

    public int Row;

    protected override void Start()
    {
        base.Start();
        m_used = false;
        m_leverNotUsed = GetComponent<SpriteRenderer>().sprite;

        /*for (int i = 0; i < m_objectsToActivate.Count; i++)
        {
            m_objectsToActivate[i].SetActive(false);
        }

        for (int i = 0; i < m_objectsToDeactivate.Count; i++)
        {
            m_objectsToDeactivate[i].SetActive(true);
        }*/

        for (int i = 0; i < m_bridgesToOpen.Count; i++)
        {
            m_bridgesToOpen[i].GetComponent<BridgeHandler>().Close();
        }
    }

    public override void Interact(PlayerInteract player)
    {
        if (!m_used)
        {
            base.Interact(player);
            m_used = true;

            GetComponent<SpriteRenderer>().sprite = m_leverUsed;

            if (m_puzzleManager)
            {
                if(Row == 1)
                {
                    m_puzzleManager.Activate1stRowLever(this, GetComponent<PuzzleTorchTip>().m_stoneColor);
                }
                else if (Row == 2)
                {
                    m_puzzleManager.Activate2ndRowLever(this, GetComponent<PuzzleTorchTip>().m_stoneColor);
                }
                else if (Row == 3)
                {
                    m_puzzleManager.Activate3rdRowLever(this, GetComponent<PuzzleTorchTip>().m_stoneColor);
                }
            }
            else
            {
                if (Punish)
                {
                       m_puzzlePunishment.Punish();
                }
                

                for (int i = 0; i < m_bridgesToOpen.Count; i++)
                {
                    m_bridgesToOpen[i].GetComponent<BridgeHandler>().Open();
                }

                for (int i = 0; i < m_bridgesToClose.Count; i++)
                {
                    m_bridgesToClose[i].GetComponent<BridgeHandler>().Close();
                }
        
                for (int i = 0; i < m_objectsToDeactivate.Count; i++)
                {
                    m_objectsToDeactivate[i].SetActive(false);
                }

                for (int i = 0; i < m_objectsToActivate.Count; i++)
                {
                    m_objectsToActivate[i].SetActive(true);
                }

                for (int i = 0; i < m_leversToReactivate.Count; i++)
                {
                    m_leversToReactivate[i].Reactivate();
                }
            }
        }
    }

    public void AddObjectsToLists(GameObject bridgeToOpen, GameObject bridgeToClose, GameObject hole, GameObject objectToActivate, Lever leverToReactivate)
    {
        if (bridgeToOpen)
        {
            m_bridgesToOpen.Add(bridgeToOpen);
        }

        if (bridgeToClose)
        {
            m_bridgesToClose.Add(bridgeToClose);
        }

        if (hole)
        {
            m_objectsToDeactivate.Add(hole);
        }

        if (objectToActivate)
        {
            m_objectsToActivate.Add(objectToActivate);
        }

        if (leverToReactivate)
        {
            m_leversToReactivate.Add(leverToReactivate);
        }
    }

    public void Reactivate()
    {
        if (m_used)
        {
            LevelManager.Instance.AddInteractable(this);

            GetComponent<SpriteRenderer>().sprite = m_leverNotUsed;
            m_used = false;
        }
    }

    public void UsePuzzlePunish()
    {
        m_puzzlePunishment.Punish();
    }
}
