using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour {
    public static SoundManager m_Instance;
    public AudioClip start_scean_bgm;
    public AudioClip play_scean_bgm;
    public AudioClip start_click_island;
    public AudioClip start_click_ui_button;
    public AudioClip start_enter_play;
    public AudioClip play_crash;
    public AudioClip play_eat_bubble;
    public AudioClip play_die;
    private AudioSource currentBGM;

    public enum SoundList
    {
        START_SCEAN_BGM = 0,
        PLAY_SCEAN_BGM,
        START_CLICK_ISLAND,
        START_CLICK_UI_BUTTON,
        START_ENTER_PLAY,
        PLAY_CRASH,
        PLAY_EAT,
        PLAY_DIE,
        MAX
    };
    public enum SoundType
    {
        FX = 0,
        BGM,
        MAX
    };

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        PlaySound(SoundList.START_SCEAN_BGM, SoundType.BGM);
    }
 
    private AudioSource InitAudio(SoundType type)
    {
        GameObject audio = new GameObject(type.ToString() + "_audio");
        audio.transform.SetParent(gameObject.transform);
        AudioSource source = audio.AddComponent<AudioSource>();
        source.spatialBlend = 0;

        source.loop = (type == SoundType.BGM) ? true : false;

        return source;
    }
    private AudioClip soundClip(SoundList clip)
    {
        AudioClip _clip = new AudioClip();
        switch (clip)
        {
            case SoundList.START_SCEAN_BGM:
                _clip = start_scean_bgm;
                break;
            case SoundList.PLAY_SCEAN_BGM:
                _clip = play_scean_bgm;
                break;
            case SoundList.START_CLICK_ISLAND:
                _clip = start_click_island;
                break;
            case SoundList.START_CLICK_UI_BUTTON:
                _clip = start_click_ui_button;
                break;
            case SoundList.START_ENTER_PLAY:
                _clip = start_enter_play;
                break;
            case SoundList.PLAY_CRASH:
                _clip = start_scean_bgm;
                break;
            case SoundList.PLAY_EAT:
                _clip = play_scean_bgm;
                break;
            case SoundList.PLAY_DIE:
                _clip = play_scean_bgm;
                break;
        }
        return _clip;
    }
    private IEnumerator destroySound(float checkTime, AudioSource audio)
    {
        yield return new WaitForSeconds(checkTime + 1f);
        DestroyImmediate(audio.gameObject, true);
    }
    public void PlaySound(SoundList clip, SoundType type, float volume, float delay)
    {
        if (soundClip(clip) == null) return;
        if(type == SoundType.BGM)
            if(currentBGM)
                DestroyImmediate(currentBGM.gameObject, true);

        AudioSource audioSource = InitAudio(type);
        audioSource.clip = soundClip(clip);
        audioSource.volume = volume;
        audioSource.PlayDelayed(delay);
        StartCoroutine(destroySound(delay + soundClip(clip).length, audioSource));

        if (type == SoundType.BGM)
            currentBGM = audioSource;
    }
    public void PlaySound(SoundList clip, SoundType type, float volume)
    {
        PlaySound(clip, type, volume, 0f);
    }
    public void PlaySound(SoundList clip, SoundType type)
    {
        PlaySound(clip, type, 1f, 0f);
    }
    public void PlaySound(SoundList clip)
    {
        PlaySound(clip, SoundType.FX, 1f, 0f);
    }
}
