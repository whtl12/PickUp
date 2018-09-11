using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UI
{
    public static InGameUI m_Instance;
    public Button GameClose;
    public Button btnExit;
    public Button btnAdvert;
    public Text txtCalc;
    public Slider sldHP;
    public GameObject panelGameOver;
    [SerializeField] private Text txtblue;
    [SerializeField] private Text txtred;
    [SerializeField] private Text txtgreen;
    [SerializeField] private Text txtwhite;
    [SerializeField] private Text txtblack;
    
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }
    void Start () {
        SetButtonLisner(GameClose, ButtonEvent);
        SetButtonLisner(btnExit, ButtonEvent);
        SetButtonLisner(btnAdvert, ButtonEvent);
        
        panelGameOver.SetActive(false);
    }

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);

        switch (obj.name)
        {
            case "GameClose":
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
                break;
            case "btnExit":
                PlayManager.m_Instance.mainPlayer.GetComponentInChildren<DetectArea>().saveScore();
                UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
                break;
            case "btnAdvert": // 광고 실행
                StartCoroutine(UnityAdsHelper.Instance.ShowRewardedAd());
                break;

        }
        if (Time.timeScale != 1)
            Time.timeScale = 1;
    }
    public override void SetText(Object obj, string _str)
    {
        base.SetText(obj, _str);
        GameObject gobj = obj as GameObject;
        if(gobj.transform.parent.name == "Water")
        {
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
        } else
        {
            switch(gobj.name)
            {
                case "txtCalc":
                    txtCalc.text = _str;
                    break;
            }
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
    public override void SetActive(Object obj, bool state)
    {
        base.SetActive(obj, state);
        GameObject gobj = obj as GameObject;
        gobj.SetActive(state);
    }
}
