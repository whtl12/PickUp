using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCamera : MonoBehaviour {
    //Rotation도 해야되면 Trans 데이터로 변경하기.

    private GameObject TargetObj;
    public float MoveTime = 0;
    public int YposOffset = 1;
    private Vector3 BasePos;
    private bool RoolBack = false;

	// Use this for initialization
	void Start () {
        BasePos = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,50f))
            {
                TargetObj = hit.collider.gameObject;
                Debug.Log("Hit GameObject Name = " + TargetObj.name);
            }

        }


        if(Input.GetKeyDown(KeyCode.A))
        {
            RollBackCamera();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if(TargetObj != null)
                MoveCamera(TargetObj.transform.localPosition);
        }
    }


    void MoveCamera(Vector3 pos)
    {
        pos.y += YposOffset;
        iTween.MoveBy(gameObject, iTween.Hash("amount", pos, 
                                                "time", MoveTime,
                                                "islocal", true,
                                                "movetopath", false,
                                                "easetype", iTween.EaseType.easeInOutQuart
                                               ));

        TargetObj = null; // 일단 한번 할꺼니까...ㅎ..
        RoolBack = true;
    }

    void RollBackCamera()
    {
        if(RoolBack) // 일단 한번 할꺼니까...ㅎ..
        {
            iTween.MoveTo(gameObject, iTween.Hash("islocal", true,
                                                       "position", BasePos,
                                                       "time", MoveTime,
                                                       "easetype", iTween.EaseType.linear,
                                                       "oncomplete", "ViewResult",
                                                       "oncompletetarget", this.gameObject
                                                     ));
        }
           

        RoolBack = false;
    }
}
