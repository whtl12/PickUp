using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoCSV
{
    public int Index;
    public string CharName;
    public float vSpeed;
    public float hSpeed;
    public int MaxSize;
}

public struct CharacterData
{
    //추가할 데이터
    public int Index;
    public GameObject CharName;
    public float vSpeed;
    public float hSpeed;
    public Vector3 MaxSize;

    public void SetData(CharacterInfoCSV csv)
    {
        Index = csv.Index;
        CharName = GameObject.Find("Prefabs/" + csv.CharName);
        vSpeed = csv.vSpeed;
        hSpeed = csv.hSpeed;
        MaxSize = new Vector3(csv.MaxSize, csv.MaxSize, csv.MaxSize) / 100;
    }
}
public class CharacterInfo : Data
{
    public Dictionary<int, CharacterData> dicCharacterinfoTable = new Dictionary<int, CharacterData>();

    public override void LoadData()
    {
        WWWData.RequestReadFromGoogleDrive((int)DocsTable.Skill, (WWWData docs) =>
        {
            CharacterInfoCSV[] infos = Utils.GetInstance_Docs<CharacterInfoCSV[]>(docs.Lines);
            if (infos.Length > 0)
            {
                for (int i = 0; i < infos.Length; i++)
                {
                    CharacterData data = new CharacterData();
                    data.SetData(infos[i]);
                    if (!dicCharacterinfoTable.ContainsKey(infos[i].Index)/* && infos[i].CharName != null*/)
                        dicCharacterinfoTable.Add(infos[i].Index, data);
                    else
                        Debug.LogError("CharacterInfo Index Value Error");
                }

                m_DoneLoad.Character = true;
            }

        });
    }

    public override Dictionary<int, CharacterData> GetDictionary<CharacterData>()
    {
        return dicCharacterinfoTable as Dictionary<int, CharacterData>; ;
    }
}
