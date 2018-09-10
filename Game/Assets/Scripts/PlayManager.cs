﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour {
    public static PlayManager m_Instance;

    public GameObject mainPlayer;
    public Vector3 cameraBasicPosition;
    private Transform cameraParent;
    public float HP;
    public bool paused;

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
        HP = 1;// DataInfoManager.m_Instance.GetCharacterData(0).MaxHP;
        paused = false;
        InGameUI.m_Instance.sldHP.value = HP;
        cameraParent = Camera.main.transform.parent;
        cameraBasicPosition = new Vector3(0, 4f, -20f);
        mainPlayer = GameObject.Find("PlayerParent");
    }
    // Update is called once per frame
    void Update()
    {
        //if (mainPlayer == null)
        //{
        //    if (GameObject.Find("Player") == null)
        //    {
        //        if (GameObject.Find("Player(Clone)") != null)
        //            GameObject.Find("Player(Clone)").name = "Player";
        //        else
        //            UIManager.m_Instance.ChangeStage(UIManager.StageUI.OutGameUI);
        //    }
        //    mainPlayer = GameObject.Find("Player");
        //}
        //else
        {
            cameraParent.position = new Vector3(0, mainPlayer.transform.position.y, 10);
            Quaternion rotation = Quaternion.LookRotation(cameraParent.position - mainPlayer.transform.position);
            cameraParent.rotation = rotation;
        }

        if (MapManager.m_Instance.GetMidPosition().y > mainPlayer.transform.position.y)
            MapManager.m_Instance.AllRun();

        if (HP <= 0 && !paused)
            HPzero();
    }
    public void HPzero()
    {
        paused = true;
        mainPlayer.GetComponent<Rigidbody>().useGravity = false;
        mainPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        InGameUI.m_Instance.SetActive(InGameUI.m_Instance.panelGameOver, true);
    }
}
