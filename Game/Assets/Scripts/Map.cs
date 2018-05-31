using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    //const float MaxSpawnWidth = 3;
    //const float MinSpawnWidth = -3;
    //float IntervalSpawnWidth = 1.5f;
    //float IntervalSpawnHeight = 3f; // 동적생성 시 사용 변수

    public GameObject prefab1;
    public GameObject BGpref;
    //[HideInInspector]
    public float speed;
    List<GameObject> Obstacle = new List<GameObject>();
    List<GameObject> Background = new List<GameObject>();

    public GameObject InitObstacle()
    {
        return Instantiate(prefab1, transform.position, transform.rotation);
    }
    public GameObject InitBackground(int init = -30)
    {
        GameObject instance = Instantiate(BGpref, new Vector3(0, init), Quaternion.Euler(0, 180, 0));
        instance.transform.localScale = new Vector3(1, 2, 1);
        return instance;
    }

    // Use this for initialization
    void Start () {
        Obstacle.Add(InitObstacle());
        Background.Add(InitBackground(0));
        speed = 0.05f;
    }

    // Update is called once per frame
    void Update () {
        for(int i = 0; i < Background.Count; i++)
        {
            Background[i].transform.Translate(Vector3.up * speed);
            if (Background[i].transform.position.y > 1 && Background[i].tag != "Finish")
            {
                Background.Add(InitBackground());
                Background[i].tag = "Finish";
            }
            if (Background[i].transform.position.y > 30)
            {
                Destroy(Background[i]);
                Background.Remove(Background[i]);
            }
        }
        for (int i = 0; i < Obstacle.Count; i++)
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
    public void ChangeSpeed(float value)
    {
        speed += value;
    }
}
