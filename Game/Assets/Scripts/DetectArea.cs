using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour {
    [SerializeField] CharacterControl pCharCntl;
    public int iblue, ired, igreen, iwhite, iblack;

    void Start () {
        pCharCntl = GetComponentInParent<CharacterControl>();
        iblue = ired = igreen = iwhite = iblack = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Water")
        {
            switch(other.gameObject.name[12] - 49)
            {
                case (int)MapManager.Item.Blue:
                    iblue++;
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT_BLUE, transform.parent.gameObject);
                    InGameUI.m_Instance.SetText(other.gameObject, iblue.ToString());
                    break;
                case (int)MapManager.Item.Red:
                    ired++;
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT_RED, transform.parent.gameObject);
                    InGameUI.m_Instance.SetText(other.gameObject, ired.ToString());
                    break;
                case (int)MapManager.Item.Green:
                    igreen++;
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT_GREEN, transform.parent.gameObject);
                    InGameUI.m_Instance.SetText(other.gameObject, igreen.ToString());
                    break;
                case (int)MapManager.Item.White:
                    iwhite++;
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT_WHITE, transform.parent.gameObject);
                    InGameUI.m_Instance.SetText(other.gameObject, iwhite.ToString());
                    break;
                case (int)MapManager.Item.Black:
                    iblack++;
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT_BLACK, transform.parent.gameObject);
                    InGameUI.m_Instance.SetText(other.gameObject, iblack.ToString());
                    break;
            }

            int index = MapManager.m_Instance.WaterList.FindIndex(item => item.transform == other.transform);
            if (index > -1)
            {
                MapManager.m_Instance.pushWater(index);
            }

            pCharCntl.EatBubble();
            SoundManager.m_Instance.PlaySound(SoundManager.SoundList.PLAY_EAT);
        }
    }
    private void bubbleEvent(Object obj)
    {
        GameObject water = obj as GameObject;
        water.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().radius += 0.05f;
    }
    public void calcWater()
    {
        InGameUI.m_Instance.SetText(InGameUI.m_Instance.txtCalc.gameObject, "Blue : " + iblue + "\nRed : " + ired + "\nGreen : " + igreen + "\nWhite : " + iwhite + "\nBlack : " + iblack);

        int tmpblue = EncryptValue.GetInt("waterblue");
        EncryptValue.SetInt("waterblue", tmpblue + iblue);
        print(EncryptValue.GetInt("waterblue"));
        int tmpred = EncryptValue.GetInt("waterred");
        EncryptValue.SetInt("waterred", tmpred + ired);

        int tmpgreen = EncryptValue.GetInt("watergreen");
        EncryptValue.SetInt("watergreen", tmpgreen + igreen);

        int tmpwhite = EncryptValue.GetInt("waterwhite");
        EncryptValue.SetInt("waterwhite", tmpwhite + iwhite);

        int tmpblack = EncryptValue.GetInt("waterblack");
        EncryptValue.SetInt("waterblack", tmpblack + iblack);
    }
}
