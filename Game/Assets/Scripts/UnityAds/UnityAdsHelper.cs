using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class UnityAdsHelper : MonoBehaviour
{
    private const string android_game_id = "2683484";
    private const string ios_game_id = "xxxxxxx";

    private const string rewarded_video_id = "video";

    public static UnityAdsHelper Instance;

    void Start()
    {
        Instance = this;

        Initialize();
    }

    private void Initialize()
    {
#if UNITY_ANDROID
        Advertisement.Initialize(android_game_id);
#elif UNITY_IOS
        Advertisement.Initialize(ios_game_id);
#endif
    }

    public void ShowRewardedAd() //비디오 광고 송출 요청
    {
        if (Advertisement.IsReady(rewarded_video_id))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };

            Advertisement.Show(rewarded_video_id, options);
        }
    }

    //요청 결과에 따라 Finished, Skipped, Failed
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                {
                    Debug.Log("The ad was successfully shown.");

                    // to do ...
                    // 광고 시청이 완료되었을 때 처리
                    Debug.Log("광고 성공");
                    //Test 코드
                    SceneManager.LoadScene((int)UIManager.SceneLoadIndex.Start);
                    break;
                }
            case ShowResult.Skipped:
                {
                    Debug.Log("The ad was skipped before reaching the end.");

                    // to do ...
                    // 광고가 스킵되었을 때 처리
                    Debug.Log("광고 스킵");
                    //Test코드
                    SceneManager.LoadScene((int)UIManager.SceneLoadIndex.Start);
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad failed to be shown.");

                    // to do ...
                    // 광고 시청에 실패했을 때 처리
                    Debug.Log("광고 실패");
                    break;
                }
        }
    }
}

