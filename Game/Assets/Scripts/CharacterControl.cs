using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    public CharacterData characterData;

    float hSpeed;
    float vSpeed;
    float SpeedUpValue;
    Vector3 SizeUpValue;
    //Vector3 MinSize, MaxSize;
    //Vector3 MapOffset;
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
        //MapOffset = new Vector3(0, 0, MapManager.BACKGROUND_Z);
        
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.TransformDirection(new Vector3(directionRotate, 0, 0)) * hSpeed * Time.deltaTime);

    }
    void Update () {
        #region 캐릭터 Roll축 회전
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
        transform.rotation = rotation * Quaternion.Euler(0, -directionRotate * 30, 0);

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > vSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * vSpeed;
        #endregion

        // 캐릭터 방향
        if (Input.GetMouseButtonDown(0))
            direction = !direction;

        // 카메라 거리
        GetComponentInChildren<Animator>().speed = Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.125f;
        if (closeupSize > Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.25f)
            closeupSize -= 0.2f;
        if (closeupSize <= Mathf.Abs(GetComponent<Rigidbody>().velocity.y) * 0.25f)
            closeupSize += 0.02f;

        Camera.main.transform.localPosition = PlayManager.m_Instance.cameraBasicPosition + new Vector3(0, closeupSize * 0.2f, -closeupSize);

        // 속도에 따른 체력 소모
        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) > characterData.vSpeed * 2)
        {
            PlayManager.m_Instance.HP += (characterData.vSpeed * 2 - Mathf.Abs(GetComponent<Rigidbody>().velocity.y)) * 0.2f * Time.deltaTime;
            InGameUI.m_Instance.SetValue("sldHPbar", PlayManager.m_Instance.HP);
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
}
