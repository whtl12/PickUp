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

    int iblue, ired, igreen, iwhite, iblack;
    // Use this for initialization
    void Start () {
        SetButtonLisner(GameClose, ButtonEvent);

        iblue = ired = igreen = iwhite = iblack = 0;
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
    public override void SetText(Object obj)
    {
        base.SetText(obj);
        GameObject gobj = obj as GameObject;

        switch ((int)gobj.name[12] - 49)
        {
            case (int)MapManager.Item.Blue:
                iblue++;
                txtblue.text = "Blue " + iblue.ToString();
                break;
            case (int)MapManager.Item.Red:
                ired++;
                txtred.text = "Red " + ired.ToString();
                break;
            case (int)MapManager.Item.Green:
                igreen++;
                txtgreen.text = "Green " + igreen.ToString();
                break;
            case (int)MapManager.Item.White:
                iwhite++;
                txtwhite.text = "White " + iwhite.ToString();
                break;
            case (int)MapManager.Item.Black:
                iblack++;
                txtblack.text = "Black " + iblack.ToString();
                break;
        }
    }
}
