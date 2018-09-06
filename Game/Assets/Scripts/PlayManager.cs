using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {
    DataInfoManager dataManager;
    MapManager mapManager;

    float mapHeight;
    public GameObject mainPlayer;
    [HideInInspector] public Vector3 cameraBasicPosition;
    [HideInInspector] public List<GameObject> Player;
    [HideInInspector] public GameObject mapPref;
    [HideInInspector] public int index;


    GameObject playerParent;
    GameObject mapParent;
    GameObject cameraParent;
    List<GameObject> mapList;

    // Use this for initialization
    void Start()
    {
        Player = new List<GameObject>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        Player.Add(GameObject.Find("Player"));
        playerParent = GameObject.Find("PlayerParent");
        mapParent = GameObject.Find("MapParent");
        cameraParent = GameObject.Find("CameraParent");
        mapManager = GetComponent<MapManager>();
        cameraBasicPosition = new Vector3(0, 4f, -20f);
    }
    // Update is called once per frame
    void Update()
    {
        if (mainPlayer == null)
        {
            if (GameObject.Find("Player") == null)
            {
                if (GameObject.Find("Player(Clone)") != null)
                    GameObject.Find("Player(Clone)").name = "Player";
                else
                    UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
            }
            mainPlayer = GameObject.Find("Player");
        }
        else
        {
            cameraParent.transform.position = new Vector3(0, mainPlayer.transform.position.y, 10);
            Quaternion rotation = Quaternion.LookRotation(cameraParent.transform.position - mainPlayer.transform.position);
            cameraParent.transform.rotation = rotation;
        }

        if (mapManager.GetMidPosition().y > mainPlayer.transform.position.y)
            mapManager.AllRun();

    }
}
