using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutGameUI : UI
{

    public Button Shop;
    public Button GameStart;

    // Use this for initialization
    void Start()
    {
        UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
        SetButtonLisner(Shop, ButtonEvent);
        SetButtonLisner(GameStart, ButtonEvent);

    }

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);

        switch (obj.name)
        {
            case "Shop":
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.ShopUI);
                break;

            case "GameStart":
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.InGameUI);
                break;
        }
    }

}
