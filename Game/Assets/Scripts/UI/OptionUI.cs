using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UI {

    public Button Btn_close;
    public Slider bgmSlider;
    public Slider EffectSlider;

    // Use this for initialization
    void Start()
    {
        SetButtonLisner(Btn_close, ButtonEvent);
        SetSliderLisner(bgmSlider, SliderEvent);
        SetSliderLisner(EffectSlider, SliderEvent);
    }

    private void OnEnable()
    {
        bgmSlider.value = 1f - SoundManager.m_Instance.GetBGMVolme();
        EffectSlider.value = 1f - SoundManager.m_Instance.GetEffectVolme();
    }

    public override void SliderEvent(Object obj)
    {
        base.SliderEvent(obj);

        switch(obj.name)
        {
            case "Bgm_slider":
                //soundManager.ChangedVolume(0.5f - bgmSlider.value);
                SoundManager.m_Instance.SetSoundVolme(1f - bgmSlider.value,true);
                break;

            case "Effectsound_slider":
                SoundManager.m_Instance.SetSoundVolme(1f - EffectSlider.value, false);
                break;
        }
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

    public void SoundOnOff(Toggle toggle)
    {
        SoundManager.m_Instance.SoundOnOff(!toggle.isOn);
    }
}
