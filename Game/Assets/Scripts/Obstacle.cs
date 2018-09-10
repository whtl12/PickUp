using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
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
}
