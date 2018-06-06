using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UI
{
    public Button GameClose;
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
}
