using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenu;

    [SerializeField]
    private GameObject m_optionsMenu;

    [SerializeField]
    private GameObject m_creditsMenu;

    [SerializeField]
    private GameObject m_selectDifficultyMenu;

    [SerializeField]
    private Slider m_musicVolumeSlider;

    [SerializeField]
    TextMeshProUGUI m_musicVolumeIndicator;

    [SerializeField]
    private Slider m_effectsVolumeSlider;

    [SerializeField]
    TextMeshProUGUI m_effectsVolumeIndicator;

    [SerializeField]
    TextMeshProUGUI m_difficultyIndicator;

    private void Start()
    {
        m_mainMenu.SetActive(true);
        m_optionsMenu.SetActive(false);
        m_creditsMenu.SetActive(false);
        m_selectDifficultyMenu.SetActive(false);
        m_musicVolumeSlider.value = EffectsAudioManager.Instance.MusicVolume * 10;
        m_musicVolumeIndicator.text = (EffectsAudioManager.Instance.MusicVolume * 10).ToString();
        m_effectsVolumeSlider.value = EffectsAudioManager.Instance.EffectsVolume * 10;
        m_effectsVolumeIndicator.text = (EffectsAudioManager.Instance.EffectsVolume * 10).ToString();

        m_difficultyIndicator.text = LevelManager.CurrentDifficulty.ToString();
    }

    public void UI_PlayButton()
    {
        /*int index = SceneManager.GetActiveScene().buildIndex + 1;
        LevelManager.NextLevelIndex = 2;*/
        Life.extraLife = 0;
        PlayerCombat.staticExtraDamage = 0;
        PlayerCombat.staticWeaponSprite = null;
        PlayerInteract.StaticMoney = 0;
        SceneManager.LoadScene(2);        
    }

    public void UI_SelectDifficulty(bool value)
    {
        m_selectDifficultyMenu.SetActive(value);
        m_mainMenu.SetActive(!value);
    }

    public void UI_OptionsMenu()
    {
        m_mainMenu.SetActive(!m_mainMenu.activeSelf);
        m_creditsMenu.SetActive(false);
        m_optionsMenu.SetActive(!m_optionsMenu.activeSelf);
    }

    public void UI_Credits()
    {
        m_mainMenu.SetActive(!m_mainMenu.activeSelf);
        m_optionsMenu.SetActive(false);
        m_creditsMenu.SetActive(!m_creditsMenu.activeSelf);
    }

    public void UI_SetMusicVolume(float volume)
    {
        EffectsAudioManager.Instance.SetMusicVolume(volume/10);
        m_musicVolumeIndicator.text = volume.ToString();
    }

    public void UI_SetEffectsVolume(float volume)
    {
        EffectsAudioManager.Instance.SetEffectsVolume(volume/10);
        m_effectsVolumeIndicator.text = volume.ToString();
    }

    public void ChangeDifficulty(int value)
    {
        if ((LevelManager.CurrentDifficulty + value) <= Difficulty.VeryHard && (LevelManager.CurrentDifficulty + value) >= Difficulty.Easy)
        {
            LevelManager.CurrentDifficulty += value;
            m_difficultyIndicator.text = LevelManager.CurrentDifficulty.ToString();
        }
    }

    public void UI_Quit()
    {
        Application.Quit();
    }
}
