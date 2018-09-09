using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    CharacterData characterData;

    float hSpeed;
    float vSpeed;
    float SpeedUpValue;
    Vector3 SizeUpValue;
    //Vector3 MinSize, MaxSize;
    Vector3 MapOffset;
    [HideInInspector] public int AteNum = 0;
    [HideInInspector] public int playerIndex = 0;

    bool direction = false;
    float directionRotate = 0;
    float closeupSize = 0;

    void Start () {
        characterData = DataInfoManager.m_Instance.GetCharacterData(0);
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
                directionRotate -= 0.0125f * hSpeed;
        }
        else
        {
            if (directionRotate < 1)
                directionRotate += 0.0125f * hSpeed;
        }

        Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.parent.position - transform.position);
        transform.rotation = rotation * Quaternion.Euler(0, -directionRotate * 30,0);
        

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > vSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * vSpeed;

        if (Input.GetMouseButtonDown(0))
            direction = !direction;

        GetComponentInChildren<Animator>().speed = Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.125f;
        if (closeupSize > Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.25f)
            closeupSize -= 0.2f;
        if (closeupSize <= Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.25f)
            closeupSize += 0.02f;

        Camera.main.transform.localPosition = PlayManager.m_Instance.cameraBasicPosition + new Vector3(0, closeupSize * 0.2f, - closeupSize);

        if(Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > characterData.vSpeed * 2)
        {
            PlayManager.m_Instance.HP += (characterData.vSpeed * 2 - Mathf.Abs(GetComponent<Rigidbody>().velocity.y)) * 0.2f * Time.deltaTime;
            InGameUI.m_Instance.SetValue("sldHPbar", PlayManager.m_Instance.HP);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.parent.parent.name == "Obstacle")
        {
            if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > characterData.vSpeed)
            {
                print(GetComponent<Rigidbody>().velocity);
                StartCoroutine(ObstacleHit());
                PlayManager.m_Instance.HP += characterData.vSpeed * 0.5f - Mathf.Abs(GetComponent<Rigidbody>().velocity.y);
                InGameUI.m_Instance.SetValue("sldHPbar", PlayManager.m_Instance.HP);
                FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_CRASH, collision.gameObject);
            }
        }
    }
    public void EatBubble()
    {
        if (AteNum < 50)
        {
            AteNum++;

            if (AteNum < 10)
            {
                transform.localScale += SizeUpValue;
                transform.GetComponentInChildren<CapsuleCollider>().radius += 0.025f;
            }

            vSpeed += SpeedUpValue;
            hSpeed += SpeedUpValue * 0.5f;
        }
    }
    IEnumerator ObstacleHit()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        //for (int i = 0; i < 2; i++)
        //{
        //    GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        //    yield return new WaitForSeconds(0.2f);
        //    GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        //    yield return new WaitForSeconds(0.4f);
        //}

        yield return new WaitForSeconds(1.5f);

        GetComponent<CapsuleCollider>().enabled = true;

        yield return null;
    }
}
