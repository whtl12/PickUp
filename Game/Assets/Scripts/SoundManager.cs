using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour {
    public AudioSource bgm;
    public AudioClip bgmStart;
    public AudioClip bgmPlay;

    // Use this for initialization
    void Start () {
        bgm = GetComponent<AudioSource>();
	}
	public void ChangedVolume(float value)
    {
        bgm.volume = value;
    }
    public void ChangeMusic(int clip)
    {
        //bgm = GetComponent<AudioSource>();
        switch (clip)
        {
            case 0:
                bgm.clip = bgmStart;
                break;
            case 1:
                bgm.clip = bgmPlay;
                break;

        }
        bgm.Play();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
