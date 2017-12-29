using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GetUIPosition
{
    public class UIPosition
    {
        public string OutGameUIPos = "OutGameUI";
        public string InGameUIPos = "InGameUI";
        public string BasicPopupUIPos = "BasicPopup";

        Dictionary<string, string> UIPosInfoDIc = new Dictionary<string, string>();

        //어떤 팝업을 열고 닫을껀지 알기위해서. 아래의 enum 문과 같은 string으로 해야함... ( 더 좋은 방법이 있을까..?)
        public UIPosition()
        {
            UIPosInfoDIc.Add("InGameUI", InGameUIPos);
            UIPosInfoDIc.Add("OutGameUI", OutGameUIPos);
            UIPosInfoDIc.Add("BasicPopup", BasicPopupUIPos);
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
        OutGameUI
    };

    public enum PopupUI
    {
        BasicPopup,
    };

    public GameObject UIParent;
    public static UIManager m_Instance;
    // Use this for initialization
    void Awake () {
        m_PopupController = new PopupController();
        m_Instance = this;
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
        m_PopupController.ChangeStage(GetUIObj(name));
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



}
