using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//독스에 써져있는 1행값. ( 읽어오기위해서 있는값 , 독스 1행과 변수 이름이 같아야함 )
public class SkillInfoCSV
{
    public int Index;
    public string Aility;
    public int AilityBasicValue;
    public int AilityPlusValue;
    public int MinLevel;
    public int MaxLevel;

}

// 사용할 데이터에 독스값 입력하기 위한 구조체.
public struct SkillData
{
    //추가할 데이터
    public int Index;
    public SkillInfo.Alility Aility;
    public int AilityBasicValue;
    public int AilityPlusValue;
    public int MinLevel;
    public int MaxLevel;

    public void SetData(SkillInfoCSV csv)
    {
        Index = csv.Index;

        if (Utils.IsEnumParseName(typeof(SkillInfo.Alility), csv.Aility))
            Aility = (SkillInfo.Alility)Enum.Parse(typeof(SkillInfo.Alility), csv.Aility);
        else
            Aility = SkillInfo.Alility.MAX;

        AilityBasicValue = csv.AilityBasicValue;
        AilityPlusValue = csv.AilityPlusValue;
        MinLevel = csv.MinLevel;
        MaxLevel = csv.MinLevel;
    }
}

public class SkillInfo : Data
{
    //독스에 써져있는 1행값
    public enum Alility
    {
        MaxWaterUp = 0,
        SpeedDown,
        MAX
    }

    //읽어온 데이터를 담고 있는 Dic.
    public Dictionary<int, SkillData> dicSkillinfoTable = new Dictionary<int, SkillData>();

    //독스 읽어오기.
    public override void LoadData()
    {
        WWWData.RequestReadFromGoogleDrive((int)DocsTable.Skill, (WWWData docs) =>
        {
            SkillInfoCSV[] infos = Utils.GetInstance_Docs<SkillInfoCSV[]>(docs.Lines);
            if (infos.Length > 0)
            {
                for (int i = 0; i < infos.Length; i++)
                {
                    SkillData data = new SkillData();
                    data.SetData(infos[i]);
                    if (!dicSkillinfoTable.ContainsKey(infos[i].Index) && infos[i].Aility != null)
                        dicSkillinfoTable.Add(infos[i].Index, data);
                    else
                        Debug.LogError("SkillInfo Index Value Error");
                }
            }

        });

       
    }


    public override Dictionary<int, SkillData> GetDictionary<SkillData>()
    {
        return dicSkillinfoTable as Dictionary<int, SkillData>;
    }
    
}