using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    MapInfo mapInfo;
    public GameObject BGpref;
    public bool isGround { get; private set; }
    public int index { get; private set; }
    List<GameObject> Obstacle = new List<GameObject>();
    List<GameObject> Background = new List<GameObject>();

    //public GameObject InitObstacle()
    //{
    //    return Instantiate(prefab, transform.position, transform.rotation);
    //}
    public void InitBackground()
    {
        GameObject instance = Instantiate(BGpref, new Vector3(0, -mapInfo.dumiHeight * index++, 1), Quaternion.Euler(0, 180, 0));
        instance.transform.localScale = new Vector3(1, 4, 1);
        Background.Add(instance);
    }
    private void Awake()
    {
        mapInfo = new MapInfo();
    }
    // Use this for initialization
    void Start () {

        //Obstacle.Add(InitObstacle());
        index = 0;
        InitBackground();
        InitBackground();
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGround = false;
    }
    // Update is called once per frame
    void Update () {

    }
}
