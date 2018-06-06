using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInfo : Data
{
    public override void LoadData()
    {
        m_DoneLoad.Item = true;
    }

    public override Dictionary<int, ItemInfo> GetDictionary<ItemInfo>()
    {
        return null;
    }
}