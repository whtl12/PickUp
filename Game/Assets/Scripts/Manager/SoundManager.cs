using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour {
    public static SoundManager m_Instance;
    public AudioClip start_scean_bgm;
    public AudioClip play_scean_bgm;
    public AudioClip intro_sound;
    public AudioClip start_click_island;
    public AudioClip start_click_ui_button;
    public AudioClip start_enter_play;
    public AudioClip play_crash;
    public AudioClip play_eat_bubble;
    public AudioClip play_die;
    private AudioSource currentBGM;
    private float SetBGMVolme = 1;
    private float SetEffectVolme = 1;
    private bool OnSound = false;

    public enum SoundList
    {
        START_SCEAN_BGM = 0,
        PLAY_SCEAN_BGM,
        INTRO_SOUND,
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

        SetBGMVolme = EncryptValue.GetFloat("BGMSound", 1);
        SetEffectVolme = EncryptValue.GetFloat("EffectSound",1);
        OnSound = EncryptValue.GetFloat("SoundIsOn", 0) == 0 ? true : false;
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)UIManager.SceneLoadIndex.Start)
        {
            PlaySound(SoundList.START_SCEAN_BGM, SoundType.BGM);
        }
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
            case SoundList.INTRO_SOUND:
                _clip = intro_sound;
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
                _clip = play_crash;
                break;
            case SoundList.PLAY_EAT:
                _clip = play_eat_bubble;
                break;
            case SoundList.PLAY_DIE:
                _clip = play_die;
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

        if (type == SoundType.BGM)
            currentBGM = audioSource;
        else
            StartCoroutine(destroySound(delay + soundClip(clip).length, audioSource));
    }
    public void PlaySound(SoundList clip, SoundType type, float volume)
    {
        if (OnSound)
            PlaySound(clip, type, volume, 0f);
    }
    public void PlaySound(SoundList clip, SoundType type)
    {
        if(OnSound)
            PlaySound(clip, type, SetBGMVolme, 0f);
    }
    public void PlaySound(SoundList clip)
    {
        if (OnSound)
            PlaySound(clip, SoundType.FX, SetEffectVolme, 0f);
    }

    public void SetSoundVolme(float val, bool IsBGM)
    {
        if (IsBGM)
            SetBGMVolme = val;
        else
            SetEffectVolme = val;

        if(currentBGM != null)
         currentBGM.volume = SetBGMVolme;
    }

    public float GetBGMVolme()
    {
        return SetBGMVolme;
    }


    public float GetEffectVolme()
    {
        return SetEffectVolme;
    }

    public void SoundOnOff(bool IsOn)
    {
        OnSound = IsOn;

        if (currentBGM != null)
        {
            if (IsOn)
                currentBGM.Play();
            else
            {
                currentBGM.Stop();
            }
                
        }
        else if(IsOn)
            PlaySound(SoundList.START_SCEAN_BGM, SoundType.BGM);

       
    }
}
