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
    private float IslandRotateY = 0;

	// Use this for initialization
	void Start () {
        BasePos = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit,50f))
            {
                TargetObj = hit.collider.gameObject;
                Debug.Log("Hit GameObject Name = " + TargetObj.name);

                if (TargetObj != null && !RoolBack)
                    MoveCamera(TargetObj.transform.localPosition);
            }
            else
                RollBackCamera();
        }


    }


    void MoveCamera(Vector3 pos)
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", new Vector3(pos.x, pos.y + YposOffset, pos.z - BasePos.z), 
                                                "time", MoveTime,
                                                "islocal", true,
                                                "movetopath", false,
                                                "easetype", iTween.EaseType.easeInOutQuart
                                               ));

        TargetObj = null; 
        RoolBack = true;
    }

    void RollBackCamera()
    {
        if(RoolBack)
        {
            iTween.MoveTo(gameObject, iTween.Hash("islocal", true,
                                                       "position", BasePos,
                                                       "time", MoveTime,
                                                       "easetype", iTween.EaseType.easeInOutQuart,
                                                       "oncomplete", "ViewResult",
                                                       "oncompletetarget", this.gameObject
                                                     ));
        }
           

        RoolBack = false;
    }
}
