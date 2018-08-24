using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GetUIPosition
{
    public class UIPosition
    {
        public string OutGameUIPos = "OutGameUI";
        public string InGameUIPos = "InGameUI";
        public string BasicPopupUIPos = "BasicPopup";
        public string ShopUIPos = "ShopUI";
        public string OptionPopupPos = "OptionPopup";

        Dictionary<string, string> UIPosInfoDIc = new Dictionary<string, string>();
            //어떤 팝업을 열고 닫을껀지 알기위해서. 아래의 enum 문과 같은 string으로 해야함... ( 더 좋은 방법이 있을까..?)
        public UIPosition()
        {
            UIPosInfoDIc.Add("BasicPopup", BasicPopupUIPos);
            UIPosInfoDIc.Add("ShopUI", ShopUIPos);
            UIPosInfoDIc.Add("OutGameUI", OutGameUIPos);
            UIPosInfoDIc.Add("InGameUI", InGameUIPos);
            UIPosInfoDIc.Add("OptionPopup", OptionPopupPos);
        }


        public string GetObjectPosStr(string name)
        {
            if(UIPosInfoDIc.ContainsKey(name))
                return UIPosInfoDIc[name];

            return "";
        }
    }

}


public class UIManager : MonoBehaviour {

    PopupController m_PopupController;

    public enum StageUI
    {
        InGameUI,
        OutGameUI,
        ShopUI
    };

    public enum PopupUI
    {
        BasicPopup,
        OptionPopup
    };

    //PlayerSetting 에 LoadScene이랑  index 맞추기.
    public enum SceneLoadIndex
    {
        Intro =0,
        Start,
        Play
    }

  
    public GameObject UIParent;
    public static UIManager m_Instance;

    // Use this for initialization
    void Awake () {

        m_Instance = this;

        m_PopupController = new PopupController();
        
    }
	
	// Update is called once per frame
	void Update () {
        //if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                m_PopupController.ClosePopup();
            }

        }
    }

    public void ChangeStage(StageUI name)
    {
        switch(name)
        {
            case StageUI.InGameUI:
                StartCoroutine(delayStart());
                break;
            case StageUI.OutGameUI:
                if (SceneManager.GetActiveScene().buildIndex == (int)SceneLoadIndex.Play
                    || SceneManager.GetActiveScene().buildIndex == (int)SceneLoadIndex.Intro
                    )
                {
                    m_PopupController.Clear();
                    SceneManager.LoadScene((int)SceneLoadIndex.Start);
                    SoundManager.m_Instance.PlaySound(SoundManager.SoundList.START_SCEAN_BGM, SoundManager.SoundType.BGM);

                }
                else
                    m_PopupController.ChangeStage(GetUIObj(name));
                break;

            default:
                m_PopupController.ChangeStage(GetUIObj(name));
                break;
        }

       
    }
    public void OpenPopup(PopupUI name)
    {
        m_PopupController.OpenPopup(GetUIObj(name));
    }

    public void ClosePopup()
    {
        m_PopupController.ClosePopup();
    }

    public void AllClosePopup()
    {
        m_PopupController.AllClosePopup();
    }


    GameObject GetUIObj<T>(T uiName)
    {
        GetUIPosition.UIPosition posString = new GetUIPosition.UIPosition();
        GameObject gameObj = UIParent.transform.Find(posString.GetObjectPosStr(uiName.ToString())).gameObject;

        if (gameObj == null)
            Debug.LogError("UIManager UI Object NUll Error!");

        return gameObj;
    }

    private IEnumerator delayStart()
    {
        Image fade = GameObject.Find("Fade").GetComponent<Image>();
        while(fade.color.a < 1)
        {
            fade.color += new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        //yield return new WaitForSeconds(1.0f);
        //m_PopupController.Clear();
        SceneManager.LoadScene((int)SceneLoadIndex.Start);
        SceneManager.LoadSceneAsync((int)SceneLoadIndex.Play);
        SoundManager.m_Instance.PlaySound(SoundManager.SoundList.PLAY_SCEAN_BGM, SoundManager.SoundType.BGM);
    }

}
