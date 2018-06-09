using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour {
    CharacterControl parent;
    // Use this for initialization
    void Start () {
        parent = GetComponentInParent<CharacterControl>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trickle")
        {
            other.gameObject.SetActive(false);
            parent.EatBubble();
        }
    }
}
