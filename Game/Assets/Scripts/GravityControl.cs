using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour {
    private Transform core;
    private static float gravityForce = -9.8f;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //GetComponent<Rigidbody>().useGravity = false;
        core = Camera.main.transform.parent;
    }
	
	// Update is called once per frame
	void Update () {
        Attract(gameObject.transform);		
	}
    private void Attract(Transform body)
    {
        Vector3 gravityUp = (body.position - core.position).normalized;
        Vector3 bodyUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravityUp * gravityForce);
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
