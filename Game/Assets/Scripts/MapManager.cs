using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    private const int MAPCOUNT = 3;
    private const int ROCKCOUNT = 4;
    private const int TREECOUNT = 2;

    private const int MAPSTART = 0;
    private const int ROCKSTART = 1000;
    private const int TREESTART = 1100;

    float verticalDuration = 7;

    DataInfoManager dataManager;
    List<MapData> map = new List<MapData>();
    List<MapData> rock = new List<MapData>();
    List<MapData> tree = new List<MapData>();
    GameObject mapParent;
    [SerializeField]
    ObjectPool ojtPool;

    // Use this for initialization
    void Start () {
        dataManager = GameObject.Find("DataManager").GetComponent<DataInfoManager>();
        mapParent = GameObject.Find("MapParent");
        ojtPool = GetComponent<ObjectPool>();

        for (int i = MAPSTART; i < MAPSTART + MAPCOUNT; i++)
            if (dataManager.MapContainsKey(i))
                map.Add(dataManager.GetMapData(i));

        ojtPool.SetObjectPool(map[0].Name, 3);
        ojtPool.Initialize(GameObject.Find("Map").transform);


        for (int i = ROCKSTART; i < ROCKSTART + ROCKCOUNT; i++)
            if (dataManager.MapContainsKey(i))
                rock.Add(dataManager.GetMapData(i));

        for (int i = TREESTART; i < TREESTART + TREECOUNT; i++)
            if (dataManager.MapContainsKey(i))
                tree.Add(dataManager.GetMapData(i));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
