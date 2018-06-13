using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataInfoManager : MonoBehaviour
{
    public static DataInfoManager m_Instance;
    public GameObject Loading; // Start씬에서 시작할때 버튼 눌려지는거 막을려고 임시로 만듬.

    SkillInfo SkillDataInfoTable;
    CharacterInfo CharacterDataInfoTable;
    CommonInfo CommonDataInfoTable;
    ItemInfo ItemDataInfoTable;
    MapInfo MapDataInfoTable;
    ShopInfo ShopDataInfoTable;
    
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        StartCoroutine(LoadingData());
    }

    IEnumerator LoadingData()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)UIManager.SceneLoadIndex.Start)
            Loading.SetActive(true);

        SkillDataInfoTable = new SkillInfo();
        SkillDataInfoTable.LoadData();

        CharacterDataInfoTable = new CharacterInfo();
        CharacterDataInfoTable.LoadData();

        CommonDataInfoTable = new CommonInfo();
        CommonDataInfoTable.LoadData();

        ItemDataInfoTable = new ItemInfo();
        ItemDataInfoTable.LoadData();

        MapDataInfoTable = new MapInfo();
        MapDataInfoTable.LoadData();

        ShopDataInfoTable = new ShopInfo();
        ShopDataInfoTable.LoadData();


        while (Data.m_DoneLoad.IsLoad() == false)
        {
            yield return null;
        }

        SkillDataInfoTable.GetDictionary<SkillInfo>();

        if (Loading != null)
            Loading.SetActive(false);

        //로드 끝나고 씬 바꾸기.
        //처음 로고 영상? 보여주고 씬넘기기 위한 특수 경우기때문에 그냥 넘김.
        if (SceneManager.GetActiveScene().buildIndex == (int)UIManager.SceneLoadIndex.Intro)
            SceneManager.LoadScene((int)UIManager.SceneLoadIndex.Start);
    }
    public CharacterData GetCharacterData(int key)
    {
        return CharacterDataInfoTable.GetDictionary<CharacterData>()[key];
    }
    public MapData GetMapData(int key)
    {
        return MapDataInfoTable.GetDictionary<MapData>()[key];
    }
}
