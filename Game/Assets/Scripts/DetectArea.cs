using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour {
    Map map;
    float hSpeed;
    // Use this for initialization
    void Start () {
        map = GetComponentInParent<CharacterControl>().map;
        hSpeed = GetComponentInParent<CharacterControl>().hSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trickle")
        {
            Destroy(other.gameObject);
            if (map.speed < 0.15f)
            {
                transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
                map.ChangeSpeed(0.01f);
                hSpeed += 0.2f;
            }
        }
    }
}
