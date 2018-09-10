﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {
    public static FXManager m_Instance;
    public GameObject start_click_island;
    public GameObject start_click_ui_button;
    public GameObject start_enter_play;
    public GameObject play_crash;
    public GameObject play_eat_bubble;
    public GameObject play_die;
    public List<GameObject> loopParticleList;


    public enum FXList
    {
        START_CLICK_ISLAND = 2,
        START_CLICK_UI_BUTTON,
        START_ENTER_PLAY,
        PLAY_CRASH,
        PLAY_EAT,
        PLAY_DIE,
        MAX
    };
    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        loopParticleList = new List<GameObject>();
    }

    private GameObject effect(FXList fx)
    {
        switch (fx)
        {
            case FXList.START_CLICK_ISLAND:
                return start_click_island;
            case FXList.START_CLICK_UI_BUTTON:
                return start_click_ui_button;
            case FXList.START_ENTER_PLAY:
                return start_enter_play;
            case FXList.PLAY_CRASH:
                return play_crash;
            case FXList.PLAY_EAT:
                return play_eat_bubble;
            case FXList.PLAY_DIE:
                return play_die;
            default:
                return null;
        }
    }
    private IEnumerator destroyFX(float checkTime, GameObject particle)
    {
        yield return new WaitForSeconds(checkTime + 1f);
        while(particle)
        {
            if(particle.GetComponent<ParticleSystem>().isStopped)
                DestroyImmediate(particle, true);
            yield return null;
        }
    }
    //private IEnumerator PlayOnDelay(ParticleSystem particle, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    //particle.Play();
    //}
    public void PlayFX(FXList fx, GameObject target, Vector3 offset, float delay, bool shake)
    {
        if (effect(fx) == null) return;
        if (target == null) return;

        //ParticleSystem particle = effect(fx);
        //particle.gameObject.transform.position = target.transform.position + offset;
        //StartCoroutine(PlayOnDelay(particle, delay));
        //if (effect(fx).isStopped)
        //    StartCoroutine(destroyFX(delay, particle));

        GameObject particle = null;
        int index = loopParticleList.FindIndex(item => item.name.Contains(effect(fx).name));
        if (index > -1)
            particle = loopParticleList[index];
        else
            particle = Instantiate(effect(fx), target.transform.position + offset, Quaternion.identity, gameObject.transform);


        if (!particle.GetComponent<ParticleSystem>().main.loop)
            StartCoroutine(destroyFX(delay, particle));
        else
        {
            particle.transform.SetParent(target.transform);
            if (index > -1)
                particle.SetActive(true);
            else
                loopParticleList.Add(particle);
        }
        if (shake)
        {

        }

       
    }
    public void PlayFX(FXList fx, GameObject target, Vector3 offset, float delay)
    {
        PlayFX(fx, target, offset, delay, false);
    }
    public void PlayFX(FXList fx, GameObject target, Vector3 offset, bool shake)
    {
        PlayFX(fx, target, offset, 0f, shake);
    }
    public void PlayFX(FXList fx, GameObject target, float delay, bool shake)
    {
        PlayFX(fx, target, Vector3.zero, delay, shake);
    }
    public void PlayFX(FXList fx, GameObject target, bool shake)
    {
        PlayFX(fx, target, Vector3.zero, 0f, shake);
    }
    public void PlayFX(FXList fx, GameObject target, float delay)
    {
        PlayFX(fx, target, Vector3.zero, delay, false);
    }
    public void PlayFX(FXList fx, GameObject target)
    {
        PlayFX(fx, target, Vector3.zero, 0f, false);
    }

    public void StopFX(bool IsAll, FXList fx = FXList.MAX)
    {
        if(IsAll)
        {
            for (int i = 0; i < loopParticleList.Count; i++)
            {
                loopParticleList[i].SetActive(false);
            }
        }
        else
        {
            int index = loopParticleList.FindIndex(item => item.name.Contains(effect(fx).name));

            if(index > -1)
            {
                loopParticleList[index].SetActive(false);
            }
        }
    }
}