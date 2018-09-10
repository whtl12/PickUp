using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	void Start () {
        Collider[] collider = GetComponentsInChildren<Collider>();
        foreach(Collider obj in collider)
        {
            obj.gameObject.AddComponent<ObstacleCollider>();
        }
    }
}
