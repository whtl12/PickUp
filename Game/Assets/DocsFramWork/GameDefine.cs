using UnityEngine;
using System.Collections;
using System.IO;
public partial class GameDefine : MonoBehaviour
{



    #region semifixed


    public static bool IsLoadAssetBundleOK = false;
    public static bool IsLoadDataDocs = false;
    


    /// <summary>
    /// 기기에서 패치 파일을 저장하는 저장소 Path 반환.
    /// </summary>
    /// <param name="bWWWPath"></param>
    /// <returns></returns>

    public static string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
           // path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
        else
        {
            string path = Application.dataPath;
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }


    public static string GoogleSheetKey
    {
        get
        {
            return "1AxPaql5ywsV-WXhzuyAPXrOhlo5TnfUM9vcjsayA0MI";
        }
    }
    public static string GoogleSheetBaseUrl = "https://docs.google.com/spreadsheet/pub?key={0}&single=true&output=csv&gid={1}";
    #endregion
}
