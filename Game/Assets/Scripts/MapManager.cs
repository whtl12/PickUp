using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public const int BACKGROUND_Z = 10;
    private const float OBSTACLEINTERVAL = 6;
    private const float WATERINTERVAL = 8;

    private const int MAPCOUNT = 4;
    private const int WATERCOUNT = 3;
    private const int ROCKCOUNT = 5;
    private const int TREECOUNT = 3;
    private const int LEEFCOUNT = 2;
    private const int PLANTCOUNT = 3;

    private const int MAPSTART = 0;
    private const int WATERSTART = 100;
    private const int ROCKSTART = 1000;
    private const int TREESTART = 1100;
    private const int LEEFSTART = 1200;
    private const int PLANTSTART = 1300;

    List<MapData> map = new List<MapData>();
    List<MapData> water = new List<MapData>();
    List<MapData> rock = new List<MapData>();
    List<MapData> tree = new List<MapData>();
    List<MapData> leef = new List<MapData>();
    List<MapData> plant = new List<MapData>();

    float obstacle_y;
    float water_y;
    int mapIndex;

    DataInfoManager dataManager;
    List<GameObject> backgroundList = new List<GameObject>();
    List<GameObject> WaterList = new List<GameObject>();
    List<GameObject> ObstacleList = new List<GameObject>();

    ObjectPool mapPool = new ObjectPool();
    ObjectPool obsPool = new ObjectPool();
    ObjectPool waterPool = new ObjectPool();

    GameObject mapParent;
    Transform bgParent;
    Transform wtParent;
    Transform obParent;
    Quaternion bidoQuaternion;

    enum ObjectElement
    {
        Background = 0,
        Water,
        Obstacle,
        MAX
    };
    enum Obstacle
    {
        Rock = 0,
        Tree,
        Leef,
        Plant,
        MAX
    }
    enum Item
    {
        Red = 0,
        Green,
        Blue,
        MAX
    }

    void Start () {
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        mapParent = GameObject.Find("MapParent");
        bgParent = GameObject.Find("Background").transform;
        obParent = GameObject.Find("Obstacle").transform;
        wtParent = GameObject.Find("Water").transform;
        mapIndex = 0;
        obstacle_y = 0;
        water_y = 0;
        bidoQuaternion = Quaternion.Euler(-90, 0, 0);

        InitMap(map, ObjectElement.Background, MAPSTART, MAPCOUNT);
        InitMap(water, ObjectElement.Water, WATERSTART, WATERCOUNT);
        InitMap(rock, ObjectElement.Obstacle, ROCKSTART, ROCKCOUNT);
        InitMap(tree, ObjectElement.Obstacle, TREESTART, TREECOUNT);
        InitMap(leef, ObjectElement.Obstacle, LEEFSTART, LEEFCOUNT);
        InitMap(plant, ObjectElement.Obstacle, PLANTSTART, PLANTCOUNT);

        // init map
        while (true)
        {
            popBackground();
            if (backgroundList.Count >= 7)
                break;
        }

        // init obstacle
        while(true)
        {
            switch (Random.Range(0, (int)Obstacle.MAX))
            {
                case (int)Obstacle.Rock:
                    InitObstacle(rock);
                    break;
                case (int)Obstacle.Tree:
                    InitObstacle(tree);
                    break;
                case (int)Obstacle.Leef:
                    InitObstacle(leef);
                    break;
                case (int)Obstacle.Plant:
                    InitObstacle(plant);
                    break;
            }
            if (ObstacleList.Count > 30)
                break;
        }
        while (true)
        {
            InitWater();
            if (WaterList.Count > 15)
                break;
        }
    }

    public Vector3 GetMidPosition()
    {
        return backgroundList[backgroundList.Count / 2 - 1].transform.position;
    }
    public void AllRun()
    {
        popBackground();
        pushBackground();
        popObstacle();
        pushObstacle();
        popWater();
        pushWater();
    }
    public void popObstacle()
    {
        while(true)
        {
            switch (Random.Range(0, (int)Obstacle.MAX))
            {
                case (int)Obstacle.Rock:
                    InitObstacle(rock);
                    break;
                case (int)Obstacle.Tree:
                    InitObstacle(tree);
                    break;
                case (int)Obstacle.Leef:
                    InitObstacle(leef);
                    break;
                case (int)Obstacle.Plant:
                    InitObstacle(plant);
                    break;
            }
            if (obstacle_y < backgroundList[backgroundList.Count - 1].transform.position.y)
                break;
        }
    }
    public void popWater()
    {
        while (true)
        {
            InitWater();
            if (water_y < backgroundList[backgroundList.Count - 1].transform.position.y)
                break;
        }

    }
    public void popBackground()
    {
        int rnd = 3; // Random.Range(0, map.Count);
        backgroundList.Add(mapPool.PopFindByName(map[rnd].Obj, new Vector3(0, mapIndex++ * -map[rnd].mapHeight, BACKGROUND_Z), bidoQuaternion, mapParent.transform));

    }
    public void pushObstacle()
    {
        int i = 0;
        while(true)
        {
            if (ObstacleList[i].transform.position.y > backgroundList[0].transform.position.y)
            {
                obsPool.PushToPool(ObstacleList[i], obParent);
                ObstacleList.RemoveAt(i);
                i++;
            }
            else break;
        }
    }
    public void pushWater()
    {
        int i = 0;
        while (true)
        {
            if (WaterList[i].transform.position.y > backgroundList[0].transform.position.y)
            {
                waterPool.PushToPool(WaterList[i], wtParent);
                WaterList.RemoveAt(i);
                i++;
            }
            else break;
        }
    }
    public void pushBackground()
    {
        mapPool.PushToPool(backgroundList[0], bgParent);
        backgroundList.RemoveAt(0);
    }

    void InitObstacle(List<MapData> obs)
    {
        int rnd = Random.Range(0, obs.Count);
        float position_y = - Random.Range(0f,4f);
        //switch (Random.Range(0, 3)) {
        //    case 0: position_y = -Random.Range(1, 2); break;
        //    case 1: position_y = -Random.Range(3, 4); break;
        //    case 2: position_y = -Random.Range(3, 8); break;
        //}
        int sign_z = Random.Range(0, 2) == 1 ? -1 : 1;
        float position_x = Random.Range(-6.2f, 6.2f);
        float position_z =  sign_z * Mathf.Sqrt((obs[rnd].offset + 43 > Mathf.Pow(position_x, 2)) ? obs[rnd].offset + 43 - Mathf.Pow(position_x, 2) : 1) + BACKGROUND_Z;
        //float zRate = Mathf.Abs((obs[rnd].edgeZ - obs[rnd].centerZ) * position_x / obs[rnd].edgeX) + obs[rnd].centerZ;
        float rotation_z = Random.Range(-16, -14) * position_x;
        Quaternion surfaceAngle = Quaternion.Euler(0, 0, sign_z < 0 ? rotation_z : (-180 - rotation_z));

        if (ObstacleList.Count > 0)
        {
            int confirmCount = (ObstacleList.Count <= 3) ? ObstacleList.Count : 3;
            for (int i = 1; i <= confirmCount; i++)
            {
                if (Vector3.Distance(new Vector3(position_x, obstacle_y + position_y, position_z), ObstacleList[ObstacleList.Count - i].transform.position) < OBSTACLEINTERVAL)
                    return;
            }
        }

        ObstacleList.Add(obsPool.PopFindByName(obs[rnd].Obj, new Vector3(position_x, obstacle_y + position_y, position_z), bidoQuaternion * surfaceAngle, mapParent.transform.GetChild((int)ObjectElement.Obstacle)));
        obstacle_y += position_y;
    }

    void InitWater()
    {
        int rnd = Random.Range(0, water.Count);
        float position_y = -Random.Range(0f, 4f);
        int sign_z = Random.Range(0, 2) == 1 ? -1 : 1;
        float position_x = Random.Range(-6.2f, 6.2f);
        float position_z = sign_z * Mathf.Sqrt((water[rnd].offset + 43 > Mathf.Pow(position_x, 2)) ? water[rnd].offset + 43 - Mathf.Pow(position_x, 2) : 1) + BACKGROUND_Z;
        float rotation_z = Random.Range(-16, -14) * position_x;
        Quaternion surfaceAngle = Quaternion.Euler(0, 0, sign_z < 0 ? rotation_z : (-180 - rotation_z));

        if (WaterList.Count > 0)
        {
            int confirmCount = (WaterList.Count <= 3) ? WaterList.Count : 3;
            for (int i = 1; i <= confirmCount; i++)
            {
                try
                {
                    if (Vector3.Distance(new Vector3(position_x, water_y + position_y, position_z), WaterList[WaterList.Count - i].transform.position) < WATERINTERVAL)
                        return;
                }
                catch
                {
                    print(WaterList.Count - i);
                }
            }
        }

        WaterList.Add(waterPool.PopFindByName(water[rnd].Obj, new Vector3(position_x, water_y + position_y, position_z), bidoQuaternion * surfaceAngle, mapParent.transform.GetChild((int)ObjectElement.Water)));
        water_y += position_y;
    }

    void InitMap(List<MapData> list, ObjectElement flag, int START, int COUNT)
    {
        for (int i = START; i < START + COUNT; i++)
            if (dataManager.MapContainsKey(i) && dataManager.GetMapData(i).Obj)
                list.Add(dataManager.GetMapData(i));

        switch (flag)
        {
            case ObjectElement.Background:
                /*  all map
                for (int i = 0; i < list.Count; i++)
                {
                    mapPool.SetObjectPool(list[i].Obj, 6);
                    mapPool.Initialize(bgParent);
                }*/
                mapPool.SetObjectPool(list[3].Obj, 8);
                mapPool.Initialize(bgParent);
                break;
            case ObjectElement.Water:
                for (int i = 0; i < list.Count; i++)
                {
                    waterPool.SetObjectPool(list[i].Obj, 12);
                    waterPool.Initialize(wtParent);
                }
                break;
            case ObjectElement.Obstacle:
                for (int i = 0; i < list.Count; i++)
                {
                    obsPool.SetObjectPool(list[i].Obj, 8);
                    obsPool.Initialize(obParent);
                }
                break;
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
