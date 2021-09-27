using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get => instance;
    }

    [SerializeField]
    private Image m_lifeBar;

    [SerializeField]
    private TextMeshProUGUI m_lifeText;

    [SerializeField]
    private GameObject m_BortlifeBars;

    [SerializeField]
    private Image[] m_BortBars;

    [SerializeField]
    private TextMeshProUGUI m_ammoText;

    [SerializeField]
    private TextMeshProUGUI m_moneyText;

    [Header("Menus")]
    [SerializeField]
    private GameObject m_pauseMenu;

    [SerializeField]
    private GameObject m_DeathMenu;

    [SerializeField]
    private GameObject m_ExchangeModMenu;

    [Header ("Options Menu Variables")]
    [SerializeField]
    private GameObject m_pauseButtons;

    [SerializeField]
    private GameObject m_pauseOptions;

    [SerializeField]
    private Slider m_musicVolumeSlider;

    [SerializeField]
    TextMeshProUGUI m_musicVolumeIndicator;

    [SerializeField]
    private Slider m_effectsVolumeSlider;

    [SerializeField]
    TextMeshProUGUI m_effectsVolumeIndicator;

    [Header("Mod Exchange Variables")]

    [SerializeField]
    private TextMeshProUGUI m_slot1_Name;

    [SerializeField]
    private Image m_slot1_Book;

    [SerializeField]
    private TextMeshProUGUI m_slot1_ExchangeText;

    [SerializeField]
    private TextMeshProUGUI m_slot2_Name;

    [SerializeField]
    private Image m_slot2_Book;

    [SerializeField]
    private TextMeshProUGUI m_slot2_ExchangeText;

    [SerializeField]
    private TextMeshProUGUI m_new_Name;

    [SerializeField]
    private Image m_new_Book;

    [Space(5)]
    [SerializeField]
    private Sprite m_emtyBook;

    private bool m_onMenus;
    private LevelManager m_lm;
    private BehaviourManager m_bm;
    private BaseBehaviour m_newMod;
    private AddBehaviourToWeapon m_newModInteractable;

    public bool OnMenus { get => m_onMenus; set => m_onMenus = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_lm = LevelManager.Instance;
        m_bm = BehaviourManager.Instance;
        m_pauseMenu.SetActive(false);
        m_DeathMenu.SetActive(false);
        m_ExchangeModMenu.SetActive(false);
        m_pauseOptions.SetActive(false);
        SetBortLifeBar(false);


        m_musicVolumeSlider.value = EffectsAudioManager.Instance.MusicVolume * 10;
        m_musicVolumeIndicator.text = (EffectsAudioManager.Instance.MusicVolume * 10).ToString();
        m_effectsVolumeSlider.value = EffectsAudioManager.Instance.EffectsVolume * 10;
        m_effectsVolumeIndicator.text = (EffectsAudioManager.Instance.EffectsVolume * 10).ToString();
    }

    public void UpdateLifeBar(float current, float max)
    {
        m_lifeBar.fillAmount = current / max;
        m_lifeText.text = current.ToString();
    }

    public void UpdateBortLifeBar(float current, float max)
    {
        float bar = max / 4;
        float bar1 = max - bar;
        float bar2 = max - bar*2;
        float bar3 = max - bar*3;
        float bar4 = 0;

        m_BortBars[0].fillAmount = (current - bar1) / bar;
        m_BortBars[1].fillAmount = (current - bar2) / bar;
        m_BortBars[2].fillAmount = (current - bar3) / bar;
        m_BortBars[3].fillAmount = (current - bar4) / bar;
    }

    public void SetBortLifeBar(bool value)
    {
        m_BortlifeBars.SetActive(value);
    }

    public void UpdateMoney(float money)
    {
        m_moneyText.text = money.ToString();
    }

    public void UpdateAmmo(float current, float max)
    {
        m_ammoText.text = current.ToString() + " / " + max;
    }

    public void UpdateOverHeat(float current)
    {
        m_ammoText.text = current.ToString() + "%";
    }

    public void ShowPausePanel(bool value)
    {
        m_pauseMenu.SetActive(value);
        m_pauseButtons.SetActive(true);
        m_pauseOptions.SetActive(false);
        m_onMenus = value;
    }

    public void ShowDeathMenu(bool value)
    {
        m_DeathMenu.SetActive(value);
        m_onMenus = value;
    }

    public void ShowExchangeMods(bool value, BaseBehaviour newMod, AddBehaviourToWeapon newModInteractable)
    {
        m_newMod = newMod;
        m_newModInteractable = newModInteractable;

        m_ExchangeModMenu.SetActive(value);
        m_onMenus = value;

        if (value)
        {
            BaseBehaviour slot1 = null;
            BaseBehaviour slot2 = null;

            if (m_bm.BehaviourSlot1)
            {
                slot1 = m_bm.BehaviourSlot1;
            }
            if (m_bm.BehaviourSlot2)
            {
                slot2 = m_bm.BehaviourSlot2;
            }

            if (slot1)
            {
                m_slot1_Name.text = slot1.name;
                m_slot1_Book.sprite = slot1.BookSprite;
                m_slot1_ExchangeText.text = "Replace";
            }
            else
            {
                m_slot1_Name.text = "Empty";
                m_slot1_Book.sprite = m_emtyBook;
                m_slot1_ExchangeText.text = "Equip";
            }

            if (slot2)
            {
                m_slot2_Name.text = slot2.name;
                m_slot2_Book.sprite = slot2.BookSprite;
                m_slot2_ExchangeText.text = "Replace";
            }
            else
            {
                m_slot2_Name.text = "Empty";
                m_slot2_Book.sprite = m_emtyBook;
                m_slot2_ExchangeText.text = "Equip";
            }

            m_new_Name.text = newMod.name;
            m_new_Book.sprite = newMod.BookSprite;
        }
    }

    #region UI Buttons

    public void UI_ResumeButton()
    {
        m_lm.PauseGame();
    }

    public void UI_RestartButton()
    {
        m_lm.Restart();
    }

    public void UI_QuitButton()
    {
        m_lm.QuitGame();
    }

    public void UI_MainMenuButton()
    {
        m_lm.GoMainMenu();
    }

    public void UI_DiscardMod(int slotNumber)
    {
        BehaviourManager.Instance.SetNewBehaviour(m_newMod, slotNumber);
        m_newModInteractable.Used();
        ShowExchangeMods(false, null, null);
    }

    public void UI_DiscardNewMod()
    {
        ShowExchangeMods(false, null, null);
    }

    public void UI_OptionsMenu()
    {
        m_pauseButtons.SetActive(!m_pauseButtons.activeSelf);
        m_pauseOptions.SetActive(!m_pauseOptions.activeSelf);
    }

    public void UI_SetMusicVolume(float volume)
    {
        EffectsAudioManager.Instance.SetMusicVolume(volume / 10);
        m_musicVolumeIndicator.text = volume.ToString();
    }

    public void UI_SetEffectsVolume(float volume)
    {
        EffectsAudioManager.Instance.SetEffectsVolume(volume / 10);
        m_effectsVolumeIndicator.text = volume.ToString();
    }

    #endregion
}
