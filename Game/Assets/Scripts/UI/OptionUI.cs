using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : UI {

    public Button Btn_close;
    public Slider bgmSlider;
    public SoundManager soundManager;

    // Use this for initialization
    void Start()
    {
        SetButtonLisner(Btn_close, ButtonEvent);
        SetSliderLisner(bgmSlider, SliderEvent);
    }

    public override void SliderEvent(Object obj)
    {
        base.SliderEvent(obj);

        switch(obj.name)
        {
            case "Bgm_slider":
                //soundManager.ChangedVolume(0.5f - bgmSlider.value);
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
}
