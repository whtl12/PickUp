using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    float h, v;
    float hSpeed;
    public Map map;
	// Use this for initialization
	void Start () {
        h = v = 0;
        hSpeed = 0.5f;
        GetComponent<Rigidbody>().maxDepenetrationVelocity = 3;
    }

    // Update is called once per frame
    void Update () {
        h = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().AddForce(new Vector3(h * hSpeed, v));
        //if (transform.position.x > 3 || transform.position.x < -3)
        //    GetComponent<Rigidbody>().velocity = new Vector3();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            if(map.speed > 0.03f)
            {
                map.ChangeSpeed(-0.01f);
                transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
            }

            iTween.MoveAdd(collision.transform.parent.gameObject, Vector3.down * hSpeed * 10, 1);
            StartCoroutine(ObstacleHit());
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "trickle")
        {
            Destroy(other.gameObject);
            if (map.speed < 0.13f)
            {
                transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
                map.ChangeSpeed(0.01f);
            }
        }
    }
    IEnumerator ObstacleHit()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        for(int i = 0; i < 2; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        GetComponent<CapsuleCollider>().enabled = true;



        yield return null;
    }
}
