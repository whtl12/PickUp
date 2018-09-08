using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UI
{
    public Button GameClose;
    [SerializeField] private Text txtblue;
    [SerializeField] private Text txtred;
    [SerializeField] private Text txtgreen;
    [SerializeField] private Text txtwhite;
    [SerializeField] private Text txtblack;
    [SerializeField] private Slider sldHP;

    // Use this for initialization
    void Start () {
        SetButtonLisner(GameClose, ButtonEvent);

    }

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);

        switch (obj.name)
        {
            case "GameClose":
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
                break;

        }
    }
    public override void SetText(Object obj, string _str)
    {
        base.SetText(obj, _str);

        switch (obj.name[12] - 49)
        {
            case (int)MapManager.Item.Blue:
                txtblue.text = "Blue " + _str;
                break;
            case (int)MapManager.Item.Red:
                txtred.text = "Red " + _str;
                break;
            case (int)MapManager.Item.Green:
                txtgreen.text = "Green " + _str;
                break;
            case (int)MapManager.Item.White:
                txtwhite.text = "White " + _str;
                break;
            case (int)MapManager.Item.Black:
                txtblack.text = "Black " + _str;
                break;
        }
    }
    public override void SetValue(string obj, float _value)
    {
        base.SetValue(obj, _value);

        switch (obj)
        {
            case "sldHPbar":
                sldHP.value = _value;
                break;
        }
    }
}
