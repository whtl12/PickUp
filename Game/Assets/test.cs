using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public GameObject playerPref;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 edge = Vector3.zero;
        Vector3 normalized = (transform.position - collision.transform.position).normalized;
        print(90 - Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg);
        //print(normalized.magnitude);
        //print(normalized);
        //print((transform.position - collision.transform.position));
        foreach (ContactPoint contact in collision.contacts)
            edge += contact.point;
        edge /= collision.contacts.Length;
        edge = transform.position - edge;
        print(edge);
        print(transform.position);
        print(transform.position - Vector3.right * edge.x);

        if (Mathf.Abs(edge.x) < 0.3)
        {
            //5:5
            Destroy(gameObject);
            Instantiate(playerPref, transform.position, Quaternion.identity);
            Instantiate(playerPref, transform.position - Vector3.right * edge.x * 2, Quaternion.identity);
        }
    }
}
