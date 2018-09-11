using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCamera : MonoBehaviour {
    //Rotation도 해야되면 Trans 데이터로 변경하기.

    private GameObject TargetObj;
    public float MoveTime = 0;
    public int YposOffset = 1;
    public int ZposOffset = -2;
    private Vector3 BasePos;
    private bool RollBack = false;
    private bool RotateState = false;
    private float[] IslandRotateY = new float[5];
    private GameObject[] IslandEntry = new GameObject[5];
    private int IslandIndex = 0;
    private Vector3 tmpMousePosition;
    private GameObject tmpTarget;
    [SerializeField][Range(0f, 1f)] private float matR, matG, matB;
    [SerializeField] private Material leaf_material;
    [SerializeField] private GameObject islandParent;
    [SerializeField] private OutGameUI outgameUI;
    enum Island {
        Start = 0,
        Shop,
        b,
        Record,
        Option,
        MAX
    }

    // Use this for initialization
    void Start () {
        BasePos = this.transform.localPosition;
        IslandRotateY[(int)Island.Start] = 0; // Start
        IslandRotateY[(int)Island.Shop] = 70;
        IslandRotateY[(int)Island.b] = 132;
        IslandRotateY[(int)Island.Record] = 214;
        IslandRotateY[(int)Island.Option] = 284;
        for(int i = 0; i < islandParent.transform.childCount; i++)
            IslandEntry[i] = islandParent.transform.GetChild(i).gameObject;

        matR = 0.6f;
        matG = 0.4f;
        matB = 0.5f;
        leaf_material.color = new Color(matR, matG, matB);
        tmpTarget = null;
    }

    // Update is called once per frame
    void Update () {

        if (DataInfoManager.m_Instance.GetUserState() == DataInfoManager.UserState.UIOpen)
            return;

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

                if (IslandIndex > (int)Island.Option)
                    IslandIndex = (int)Island.Start;
                if (IslandIndex < (int)Island.Start)
                    IslandIndex = (int)Island.Option;
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

                TestChangeColor(TargetObj, DefaultColor.ContainsKey(TargetObj.name));
               
                if (!RollBack)
                {
                    if (TargetObj.transform.parent.name == "IslandParent")
                    {
                        tmpTarget = TargetObj;
                        CloseUpIsland();
                    }
                }
                else
                {
                    if (tmpTarget == TargetObj)
                    {
                        clickIsland(IslandIndex);
                    }
                    else
                        RollBackCamera();
                }
            }
            else
                RollBackCamera();
        }


    }
    private void clickIsland(int index)
    {
        switch(index)
        {
            case (int)Island.Start:
                outgameUI.ButtonEvent(IslandEntry[index]);
                SoundManager.m_Instance.PlaySound(SoundManager.SoundList.START_ENTER_PLAY);
                FXManager.m_Instance.PlayFX(FXManager.FXList.START_ENTER_PLAY, IslandEntry[index]);
                break;

            case (int)Island.Option:
                UIManager.m_Instance.OpenPopup(UIManager.PopupUI.OptionPopup);
                break;

            case (int)Island.Shop:
                UIManager.m_Instance.OpenPopup(UIManager.PopupUI.ShopUI);
                break;
        }
    }
    private void CloseUpIsland()
    {
        if (TargetObj != null) 
            if (TargetObj.name == IslandEntry[IslandIndex].name)
            {
                MoveCamera(TargetObj.transform.position);
                SoundManager.m_Instance.PlaySound(SoundManager.SoundList.START_CLICK_ISLAND);
                FXManager.m_Instance.PlayFX(FXManager.FXList.START_CLICK_ISLAND, IslandEntry[IslandIndex]);
                if(IslandIndex == 4)
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_DIE, IslandEntry[IslandIndex]);
            }
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
        iTween.MoveBy(gameObject, iTween.Hash("amount", new Vector3(pos.x, pos.y + YposOffset, pos.z + ZposOffset), 
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

        FXManager.m_Instance.StopFX(true);
        RollBack = false;
    }

    /// <summary>
    /// UI붙일때 코드 옴길꺼임 일단 테스트
    /// </summary>
    /// <param name="obj"></param>

    Dictionary<string, List<Color>> DefaultColor = new Dictionary<string, List<Color>>();



    private void TestChangeColor(GameObject obj, bool IsRollback)
    {

        foreach (SkinnedMeshRenderer mesh in obj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            if (mesh != null)
            {
                Material[] material = mesh.materials;
                if (!DefaultColor.ContainsKey(obj.name))
                {
                    DefaultColor.Add(obj.name, GetAllMaterialColor(material));
                    ChangeColor(material, new Color(144 / 255, 144 / 255, 144 / 255, 0));
                }
                else if (IsRollback)
                {
                    ChangeColor(material, DefaultColor[obj.name].ToArray());
                    DefaultColor.Remove(obj.name);
                }
                   
            }
        }
    }


    List<Color> GetAllMaterialColor(Material[] mat)
    {
        List<Color> colorList = new List<Color>();

        for (int i = 0; i < mat.Length; i++)
        {
            colorList.Add(mat[i].color);
        }

        return colorList;
    }

    void ChangeColor(Material[] mat, params Color[] color)
    {
        for (int i = 0; i < mat.Length; i++)
        {
            int colorIndex = i >= color.Length ? color.Length - 1 : i;
            mat[i].color = color[colorIndex];
        }
    }

}
