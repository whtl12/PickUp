using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class WWWData : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public delegate void Callback(WWWData data);

    public static Object GetConfigData(int id)
    {
        return Resources.Load("config/" + id.ToString(), typeof(TextAsset));
    }


    static List<WWWData> m_requestedData = new List<WWWData>();

    public static bool isAllDone
    {
        get
        {
            return m_requestedData.Count == 0;
        }
    }



    public static void RequestReadFromGoogleDrive(int id, Callback OnDone)
    {
        RequestReadFromGoogleDrive(GameDefine.GoogleSheetKey, id, OnDone);
    }

    public static bool useResources = false;

    public static void RequestReadFromGoogleDrive(string key, int id, Callback OnDone)
    {
        GameObject Obj = Camera.main.gameObject;
        WWWData data = Obj.AddComponent<WWWData>();

        if (useResources == false)
        {
           // if (UTIL.DownloadGoogleDocsDirectly())
            {
                m_requestedData.Add(data);
                data.StartDownload(key, id, OnDone);
            }
           // else
            {
                // load from patch
              //  Object patched = DataMgr.instance.GetData(GameDefine.Name_OfPatchFile[(int)GameDefine.EnumPatchFile.GoogleDataTable], id.ToString()); //PatchManager.GetPatchItem( id.ToString());
               // if (patched != null)
              //  {
               //     data.dataString = (patched as TextAsset).text;
               // }
              //  else
                {
                    // error!!!
                    // load from resources
                    // data.dataString = (GetConfigData ( id) as TextAsset).text;
             //       throw new System.Exception("WWWData::RequestReadFromGoogleDrive error. id is " + id);
                }
               // OnDone(data);
            }
        }
        else
        {
            data.dataString = (GetConfigData(id) as TextAsset).text;
            OnDone(data);
        }


    }

    void StartDownload(string key, int id, Callback OnDone)
    {

        StartCoroutine(ReadWithGid(key, id, OnDone));
    }

    public void DownloadAndSave(int id)
    {
        DownloadAndSave(GameDefine.GoogleSheetKey, id);
    }

    public void DownloadAndSave(string key, int id)
    {
        string url = string.Format(GameDefine.GoogleSheetBaseUrl, key, id);
        WWW www = new WWW(url);
        while (www.isDone == false) ;
        string path = string.Format("{0}/Resources/config/{1}.txt", Application.dataPath, id);

#if UNITY_EDITOR
        //CLog.Log(string.Format("###################### DownloadAndSave / id = {0} / www.text = {1}", id, www.text));
#endif

        string str = www.text;

        str = Utils.parseJson(str.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries));

        File.WriteAllText(path, str);

        DestroyImmediate(this);
    }

    /*
	public string text {
		get {
			return www.text;
		}
	}
	*/

    public string dataString;
    [System.NonSerialized]
    public WWW www;



    //string content;
    IEnumerator ReadWithGid(string key, int id, Callback OnDone)
    {
        string url = string.Format(GameDefine.GoogleSheetBaseUrl, key, id);

        while (true)
        {
            www = new WWW(url);
            yield return www;

            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                dataString = www.text;
                OnDone(this);
                break;
            }
            else
            {
                //CLog.LogError("WWWData::ReadWithGid / id is " + id + " error message is " + www.error);

                //Application.Quit();
            }
        }
        //if (UTIL.DownloadGoogleDocsDirectly())
        {
            m_requestedData.Remove(this);
        }
        //		return tryCount < retryCount;
    }


    public string[] Lines
    {
        get
        {
            return dataString.Split(new char[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
            //			return SplitWithDoubleQuote( dataString, '\n');
        }
    }

    public byte[] Bytes
    {
        get
        {
            return System.Text.Encoding.UTF8.GetBytes(dataString);
        }
    }

    public static string[] SplitWithDoubleQuote(string str, char ch)
    {
        System.Text.RegularExpressions.Regex CSVParser = new System.Text.RegularExpressions.Regex(string.Format("{0}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))", ch));
        return CSVParser.Split(str);
    }

    public LitJson.JsonData GetJson<T>()
    {
        LitJson.JsonData json = new LitJson.JsonData();
        string[] lines = Lines;
        if (lines.Length > 1)
        {
            string[] columnNames = SplitWithDoubleQuote(lines[0], ',');

            // for UnitySerializer
            System.Type type = typeof(T);
            json["___i"] = type.FullName;


            if (type.IsArray)
            {

                json["count"] = lines.Length - 1;

                LitJson.JsonData array = new LitJson.JsonData();
                json["contents"] = array;
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = SplitWithDoubleQuote(lines[i], ',');
                    LitJson.JsonData aItem = new LitJson.JsonData();
                    // for UnitySerializer
                    if (type.IsArray)
                    {
                        aItem["___i"] = type.GetElementType().FullName;
                    }
                    for (int v = 0; v < columnNames.Length; v++)
                    {
                        string val = values[v];
                        if (string.IsNullOrEmpty(val) == false)
                        {
                            aItem[columnNames[v]] = val;
                        }
                    }

                    array.Add(aItem);
                }
            }
            else
            {
                string[] values = SplitWithDoubleQuote(lines[1], ',');
                for (int v = 0; v < columnNames.Length; v++)
                {
                    string val = values[v];
                    if (string.IsNullOrEmpty(val) == false)
                    {
                        json[columnNames[v]] = val;
                    }
                }
            }

        }


        //			CLog.Log ( json.ToJson());

        return json;
    }

    //public T GetInstance<T>()
    //{
    //    return (T)Serialization.UnitySerializer.JSONDeserialize(
    //        GetJson<T>().ToJson());
    //}

    //public void GetInstance<T>(object o)
    //{
    //    Serialization.UnitySerializer.JSONDeserializeInto(GetJson<T>().ToJson(), o);
    //}
}
