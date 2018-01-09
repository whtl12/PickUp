using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInfoManager : MonoBehaviour
{
    public static DataInfoManager m_Instance;

    Data DataInfoTable;

    private void Awake()
    {
        m_Instance = this;

        DataInfoTable = new SkillInfo();
        DataInfoTable.LoadData();
    }
}
