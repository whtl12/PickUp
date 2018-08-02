﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCamera : MonoBehaviour {
    //Rotation도 해야되면 Trans 데이터로 변경하기.

    private GameObject TargetObj;
    public float MoveTime = 0;
    public int YposOffset = 1;
    private Vector3 BasePos;
    private bool RoolBack = false;
    private bool RotateState = false;
    private float[] IslandRotateY = new float[5];
    private int IslandIndex = 0;
    private Vector3 tmpMousePosition;
    public Material leaf_material;
    [SerializeField] private float matR, matG, matB;
    [SerializeField] private GameObject islandParent;

    // Use this for initialization
    void Start () {
        BasePos = this.transform.localPosition;
        matR = 0.6f;
        matG = 0.4f;
        matB = 0.5f;
        IslandRotateY[0] = 0; // Start
        IslandRotateY[1] = 70;
        IslandRotateY[2] = 132;
        IslandRotateY[3] = 214;
        IslandRotateY[4] = 284;

        leaf_material.color = new Color(matR, matG, matB);
    }

    // Update is called once per frame
    void Update () {
        leaf_material.color = new Color(matR, matG, matB);
        if (Input.GetMouseButtonDown(0))
        {
            tmpMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (Mathf.Abs(tmpMousePosition.x - Input.mousePosition.x) > 50 && !RotateState && !RoolBack)
            {
                if (tmpMousePosition.x - Input.mousePosition.x > 0)
                    IslandIndex++;
                else
                    IslandIndex--;

                if (IslandIndex > 4)
                    IslandIndex = 0;
                if (IslandIndex < 0)
                    IslandIndex = 4;
                print("IslandIndex : " + IslandIndex);

                StartCoroutine(RotateIsland());
                RotateState = !RotateState;
                tmpMousePosition = Input.mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(0) && !RotateState)
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
    private void OnMouseDrag()
    {
        IslandIndex += (int)((tmpMousePosition.x - Input.mousePosition.x) / 20);
        print(IslandIndex);
    }
    IEnumerator RotateIsland()
    {
        iTween.RotateTo(islandParent, iTween.Hash("y", IslandRotateY[IslandIndex], 
                                                "time", MoveTime,
                                                "islocal", true
                                                ));
        yield return new WaitForSeconds(1.0f);
        RotateState = !RotateState;
        yield return null;
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
