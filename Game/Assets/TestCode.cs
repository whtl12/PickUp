using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.m_Instance.ChangeStage(UIManager.StageUI.InGameUI);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            UIManager.m_Instance.OpenPopup(UIManager.PopupUI.BasicPopup);
        }

        if (Input.GetKey(KeyCode.B))
        {
            UIManager.m_Instance.ClosePopup();
        }

        if (Input.GetKey(KeyCode.C))
        {
            UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
        }

        if (Input.GetKey(KeyCode.D))
        {

        }


    }
}
