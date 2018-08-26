using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {
    DataInfoManager dataManager;
    MapManager mapManager;

    float mapHeight;
    public GameObject mainPlayer;
    Vector3 cameraBasicPosition = new Vector3(0, 3f, -8f);
    [HideInInspector] public List<GameObject> Player = new List<GameObject>();
    [HideInInspector] public GameObject mapPref;
    [HideInInspector] public bool isGround;
    [HideInInspector] public int index;

    GameObject playerParent;
    GameObject mapParent;
    GameObject cameraParent;
    List<GameObject> mapList;

    // Use this for initialization
    void Start()
    {
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        Player.Add(GameObject.Find("Player"));
        playerParent = GameObject.Find("PlayerParent");
        mapParent = GameObject.Find("MapParent");
        cameraParent = GameObject.Find("CameraParent");
        mapManager = GetComponent<MapManager>();

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
            //Camera.main.gameObject.transform.rotation = Quaternion.FromToRotation(mainPlayer.transform.position, new Vector3(0, mainPlayer.transform.position.y, 10)); // Quaternion.Euler(30, - 20 * mainPlayer.transform.position.x / CharacterControl.MaxHorizontal, 0);
            Quaternion rotation = Quaternion.LookRotation(cameraParent.transform.position - mainPlayer.transform.position);
            // cameraParent.transform.rotation = Quaternion.Slerp(cameraParent.transform.rotation, rotation, Time.deltaTime * 0.2f);
            cameraParent.transform.rotation = rotation;
        }

        if (mapManager.GetMidPosition().y > mainPlayer.transform.position.y)
            mapManager.AllRun();

    }
    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
}
