using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using Json = LitJson.JsonData;
using System.Text.RegularExpressions;

public class Utils : MonoBehaviour
{
    public static string parseJson(string[] dataString)
    {
        LitJson.JsonData json = new LitJson.JsonData();
        string[] lines = dataString;

        if (lines.Length > 1)
        {
            string[] columnNames = WWWData.SplitWithDoubleQuote(lines[0], ',');

            int count = 0;
            for (int i = 0; i < columnNames.Length; i++)
            {
                if (columnNames[i] == string.Empty ||
                    columnNames[i].Contains("\r"))
                {
                    break;
                }
                count++;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                LitJson.JsonData dic = new LitJson.JsonData();
                dic.SetJsonType(LitJson.JsonType.Object);

                string[] value = WWWData.SplitWithDoubleQuote(lines[i], ',');
                for (int j = 0; j < count; j++)
                {
                    string val = "";
                    try
                    {
                        val = value[j];
                    }
                    catch
                    {
                        Debug.LogError("Utils::parseJson error / line is " + lines[i]);
                    }
                    if (string.IsNullOrEmpty(val) == false)
                    {
                        dic[columnNames[j]] = val;
                    }
                }

                json.Add(dic);
            }

        }

        return json.ToJson();
    }

    // 이모지 필터
    public static string RestrictEmoji(string str)
    {
        Regex regexEmoji = new Regex(@"\uD83C[\uDF00-\uDFFF]|\uD83D[\uDC00-\uDEFF]|[\u2600-\u26FF]");
        str = regexEmoji.Replace(str, "");
        return str;
    }

    public static T GetInstance_Docs<T>(string[] dataString)
    {
        string js = parseJson(dataString);
        return (T)LitJson.JsonMapper.ToObject<T>(js);
    }

    public static T GetInstance_Docs<T>(string dataString)
    {
        return (T)LitJson.JsonMapper.ToObject<T>(dataString);
    }

    public static bool IsEnumParseName(Type type, string name)
    {
        if (name == null)
            return false;

        System.Reflection.FieldInfo info = type.GetField(name);

        if (info == null)
        {
            Debug.LogError(string.Format("Not Exist {0} Value : {1}", type.Name, name));
            return false;
        }
        return true;
    }

    public static string[] ConvertToStrList(string strvalue)
    {
        if (strvalue == null)
            strvalue = string.Empty;

        if (strvalue.StartsWith("\""))
            strvalue = strvalue.Replace("\"", "");

        string[] arrStrList = strvalue.Split(new char[] { ' ', ',', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        return arrStrList;
    }

    public static int[] ConvertToIntList(string strlistvalue)
    {
        string[] arrStrList = ConvertToStrList(strlistvalue);
        int[] intList = new int[arrStrList.Length];
        for (int i = 0; i < arrStrList.Length; i++)
        {
            intList[i] = Convert.ToInt32(arrStrList[i]);
        }
        return intList;
    }

    public static string ConvertIntListToString(int[] values)
    {
        if (values == null)
            return string.Empty;

        StringBuilder str = new StringBuilder();

        for (int i = 0; i < values.Length; i++)
        {
            str.Append(values[i].ToString());
            if (i != values.Length - 1)
                str.Append(",");
        }

        return str.ToString();
    }

    public static byte[] ConvertToByteList(string strlistvalue)
    {
        string[] arrStrList = ConvertToStrList(strlistvalue);
        byte[] byteList = new byte[arrStrList.Length];
        for (int i = 0; i < arrStrList.Length; i++)
        {
            byteList[i] = Convert.ToByte(arrStrList[i]);
        }
        return byteList;
    }

    public static float[] ConvertToFloatList(string strlistvalue)
    {
        string[] arrStrList = ConvertToStrList(strlistvalue);
        float[] floatList = new float[arrStrList.Length];
        for (int i = 0; i < arrStrList.Length; i++)
        {
            floatList[i] = Convert.ToSingle(arrStrList[i]);
        }
        return floatList;
    }

    public static List<string[]> ConvertJsonToStringList(string objectName, string json)
    {
        List<string[]> list = new List<string[]>();

        if (json == null)
            return list;

        if (json.StartsWith("\""))
        {
            json = json.Remove(0, 1);
            json = json.Remove(json.Length - 1, 1);
        }

        json = json.Replace("\"\"", "\"");

        LitJson.JsonData jsondata = LitJson.JsonMapper.ToObject(json);
        LitJson.JsonData item = jsondata[objectName];

        for (int i = 0; i < item.Count; i++)
        {
            string[] arrstr = new string[item[i].Count];

            for (int j = 0; j < item[i].Count; j++)
            {
                arrstr[j] = item[i][j].ToString();
            }

            list.Add(arrstr);
        }

        return list;
    }

    public static float RoundValue(float v, int digit = 2)
    {
        if (v == 0)
            return 0;

        return (float)Math.Round(v, digit);
    }

    //public static void Save<T>(T instance, string name)
    //{
    //    string json = Serialization.UnitySerializer.JSONSerialize(instance);
    //    PlayerPrefs.SetString(name, json);
    //}

    //public static void SaveWithEncrypt<T>(T instance, string name)
    //{
    //    string json = Serialization.UnitySerializer.JSONSerialize(instance);
    //    //#if UNITY_ANDROID
    //    //            json = AES.EncryptRJ256(GameDefine.SerializeKey, GameDefine.SerializeIV, json);
    //    //#else
    //    json = RC4Crypt.EncryptRC4(GameDefine.SerializeKey, json);
    //    //#endif
    //    PlayerPrefs.SetString(name, json);
    //}


    public static bool Load<T>(string name, T o) where T : class
    {
        string data = PlayerPrefs.GetString(name);
        if (string.IsNullOrEmpty(data) == false)
        {
          //  Serialization.UnitySerializer.JSONDeserializeInto(data, o);
            return true;
        }
        else
            return false;
    }

    public static bool LoadWithDecrypt<T>(string name, T o) where T : class
    {
        string data = PlayerPrefs.GetString(name);
        if (string.IsNullOrEmpty(data) == false)
        {
            //#if UNITY_ANDROID
            //                data = AES.DecryptRJ256(GameDefine.SerializeKey, GameDefine.SerializeIV, data);
            //#else
          //  data = RC4Crypt.DecryptRC4(GameDefine.SerializeKey, data);
            //#endif	
          //  Serialization.UnitySerializer.JSONDeserializeInto(data, o);
            return true;
        }
        else
            return false;
    }

    public static T Load<T>(string name) where T : class
    {
        string data = PlayerPrefs.GetString(name);
        if (string.IsNullOrEmpty(data) == false)
        {
         //   return Serialization.UnitySerializer.JSONDeserialize<T>(data);
        }
        return null;
    }

    public static Json BuildJson(object list)
    {
        return LitJson.JsonMapper.ToObject((LitJson.JsonMapper.ToJson(list)));
    }
}