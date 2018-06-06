using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UI
{
    public Button Close;
    // Use this for initialization
    void Start () {
        SetButtonLisner(Close, ButtonEvent);
    }

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);

        switch (obj.name)
        {
            case "Close":
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
                break;

        }
    }
}
