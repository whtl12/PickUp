using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfoCSV
{
    public int Index;
    public string Path;
    public string Name;
    public float mapHeight;
    public float edgeX;
    public float edgeZ;
    public float offsetX;
    public float centerX;
    public float centerZ;

}

public struct MapData
{
    public int Index;
    public GameObject Obj;
    public string Name;
    public float mapHeight;
    public float edgeX;
    public float edgeZ;
    public float centerX;
    public float centerZ;

    public void SetData(MapInfoCSV csv)
    {
        Index = csv.Index;
        Obj = Resources.Load(csv.Path + csv.Name) as GameObject;
        if(!Obj)
            Debug.Log("Error: key " + Index + " , name " + Obj);
        Name = csv.Name;
        mapHeight = csv.mapHeight;
        edgeX = csv.edgeX + csv.offsetX;
        edgeZ = csv.edgeZ;
        centerX = csv.centerX;
        centerZ = csv.centerZ;
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
        return dicMapinfoTable as Dictionary<int, MapData>;
    }
}