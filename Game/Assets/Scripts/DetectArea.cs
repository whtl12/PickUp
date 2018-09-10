using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour {
    [SerializeField] CharacterControl pCharCntl;
    int iblue, ired, igreen, iwhite, iblack;

    Transform pTransform;
    // Use this for initialization
    void Start () {
        pTransform = transform.parent;
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
                    InGameUI.m_Instance.SetText(other.gameObject, iblue.ToString());
                    break;
                case (int)MapManager.Item.Red:
                    ired++;
                    InGameUI.m_Instance.SetText(other.gameObject, ired.ToString());
                    break;
                case (int)MapManager.Item.Green:
                    igreen++;
                    InGameUI.m_Instance.SetText(other.gameObject, igreen.ToString());
                    break;
                case (int)MapManager.Item.White:
                    iwhite++;
                    InGameUI.m_Instance.SetText(other.gameObject, iwhite.ToString());
                    break;
                case (int)MapManager.Item.Black:
                    iblack++;
                    InGameUI.m_Instance.SetText(other.gameObject, iblack.ToString());
                    break;
            }

            int index = MapManager.m_Instance.WaterList.FindIndex(item => item.transform == other.transform);
            if (index > -1)
            {
                MapManager.m_Instance.pushWater(index);
            }

            pCharCntl.EatBubble();
            FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT, transform.parent.gameObject);
            SoundManager.m_Instance.PlaySound(SoundManager.SoundList.PLAY_EAT);
        }
    }
    private void bubbleEvent(Object obj)
    {
        GameObject water = obj as GameObject;
        water.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().radius += 0.05f;
    }
}
