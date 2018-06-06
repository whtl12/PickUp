using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CommonInfo : Data
{
    public override void LoadData()
    {
        m_DoneLoad.Common = true;
    }

    public override Dictionary<int, CommonInfo> GetDictionary<CommonInfo>()
    {
        return null;
    }
}
