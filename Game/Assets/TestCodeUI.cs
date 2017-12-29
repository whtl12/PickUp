using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCodeUI : UI {

    public Button testBtn;
    public Button testBtn2;

	// Use this for initialization
	void Start () {
        SetButtonLisner(testBtn, ButtonEvent);
        SetButtonLisner(testBtn2, ButtonEvent);
	}

    public override void ButtonEvent(Object obj)
    {
        base.ButtonEvent(obj);
        Debug.Log("TestCodeUI Play : " + obj.name);

        switch (obj.name)
        {
            case "Button1":
                Debug.Log("Type 1 Play");
                break;

            case "Button2":
                Debug.Log("Type 2 Play");
                break;
        }
    }
}
