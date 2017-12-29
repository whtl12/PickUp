using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI : MonoBehaviour {

    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onBtnClick;

    // Use this for initialization
    void Start () {
		
	}

    //필요한 이벤트 함수는 오버라이딩.

    public virtual void ButtonEvent(Object obj)
    {
        //UI사운드는 여기서. 플레이

        Debug.Log("Base UI Sound On");
    }

    public void SetButtonLisner(Button btn,System.Action<GameObject> callback)
    {
        btn.onClick.AddListener(()=> callback(btn.gameObject));
    }
}
