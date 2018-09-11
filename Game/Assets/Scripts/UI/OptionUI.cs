using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UI {

    public Button Btn_close;
    public Slider bgmSlider;
    public Slider EffectSlider;
    public Toggle PlayToggle;
    public Toggle StopToggle;

    // Use this for initialization
    void Start()
    {
        SetButtonLisner(Btn_close, ButtonEvent);
        SetSliderLisner(bgmSlider, SliderEvent);
        SetSliderLisner(EffectSlider, SliderEvent);
    }

    private void OnEnable()
    {
        bgmSlider.value = EncryptValue.GetFloat("BGMSound");
        EffectSlider.value = EncryptValue.GetFloat("EffectSound");
        PlayToggle.isOn = EncryptValue.GetFloat("SoundIsOn", 0) == 1 ? false : true;
        StopToggle.isOn = !PlayToggle.isOn;
    }

    public override void SliderEvent(Object obj)
    {
        base.SliderEvent(obj);

        switch(obj.name)
        {
            case "Bgm_slider":
                //soundManager.ChangedVolume(0.5f - bgmSlider.value);
                SoundManager.m_Instance.SetSoundVolme(1f - bgmSlider.value,true);
                EncryptValue.SetFloat("BGMSound", 1f - bgmSlider.value);
                break;

            case "Effectsound_slider":
                SoundManager.m_Instance.SetSoundVolme(1f - EffectSlider.value, false);
                EncryptValue.SetFloat("EffectSound", 1f - EffectSlider.value);
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
        SoundManager.m_Instance.SoundOnOff(toggle.isOn);
        EncryptValue.SetFloat("SoundIsOn", toggle.isOn ? 0 : 1);
    }
}
