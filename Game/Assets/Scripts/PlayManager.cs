using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {
    public static PlayManager m_Instance;

    public InGameUI ingameUI;
    public GameObject mainPlayer;
    public Vector3 cameraBasicPosition;
    Transform cameraParent;

    // Use this for initialization
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }
    void Start()
    {

        cameraParent = Camera.main.transform.parent;
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
            cameraParent.position = new Vector3(0, mainPlayer.transform.position.y, 10);
            Quaternion rotation = Quaternion.LookRotation(cameraParent.position - mainPlayer.transform.position);
            cameraParent.rotation = rotation;
        }

        if (MapManager.m_Instance.GetMidPosition().y > mainPlayer.transform.position.y)
            MapManager.m_Instance.AllRun();

    }
}
