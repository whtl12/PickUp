using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UI {

    public Button Btn_close;
    public Slider bgmSlider;
    public AudioSource audioSource;
    public float soundVolume;

    // Use this for initialization
    void Start()
    {
        SetButtonLisner(Btn_close, ButtonEvent);
        audioSource.volume = 0.5f;
    }

    public void volumeChange()
    {
        audioSource.volume = 0.5f - bgmSlider.value;
        soundVolume = audioSource.volume;
    }

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);

        switch (obj.name)
        {
            case "Btn_close":
                UIManager.m_Instance.ClosePopup();
                break;

        }
    }
}
