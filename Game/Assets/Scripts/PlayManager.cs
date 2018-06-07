using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {
    DataInfoManager dataManager;
    CharacterData characterData;
    MapData mapData;

    bool direction;
    public float vSpeed;
    float mapHeight;
    GameObject mainPlayer;
    Vector3 cameraBasicPosition = new Vector3(0, -3.5f, -12f);
    List<GameObject> Background = new List<GameObject>();
    [HideInInspector] public List<GameObject> Player = new List<GameObject>();
    [HideInInspector] public GameObject mapPref;
    [HideInInspector] public bool isGround;
    [HideInInspector] public int index;

    public GameObject playerParent;
    public GameObject mapParent;

    // Use this for initialization
    void Start()
    {
        Player.Add(GameObject.Find("Player"));
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        characterData = dataManager.GetCharacterData(0);
        mapData = dataManager.GetMapData(0);
        vSpeed = characterData.vSpeed;
        mapHeight = mapData.mapHeight;
        mapPref = mapData.Name;
        index = 0;
        direction = false;

        InitBackground();
        InitBackground();
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
            Camera.main.gameObject.transform.position = new Vector3(0, cameraBasicPosition.y + mainPlayer.transform.position.y, cameraBasicPosition.z);
            if (mainPlayer.transform.position.y < (index - 1) * -mapHeight)
                InitBackground();
        }

        if (Input.GetMouseButtonDown(0))
            direction = !direction;

    }
    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
    public void InitPlayer(int ate, GameObject obj, Vector3 position, Quaternion quaternion)
    {
        GameObject _player = Instantiate(obj, position, quaternion);
        _player.GetComponent<CharacterControl>().AteNum = ate;
        _player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        _player.transform.parent = playerParent.transform;
        Player.Add(_player);
    }
    public void DestroyPlayer(GameObject obj)
    {
        if (obj == mainPlayer)
            if (Player.Count > 0)
                Player[0].name = "Player";
            else
                Application.Quit();
        try
        {
            Player.RemoveAt(Player.IndexOf(obj));
        }
        catch
        {
            print(string.Format("index error : Player.IndexOf {0}", Player.IndexOf(obj)));
        }
        Destroy(obj);
    }
    public void InitBackground()
    {
        GameObject instance = Instantiate(mapPref, new Vector3(0, -mapHeight * index++, 1), Quaternion.Euler(0, 180, 0));
        instance.transform.localScale = new Vector3(1, 4, 1);
        instance.transform.parent = mapParent.transform;
        Background.Add(instance);
    }
    public bool Getdirection()
    {
        return direction;
    }
}
