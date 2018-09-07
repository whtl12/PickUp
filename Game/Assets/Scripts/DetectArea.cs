using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectArea : MonoBehaviour {
    [SerializeField]CharacterControl pCharCntl;
    [SerializeField] MapManager mapManager;
    [SerializeField] private InGameUI ingameUI;

    Transform pTransform;
    // Use this for initialization
    void Start () {
        pTransform = transform.parent;
        pCharCntl = GetComponentInParent<CharacterControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.name == "Water")
        {
            ingameUI.SetText(other.gameObject);
            int index = mapManager.WaterList.FindIndex(item => item.transform == other.transform);
            if (index > -1)
            {
                mapManager.pushWater(index);
            }
            pCharCntl.EatBubble();
            FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_EAT, transform.parent.gameObject);
            SoundManager.m_Instance.PlaySound(SoundManager.SoundList.PLAY_EAT);
            //iTween.MoveTo(other.gameObject, iTween.Hash("position", other.transform.position + new Vector3(3, 1, 0),
            //                                            "time", Vector3.Distance(other.transform.position, pTransform.position),
            //                                            "easetype", iTween.EaseType.easeInOutBack,
            //                                            "oncomplete", "bubbleEvent",
            //                                            "oncompletetarget", gameObject,
            //                                            "oncompleteparams", other.gameObject as Object));
        }
    }
    private void bubbleEvent(Object obj)
    {
        GameObject water = obj as GameObject;
        water.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().radius += 0.05f;
    }
}
