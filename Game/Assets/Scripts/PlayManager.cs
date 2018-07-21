using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {
    DataInfoManager dataManager;
    MapManager mapManager;
    CharacterData characterData;

    bool direction;
    public float vSpeed;
    float mapHeight;
    public GameObject mainPlayer;
    Vector3 cameraBasicPosition = new Vector3(0, 3f, -10f);
    [HideInInspector] public List<GameObject> Player = new List<GameObject>();
    [HideInInspector] public GameObject mapPref;
    [HideInInspector] public bool isGround;
    [HideInInspector] public int index;

    GameObject playerParent;
    GameObject mapParent;
    List<GameObject> mapList;

    // Use this for initialization
    void Start()
    {
        Player.Add(GameObject.Find("Player"));
        playerParent = GameObject.Find("PlayerParent");
        mapParent = GameObject.Find("MapParent");
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        mapManager = GetComponent<MapManager>();
        characterData = dataManager.GetCharacterData(0);
        vSpeed = characterData.vSpeed;
        direction = false;
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
            Camera.main.gameObject.transform.position = new Vector3(mainPlayer.transform.position.x * 2, cameraBasicPosition.y + mainPlayer.transform.position.y, cameraBasicPosition.z);
            Camera.main.gameObject.transform.rotation = Quaternion.Euler(0, - 20 * mainPlayer.transform.position.x / CharacterControl.MaxHorizontal, 0);

        }

        if (mapManager.GetMidPosition().y > mainPlayer.transform.position.y)
            mapManager.AllRun();

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
    public bool Getdirection()
    {
        return direction;
    }
}
