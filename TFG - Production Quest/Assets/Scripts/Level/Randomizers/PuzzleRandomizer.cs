using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum stoneColors
{
    blue,
    red,
    green
}

public class PuzzleRandomizer : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer m_1stRowStone;

    [SerializeField]
    SpriteRenderer m_2ndRowStone;

    [SerializeField]
    SpriteRenderer m_3rdRowStone;

    [SerializeField]
    Sprite m_blueStone;

    [SerializeField]
    Sprite m_redStone;

    [SerializeField]
    Sprite m_greenStone;

    [SerializeField]
    Color stoneUsedColor;

    [Space(10)]

    [Header("1st Row")]
    [SerializeField]
    private List<Lever> m_1stLevers = new List<Lever>();

    [SerializeField]
    private List<GameObject> m_1stBridges = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_1stHoles = new List<GameObject>();

    [Header("2nd Row")]
    [SerializeField]
    private List<Lever> m_2ndLevers = new List<Lever>();

    [SerializeField]
    private List<GameObject> m_2ndBridges = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_2ndHoles = new List<GameObject>();

    [Header("3rd Row")]
    [SerializeField]
    private List<Lever> m_3rdLevers = new List<Lever>();

    [SerializeField]
    private List<GameObject> m_3rdBridges = new List<GameObject>();

    [SerializeField]
    private List<GameObject> m_3rdHoles = new List<GameObject>();

    private stoneColors m_1Color;
    private stoneColors m_2Color;
    private stoneColors m_3Color;

    private stoneColors m_1stRowColor;
    private stoneColors m_2ndRowColor;
    private stoneColors m_3rdRowColor;

    private int m_1stIndex;
    private int m_2ndIndex;
    private int m_3rdIndex;

    private List<stoneColors> m_1stRowUsedColor = new List<stoneColors>();
    private List<stoneColors> m_2ndRowUsedColor = new List<stoneColors>();
    private List<stoneColors> m_3rdRowUsedColor = new List<stoneColors>();

    private void Awake()
    {
        AssembleStoneColors();

        for (int i = 0; i < m_1stLevers.Count; i++)
        {
            m_1stLevers[i].PuzzleManager = this;
            m_1stLevers[i].Row = 1;

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_1stRowUsedColor.Contains(stone));
            m_1stRowUsedColor.Add(stone);

            m_1stLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
        }

        for (int i = 0; i < m_2ndLevers.Count; i++)
        {
            m_2ndLevers[i].PuzzleManager = this;
            m_2ndLevers[i].Row = 2;

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_2ndRowUsedColor.Contains(stone));
            m_2ndRowUsedColor.Add(stone);

            m_2ndLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
        }

        for (int i = 0; i < m_3rdLevers.Count; i++)
        {
            m_3rdLevers[i].PuzzleManager = this;
            m_3rdLevers[i].Row = 3;

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_3rdRowUsedColor.Contains(stone));
            m_3rdRowUsedColor.Add(stone);

            m_3rdLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
        }

        #region commented
        /*
        //1st Row
        float higestNum = 0;
        Lever chosen1stLever = null;

        for (int i = 0; i < m_1stLevers.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if(newNum > higestNum)
            {
                higestNum = newNum;
                chosen1stLever = m_1stLevers[i];
            }
        }

        higestNum = 0;
        GameObject chosen1stBridge = null;
        GameObject chosen1stHole = null;

        for (int i = 0; i < m_1stBridges.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if (newNum > higestNum)
            {
                higestNum = newNum;
                chosen1stBridge = m_1stBridges[i];
                chosen1stHole = m_1stHoles[i];
            }
        }

        chosen1stLever.AddObjectsToLists(chosen1stBridge, null, chosen1stHole, null, null);
        chosen1stLever.GetComponent<PuzzleTorchTip>().Activate(m_1Color);
        m_1stRowUsedColor.Add(m_1Color);
        RemoveObjects(chosen1stLever, chosen1stBridge, chosen1stHole, true);


        //2nd Row
        higestNum = 0;
        Lever chosen2ndLever = null;

        for (int i = 0; i < m_2ndLevers.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if (newNum > higestNum)
            {
                higestNum = newNum;
                chosen2ndLever = m_2ndLevers[i];
            }
        }

        higestNum = 0;
        GameObject chosen2ndBridge = null;
        GameObject chosen2ndHole = null;

        for (int i = 0; i < m_2ndBridges.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if (newNum > higestNum)
            {
                higestNum = newNum;
                chosen2ndBridge = m_2ndBridges[i];
                chosen2ndHole = m_2ndHoles[i];
            }
        }

        chosen2ndLever.AddObjectsToLists(chosen2ndBridge, null, chosen2ndHole, null, null);
        chosen2ndLever.GetComponent<PuzzleTorchTip>().Activate(m_2Color);
        m_2ndRowUsedColor.Add(m_2Color);
        RemoveObjects(chosen2ndLever, chosen2ndBridge, chosen2ndHole, true);


        //3rd Row
        higestNum = 0;
        Lever chosen3rdLever = null;

        for (int i = 0; i < m_3rdLevers.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if (newNum > higestNum)
            {
                higestNum = newNum;
                chosen3rdLever = m_3rdLevers[i];
            }
        }

        higestNum = 0;
        GameObject chosen3rdBridge = null;
        GameObject chosen3rdHole = null;

        for (int i = 0; i < m_3rdBridges.Count; i++)
        {
            float newNum = Random.Range(1, 100);

            if (newNum > higestNum)
            {
                higestNum = newNum;
                chosen3rdBridge = m_3rdBridges[i];
                chosen3rdHole = m_3rdHoles[i];
            }
        }

        chosen3rdLever.AddObjectsToLists(chosen3rdBridge, null, chosen3rdHole, null, null);
        chosen3rdLever.GetComponent<PuzzleTorchTip>().Activate(m_3Color);
        m_3rdRowUsedColor.Add(m_3Color);
        RemoveObjects(chosen3rdLever, chosen3rdBridge, chosen3rdHole, true);


        //Rest of levers
        for (int i = 0; i < m_1stLevers.Count; i++)
        {
            m_1stLevers[i].AddObjectsToLists(null, chosen1stBridge, null, chosen1stHole, chosen1stLever);
            m_1stLevers[i].AddObjectsToLists(null, chosen2ndBridge, null, chosen2ndHole, chosen2ndLever);
            m_1stLevers[i].AddObjectsToLists(null, chosen3rdBridge, null, chosen3rdHole, chosen3rdLever);

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_1stRowUsedColor.Contains(stone));
            m_1stRowUsedColor.Add(stone);

            m_1stLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
            m_1stLevers[i].Punish = true;
        }

        for (int i = 0; i < m_2ndLevers.Count; i++)
        {
            m_2ndLevers[i].AddObjectsToLists(null, chosen1stBridge, null, chosen1stHole, chosen1stLever);
            m_2ndLevers[i].AddObjectsToLists(null, chosen2ndBridge, null, chosen2ndHole, chosen2ndLever);
            m_2ndLevers[i].AddObjectsToLists(null, chosen3rdBridge, null, chosen3rdHole, chosen3rdLever);

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_2ndRowUsedColor.Contains(stone));
            m_2ndRowUsedColor.Add(stone);

            m_2ndLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
            m_2ndLevers[i].Punish = true;
        }

        for (int i = 0; i < m_3rdLevers.Count; i++)
        {
            m_3rdLevers[i].AddObjectsToLists(null, chosen1stBridge, null, chosen1stHole, chosen1stLever);
            m_3rdLevers[i].AddObjectsToLists(null, chosen2ndBridge, null, chosen2ndHole, chosen2ndLever);
            m_3rdLevers[i].AddObjectsToLists(null, chosen3rdBridge, null, chosen3rdHole, chosen3rdLever);

            stoneColors stone;
            do
            {
                stone = (stoneColors)Random.Range(0, 3);
            } while (m_3rdRowUsedColor.Contains(stone));
            m_3rdRowUsedColor.Add(stone);

            m_3rdLevers[i].GetComponent<PuzzleTorchTip>().Activate(stone);
            m_3rdLevers[i].Punish = true;
        }
        */
        #endregion
    }

    private void Start()
    {
        m_1stRowColor = m_1Color;
        m_2ndRowColor = m_2Color;
        m_3rdRowColor = m_3Color;

        m_1stIndex = 0;
    }

    private void RemoveObjects(Lever lever, GameObject bridge, GameObject hole, bool value)
    {
        if (value)
        {
            //Removing Levers
            if (m_1stLevers.Contains(lever))
            {
                m_1stLevers.Remove(lever);
            }
            if (m_2ndLevers.Contains(lever))
            {
                m_2ndLevers.Remove(lever);
            }
            if (m_3rdLevers.Contains(lever))
            {
                m_3rdLevers.Remove(lever);
            }
        }

        //Removing bridges
        if (m_1stBridges.Contains(bridge))
        {
            m_1stBridges.Remove(bridge);
        }
        if (m_2ndBridges.Contains(bridge))
        {
            m_2ndBridges.Remove(bridge);
        }
        if (m_3rdBridges.Contains(bridge))
        {
            m_3rdBridges.Remove(bridge);
        }

        //Removing Holes
        if (m_1stHoles.Contains(hole))
        {
            m_1stHoles.Remove(hole);
        }
        if (m_2ndHoles.Contains(hole))
        {
            m_2ndHoles.Remove(hole);
        }
        if (m_3rdHoles.Contains(hole))
        {
            m_3rdHoles.Remove(hole);
        }
    }

    private void AssembleStoneColors()
    {
        m_1Color = (stoneColors)Random.Range(0, 3);

        do
        {
            m_2Color = (stoneColors)Random.Range(0, 3);
        } while (m_2Color == m_1Color);

        do
        {
            m_3Color = (stoneColors)Random.Range(0, 3);
        } while (m_3Color == m_1Color || m_3Color == m_2Color);



        if (m_1Color == stoneColors.blue)
        {
            m_1stRowStone.sprite = m_blueStone;
        }
        else if (m_1Color == stoneColors.red)
        {
            m_1stRowStone.sprite = m_redStone;
        }
        else if (m_1Color == stoneColors.green)
        {
            m_1stRowStone.sprite = m_greenStone;
        }

        if (m_2Color == stoneColors.blue)
        {
            m_2ndRowStone.sprite = m_blueStone;
        }
        else if (m_2Color == stoneColors.red)
        {
            m_2ndRowStone.sprite = m_redStone;
        }
        else if (m_2Color == stoneColors.green)
        {
            m_2ndRowStone.sprite = m_greenStone;
        }

        if (m_3Color == stoneColors.blue)
        {
            m_3rdRowStone.sprite = m_blueStone;
        }
        else if (m_3Color == stoneColors.red)
        {
            m_3rdRowStone.sprite = m_redStone;
        }
        else if (m_3Color == stoneColors.green)
        {
            m_3rdRowStone.sprite = m_greenStone;
        }
    }

    public void Activate1stRowLever(Lever leverUsed, stoneColors stone)
    {
        if (stone == m_1stRowColor)
        {
            m_1stIndex++;

            if (m_1stIndex == 3)
            {
                OpenRowBridges(1);
            }
            else
            {     
                if (m_1stIndex == 1)
                {
                    m_1stRowColor = m_2Color;
                    m_1stRowStone.color = stoneUsedColor;
                }

                if (m_1stIndex == 2)
                {
                    m_1stRowColor = m_3Color;
                    m_2ndRowStone.color = stoneUsedColor;
                }   
            }

            leverUsed.GetComponent<PuzzleTorchTip>().ToggleTorch(false);
        }
        else
        {
            m_1stIndex = 0;
            m_1stRowColor = m_1Color;
            leverUsed.UsePuzzlePunish();
            ReactivateRowLevers(1);
        }
    }

    public void Activate2ndRowLever(Lever leverUsed, stoneColors stone)
    {
        if (stone == m_2ndRowColor)
        {
            m_2ndIndex++;

            if (m_2ndIndex == 3)
            {
                OpenRowBridges(2);
            }
            else
            {
                if (m_2ndIndex == 1)
                {
                    m_2ndRowColor = m_3Color;
                    m_2ndRowStone.color = stoneUsedColor;
                }

                if (m_2ndIndex == 2)
                {
                    m_2ndRowColor = m_1Color;
                    m_3rdRowStone.color = stoneUsedColor;
                }
            }

            leverUsed.GetComponent<PuzzleTorchTip>().ToggleTorch(false);
        }
        else
        {
            m_2ndIndex = 0;
            m_2ndRowColor = m_2Color;
            leverUsed.UsePuzzlePunish();
            ReactivateRowLevers(2);
        }
    }

    public void Activate3rdRowLever(Lever leverUsed, stoneColors stone)
    {
        if (stone == m_3rdRowColor)
        {
            m_3rdIndex++;

            if (m_3rdIndex == 3)
            {
                OpenRowBridges(3);
            }
            else
            {
                if (m_3rdIndex == 1)
                {
                    m_3rdRowColor = m_1Color;
                    m_3rdRowStone.color = stoneUsedColor;
                }

                if (m_3rdIndex == 2)
                {
                    m_3rdRowColor = m_2Color;
                    m_1stRowStone.color = stoneUsedColor;
                }
            }

            leverUsed.GetComponent<PuzzleTorchTip>().ToggleTorch(false);
        }
        else
        {
            m_3rdIndex = 0;
            m_3rdRowColor = m_3Color;
            leverUsed.UsePuzzlePunish();
            ReactivateRowLevers(3);
        }
    }

    private void OpenRowBridges(int row)
    {
        ReactivateColors();

        if (row == 1)
        {
            for (int i = 0; i < m_1stBridges.Count; i++)
            {
                m_1stBridges[i].GetComponent<BridgeHandler>().Open();
            }

            for (int i = 0; i < m_1stHoles.Count; i++)
            {
                m_1stHoles[i].SetActive(false);
            }
        }
        else if (row == 2)
        {
            for (int i = 0; i < m_2ndBridges.Count; i++)
            {
                m_2ndBridges[i].GetComponent<BridgeHandler>().Open();
            }

            for (int i = 0; i < m_2ndHoles.Count; i++)
            {
                m_2ndHoles[i].SetActive(false);
            }
        }
        else if (row == 3)
        {
            for (int i = 0; i < m_3rdBridges.Count; i++)
            {
                m_3rdBridges[i].GetComponent<BridgeHandler>().Open();
            }

            for (int i = 0; i < m_3rdHoles.Count; i++)
            {
                m_3rdHoles[i].SetActive(false);
            }
        }
    }

    private void ReactivateRowLevers(int row)
    {
        ReactivateColors();
        if (row == 1)
        {
            for (int i = 0; i < m_1stLevers.Count; i++)
            {
                m_1stLevers[i].Reactivate();
                m_1stLevers[i].GetComponent<PuzzleTorchTip>().ToggleTorch(true);
            }
        }
        else if (row == 2)
        {
            for (int i = 0; i < m_2ndLevers.Count; i++)
            {
                m_2ndLevers[i].Reactivate();
                m_2ndLevers[i].GetComponent<PuzzleTorchTip>().ToggleTorch(true);
            }
        }
        else if (row == 3)
        {
            for (int i = 0; i < m_3rdLevers.Count; i++)
            {
                m_3rdLevers[i].Reactivate();
                m_3rdLevers[i].GetComponent<PuzzleTorchTip>().ToggleTorch(true);
            }
        }
    }

    private void ReactivateColors()
    {
        m_1stRowStone.color = Color.white;
        m_2ndRowStone.color = Color.white;
        m_3rdRowStone.color = Color.white;
    }

    /*private void AddSuccesiveRows()
    {
        m_2ndLevers.AddRange(m_1stLevers);
        m_3rdLevers.AddRange(m_2ndLevers);        

        m_2ndHoles.AddRange(m_1stHoles);
        m_3rdHoles.AddRange(m_2ndHoles);
    }*/
}
