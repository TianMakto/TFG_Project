using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    VeryHard
}


public class LevelManager : MonoBehaviour
{
    static LevelManager m_instance;

    public static LevelManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<LevelManager>();
            }
            return m_instance;
        }
    }
    private GameObject m_player;
    private GameObject m_bullet;

    private BoxCollider2D m_playerHitBox;
    private InputMaster m_inputs;

    public static Difficulty CurrentDifficulty = Difficulty.Normal;

    [SerializeField]
    private Texture2D m_cursorArrow;

    [SerializeField]
    private List<InteractableFather> m_interactablesList = new List<InteractableFather>();

    [SerializeField]
    private List<BaseBehaviour> m_behavioursTypes = new List<BaseBehaviour>();

    public static int NextLevelIndex;

    public GameObject Player { get => m_player; }
    public BoxCollider2D PlayerHitBox { get => m_playerHitBox; }
    public GameObject Bullet { get => m_bullet; }
    public List<InteractableFather> InteractablesList { get => m_interactablesList; }
    public List<BaseBehaviour> BehavioursTypes { get => m_behavioursTypes; }

    private bool paused;

    private void Awake()
    {
        m_instance = this;

        do
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
            m_playerHitBox = m_player.GetComponent<BoxCollider2D>();
        } while (!m_player.GetComponent<PlayerCombat>());

        m_bullet = m_player.GetComponent<PlayerCombat>().BulletPrefab;
    }

    private void Start()
    {
        Time.timeScale = 1;
        m_inputs = m_player.GetComponent<PlayerCombat>().Inputs;
        //Cursor.SetCursor(m_cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if (m_inputs.Player.PauseGame.triggered)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (!paused)
        {
            paused = true;
            Time.timeScale = 0;
            UIManager.Instance.ShowPausePanel(paused);
        }
        else
        {
            paused = false;
            Time.timeScale = 1;
            UIManager.Instance.ShowPausePanel(paused);
        }
    }

    #region List Management

    public void AddInteractable(InteractableFather value)
    {
        m_interactablesList.Add(value);
    }

    public void RemoveInteractable(InteractableFather value)
    {
        m_interactablesList.Remove(value);
    }

    #endregion

    #region Scene Management
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoVanessaMiracles()
    {
        SceneManager.LoadScene(1);
    }

    public void GoNextLevel()
    {
        if (NextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(NextLevelIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
    #endregion
}
