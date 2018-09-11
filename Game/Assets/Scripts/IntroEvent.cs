using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadSound());
	}
	
	IEnumerator LoadSound()
    {
        yield return new WaitForSeconds(0.5f);
        SoundManager.m_Instance.PlaySound(SoundManager.SoundList.INTRO_SOUND);

        yield return new WaitForSeconds(SoundManager.m_Instance.intro_sound.length + 0.1f);
        SoundManager.m_Instance.PlaySound(SoundManager.SoundList.START_SCEAN_BGM, SoundManager.SoundType.BGM, 1, 0.5f);
        SceneManager.LoadScene((int)UIManager.SceneLoadIndex.Start);
    }
}
