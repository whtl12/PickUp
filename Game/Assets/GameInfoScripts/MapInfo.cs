using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapInfo : Data
{
    public readonly float dumiHeight = 64;
    public override void LoadData()
    {
        throw new System.NotImplementedException();
    }

    public override Dictionary<int, MapInfo> GetDictionary<MapInfo>()
    {
        return null;
    }
}