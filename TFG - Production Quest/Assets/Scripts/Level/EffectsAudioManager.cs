using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAudioManager : MonoBehaviour
{
    static EffectsAudioManager m_instance;

    AudioSource m_audio;

    AudioSource m_ambientMusic;

    private static float m_musicVolume = 1;
    private static float m_effectsVolume = 1;

    [SerializeField][Tooltip("test sounds when volume chaged for sound effects")]
    AudioClip[] m_testEffectsSounds;

    public static EffectsAudioManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<EffectsAudioManager>();
            }
            return m_instance;
        }
    }

    public float MusicVolume { get => m_musicVolume; }
    public float EffectsVolume { get => m_effectsVolume; }

    private void Awake()
    {
        m_audio = GetComponent<AudioSource>();
        m_ambientMusic = transform.GetChild(0).GetComponent<AudioSource>();        
    }

    private void Start()
    {
        m_audio.volume = m_effectsVolume;
        m_ambientMusic.volume = m_musicVolume;
    }

    public void AudioOneshot(AudioClip clip)
    {
        m_audio.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume)
    {
        m_musicVolume = volume;
        m_ambientMusic.volume = m_musicVolume;
    }

    public void SetEffectsVolume(float volume)
    {
        m_effectsVolume = volume;
        m_audio.volume = m_effectsVolume;

        //feedback sound
        int index = Random.Range(0, m_testEffectsSounds.Length - 1);
        AudioOneshot(m_testEffectsSounds[index]);
    }
}
