using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCamera : MonoBehaviour {
    //Rotation도 해야되면 Trans 데이터로 변경하기.

    private GameObject TargetObj;
    public float MoveTime = 0;
    public int YposOffset = 1;
    private Vector3 BasePos;
    private bool RollBack = false;
    private bool RotateState = false;
    private float[] IslandRotateY = new float[5];
    private GameObject[] IslandEntry = new GameObject[5];
    private int IslandIndex = 0;
    private Vector3 tmpMousePosition;
    [SerializeField] private float matR, matG, matB;
    [SerializeField] private Material leaf_material;
    [SerializeField] private GameObject islandParent;
    [SerializeField] private OutGameUI outgameUI;

    // Use this for initialization
    void Start () {
        BasePos = this.transform.localPosition;
        IslandRotateY[0] = 0; // Start
        IslandRotateY[1] = 70;
        IslandRotateY[2] = 132;
        IslandRotateY[3] = 214;
        IslandRotateY[4] = 284;
        for(int i = 0; i < islandParent.transform.childCount; i++)
            IslandEntry[i] = islandParent.transform.GetChild(i).gameObject;

        matR = 0.6f;
        matG = 0.4f;
        matB = 0.5f;
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
            if (Mathf.Abs(tmpMousePosition.x - Input.mousePosition.x) > 50 && !RotateState && !RollBack)
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
                if(!RollBack)
                    CloseUpIsland();
                else
                {
                    if (IslandIndex == 0)
                        outgameUI.ButtonEvent(IslandEntry[IslandIndex]);
                }
            }
            else
                RollBackCamera();
        }


    }
    private void CloseUpIsland()
    {
        if (TargetObj != null)
            if (TargetObj.name == IslandEntry[IslandIndex].name)
                MoveCamera(TargetObj.transform.position);
    }
    IEnumerator RotateIsland()
    {
        iTween.RotateTo(islandParent, iTween.Hash("y", IslandRotateY[IslandIndex],
                                                "time", MoveTime,
                                                "islocal", true
                                                ));
        foreach(GameObject islandObject in IslandEntry)

        iTween.RotateTo(islandObject, iTween.Hash("y", 0,
                                                "time", MoveTime,
                                                "islocal", false
                                                ));
        yield return new WaitForSeconds(1.0f);
        RotateState = !RotateState;
        yield return null;
    }

    void MoveCamera(Vector3 pos)
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", new Vector3(pos.x, pos.y + YposOffset, pos.z), 
                                                "time", MoveTime,
                                                "islocal", false,
                                                "movetopath", false,
                                                "easetype", iTween.EaseType.easeInOutQuart
                                               ));

        TargetObj = null; 
        RollBack = true;
    }

    void RollBackCamera()
    {
        if(RollBack)
        {
            iTween.MoveTo(gameObject, iTween.Hash("islocal", true,
                                                       "position", BasePos,
                                                       "time", MoveTime,
                                                       "easetype", iTween.EaseType.easeInOutQuart,
                                                       "oncomplete", "ViewResult",
                                                       "oncompletetarget", this.gameObject
                                                     ));
        }
           

        RollBack = false;
    }
}
