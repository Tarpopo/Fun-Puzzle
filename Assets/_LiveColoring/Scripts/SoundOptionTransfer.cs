using System.Collections;
using System.Collections.Generic;
using ColoringProject;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionTransfer : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private OneSoundPlay oneSoundTest;

    private void Start()
    {
        soundSlider.value = SingletoneGameLogic.Instance.SoundVolime;
        musicSlider.value = SingletoneGameLogic.Instance.MusicVolime;
    }

    /// <summary>
    /// Включить/выключить звук и музыку
    /// </summary>
    public void TurnOnOffAllSound()
    {
        float newValue;
        if (SingletoneGameLogic.Instance.SoundVolime > 0.5f) newValue = 0;
        else newValue = 1;
        SingletoneGameLogic.Instance.SoundVolime = newValue;
        SingletoneGameLogic.Instance.MusicVolime = newValue;
    }

    /// <summary>
    /// Настроить звук
    /// </summary>
    public void SetSoundVolume(float volume)
    {
        SingletoneGameLogic.Instance.SoundVolime = volume;
        oneSoundTest.Play();
    }

    /// <summary>
    /// Настроить музыку
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        SingletoneGameLogic.Instance.MusicVolime = volume;  
    }
}
