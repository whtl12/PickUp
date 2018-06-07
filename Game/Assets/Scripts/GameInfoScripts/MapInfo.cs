using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoCSV
{
    public int Index;
    public string Name;
    public float mapHeight;
}

public struct MapData
{
    public int Index;
    public GameObject Name;
    public float mapHeight;

    public void SetData(MapInfoCSV csv)
    {
        Index = csv.Index;
        Name = Resources.Load("Prefabs/" + csv.Name) as GameObject;
        mapHeight = csv.mapHeight;
    }
}
public class MapInfo : Data
{
    public Dictionary<int, MapData> dicMapinfoTable = new Dictionary<int, MapData>();

    public override void LoadData()
    {
        WWWData.RequestReadFromGoogleDrive((int)DocsTable.Map, (WWWData docs) =>
        {
            MapInfoCSV[] infos = Utils.GetInstance_Docs<MapInfoCSV[]>(docs.Lines);
            if (infos.Length > 0)
            {
                for (int i = 0; i < infos.Length; i++)
                {
                    MapData data = new MapData();
                    data.SetData(infos[i]);
                    if (!dicMapinfoTable.ContainsKey(infos[i].Index))
                        dicMapinfoTable.Add(infos[i].Index, data);
                    else
                        Debug.LogError("MapInfo Index Value Error");
                }

                m_DoneLoad.Map = true;
            }

        });
    }

    public override Dictionary<int, MapData> GetDictionary<MapData>()
    {
        return dicMapinfoTable as Dictionary<int, MapData>; ;
    }
}