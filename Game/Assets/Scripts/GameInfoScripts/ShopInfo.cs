using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopInfo : Data
{
    public override void LoadData()
    {
        m_DoneLoad.Shop = true;
       
    }

    public override Dictionary<int, ShopInfo> GetDictionary<ShopInfo>()
    {
        return null;
    }
}
