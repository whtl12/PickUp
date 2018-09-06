using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    //public const float MaxHorizontal = 3.8f;
    Vector3 MapOffset;
    DataInfoManager dataManager;
    PlayManager playManager;
    CharacterData characterData;

    float hSpeed;
    float vSpeed;
    float SpeedUpValue;
    Vector3 SizeUpValue;
    Vector3 MinSize, MaxSize;
    GameObject character;
    [HideInInspector] public int AteNum = 0;
    [HideInInspector] public int playerIndex = 0;
    bool direction = false;
    float directionRotate = 0;
    float closeupSize = 0;

    private void Awake()
    {
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
    }
    void Start () {
        characterData = dataManager.GetCharacterData(0);
        character = characterData.CharName;
        hSpeed = characterData.hSpeed;
        vSpeed = characterData.vSpeed;
        SizeUpValue = characterData.SizeUpValue;
        SpeedUpValue = characterData.SpeedUpValue;
        AteNum = 0;

        MapOffset = new Vector3(0, 0, MapManager.BACKGROUND_Z);
        
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(directionRotate, 0, 0)) * hSpeed * Time.deltaTime);
    }
    void Update () {
        if (direction)
        {
            if (directionRotate > -1)
                directionRotate -= 0.05f;
        }
        else
        {
            if (directionRotate < 1)
                directionRotate += 0.05f;
        }

        Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.parent.position - transform.position);
        transform.rotation = rotation * Quaternion.Euler(0, -directionRotate * 30,0);
        

        if (GetComponent<Rigidbody>().velocity.magnitude > vSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * vSpeed;

        if (Input.GetMouseButtonDown(0))
            direction = !direction;

        GetComponentInChildren<Animator>().speed = GetComponent<Rigidbody>().velocity.magnitude / 8;
        if (closeupSize > GetComponent<Rigidbody>().velocity.magnitude / 4)
            closeupSize -= 0.2f;
        if (closeupSize <= GetComponent<Rigidbody>().velocity.magnitude / 4)
            closeupSize += 0.02f;

        Camera.main.transform.localPosition = new Vector3(playManager.cameraBasicPosition.x,
                                                            playManager.cameraBasicPosition.y + closeupSize / 5,
                                                            playManager.cameraBasicPosition.z - closeupSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_CRASH, collision.gameObject);
        }
    }
    public void EatBubble()
    {
        if (AteNum < 10)
        {
            AteNum++;
            transform.localScale += SizeUpValue;
            transform.GetComponentInChildren<CapsuleCollider>().radius += 0.025f;
            //transform.GetChild(1).localScale = transform.localScale / 2;
        }
        if (AteNum < 30)
        {
            vSpeed += SpeedUpValue;
            hSpeed += SpeedUpValue / 2;
        }
    }
    IEnumerator ObstacleHit()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        for (int i = 0; i < 2; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.4f);
        }
        GetComponent<CapsuleCollider>().enabled = true;

        yield return null;
    }
}
