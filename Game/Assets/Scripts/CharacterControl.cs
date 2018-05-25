using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    float h, v;
    float hSpeed;
	// Use this for initialization
	void Start () {
        h = v = 0;
        hSpeed = 0.5f;
        GetComponent<Rigidbody>().maxDepenetrationVelocity = 3;
    }

    // Update is called once per frame
    void Update () {
        h = Input.GetAxis("Horizontal");
        //GetComponent<Rigidbody>().velocity += new Vector3(h * hSpeed, v);
        GetComponent<Rigidbody>().AddForce(new Vector3(h * hSpeed, v));
        if (transform.position.x > 3 || transform.position.x < -3)
            GetComponent<Rigidbody>().velocity = new Vector3();
    }

    private void OnCollisionEnter(Collision collision)
    {
        iTween.MoveTo(collision.transform.parent.gameObject, Vector3.down / 5, 1);

        //iTween.MoveBy(gameObject, Vector3.down / 5, 1);
        //iTween.MoveFrom(gameObject, Vector3.down / 5, 1);
        //iTween.MoveAdd(gameObject, Vector3.down / 5, 1);
    }
}
