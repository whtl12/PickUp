using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataInfoManager : MonoBehaviour
{
    public static DataInfoManager m_Instance;

    SkillInfo SkillDataInfoTable;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        SkillDataInfoTable = new SkillInfo();
        SkillDataInfoTable.LoadData();
        SkillDataInfoTable.GetDictionary<SkillInfo>();



        //로드 끝나고 씬 바꾸기.
        //처음 로고 영상? 보여주고 씬넘기기 위한 특수 경우기때문에 그냥 넘김.

        if (SceneManager.GetActiveScene().buildIndex == (int)UIManager.SceneLoadIndex.Intro)
            SceneManager.LoadScene((int)UIManager.SceneLoadIndex.Start);


    }
}
