using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    public const float MaxHorizontal = 3.8f;
    Vector3 MapOffset;

    DataInfoManager dataManager;
    PlayManager playManager;
    CharacterData characterData;

    float hSpeed;
    Vector3 SizeUpValue;
    Vector3 MinSize, MaxSize;
    GameObject character;

    bool isGround;
    [HideInInspector] public int AteNum = 0;
    [HideInInspector] public int playerIndex = 0;

    private void Awake()
    {
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
    }
    void Start () {
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        characterData = dataManager.GetCharacterData(0);
        hSpeed = characterData.hSpeed;
        SizeUpValue = characterData.SizeUpValue;
        MinSize = characterData.MinSize;
        MaxSize = characterData.MaxSize;
        character = characterData.CharName;
        AteNum = 0;
        isGround = false;

        MapOffset = new Vector3(0, 0, MapManager.BACKGROUND_Z);
        
        transform.GetComponentInChildren<SphereCollider>().radius = 0.8f + AteNum * 0.1f;
    }

    void Update () {
        Vector3 direction = Time.deltaTime * hSpeed * new Vector3((playManager.Getdirection() ? 1 : -1), 0, -1);
        transform.Translate(direction);

        //Quaternion rotation = Quaternion.Euler(-90, (playManager.Getdirection() ? 45 : -45), 0);
        Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.parent.position - transform.position);
        //transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, rotation, Time.deltaTime * 0.2f);
        transform.rotation = rotation * Quaternion.Euler(0, (playManager.Getdirection() ? -1 : 1) * 30,0);
        

        //if (Mathf.Abs(transform.position.x) > MaxHorizontal)
        //    transform.position = new Vector3(transform.position.x / Mathf.Abs(transform.position.x) * MaxHorizontal, transform.position.y, transform.position.z);

        if (GetComponent<Rigidbody>().velocity.magnitude > playManager.vSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * playManager.vSpeed;

        //if (transform.position.z > 0.8f)
        //    transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 1f))
        {
            if(hitInfo.collider.tag == "Background")
                isGround = true;
        } else
        {
            isGround = false;
        }

        if (!isGround)
            GetComponent<Rigidbody>().AddForce(- 2f * new Vector3(transform.position.x, 0, transform.position.z - MapOffset.z));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            collision.transform.tag = "Finish";
            if (AteNum > 0)
            {
                AteNum--;
                transform.localScale -= SizeUpValue;
                transform.GetComponentInChildren<SphereCollider>().radius -= 0.025f;
                transform.GetChild(1).localScale = transform.localScale / 2;
                //Vector3 normalized = (transform.position - collision.transform.position).normalized;
                //float Deg = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;
                Vector3 edge = Vector3.zero;
                foreach (ContactPoint contact in collision.contacts)
                    edge += contact.point;
                edge /= collision.contacts.Length;
                edge = transform.position - edge;
                int devAte = 0;
                if (Mathf.Abs(edge.x) < 0.3 && playManager.Player.Count < 5)
                {
                    if (Mathf.Abs(edge.x) < 0.1)
                        devAte = AteNum / 2;
                    else if (Mathf.Abs(edge.x) < 0.2)
                        devAte = AteNum / 4;
                    else
                        devAte = 1;

                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    playManager.InitPlayer(devAte, character, transform.position - Vector3.right * edge.x * 2, Quaternion.identity);
                    AteNum -= devAte;
                }
            }
                else
                    playManager.DestroyPlayer(gameObject);
        }
    }
    public void EatBubble()
    {
        playManager.vSpeed += 0.3f;
        if (AteNum < 10)
        {
            AteNum++;
            transform.localScale += SizeUpValue;
            transform.GetComponentInChildren<SphereCollider>().radius += 0.025f;
            transform.GetChild(1).localScale = transform.localScale / 2;
        }
    }
    //IEnumerator Revert()
    //{
    //    yield return new WaitForSeconds(1.0f);
    //    float distance = (Camera.main.gameObject.transform.position - cameraBasicPosition).magnitude;
    //    if (distance > 1)
    //        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("position", cameraBasicPosition, "time", 1f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInOutQuart));
    //}
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
