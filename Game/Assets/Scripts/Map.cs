using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    //const float MaxSpawnWidth = 3;
    //const float MinSpawnWidth = -3;
    //float IntervalSpawnWidth = 1.5f;
    //float IntervalSpawnHeight = 3f; // 동적생성 시 사용 변수

    public GameObject prefab1 = null;
    [HideInInspector]
    public float speed = .5f;
    List<GameObject> Obstacle = new List<GameObject>();

    public GameObject InitObstacle()
    {
        return Instantiate(prefab1, transform.position, transform.rotation);
    }

    // Use this for initialization
    void Start () {
        Obstacle.Add(InitObstacle());
    }

    // Update is called once per frame
    void Update () {
        for(int i = 0; i < Obstacle.Count; i++)
        {
            Obstacle[i].transform.Translate(Vector3.up * speed);
            if (Obstacle[i].transform.GetChild(Obstacle[i].transform.childCount - 1).position.y > transform.position.y && Obstacle[i].tag != "Finish")
            {
                Obstacle.Add(InitObstacle());
                Obstacle[i].tag = "Finish";
            }
            if (Obstacle[i].transform.GetChild(Obstacle[i].transform.childCount - 1).position.y > 10)
            {
                Destroy(Obstacle[i]);
                Obstacle.Remove(Obstacle[i]);
            }
        }


    }
}
