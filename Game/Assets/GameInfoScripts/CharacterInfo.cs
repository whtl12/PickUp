using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterInfo : Data
{
    public CharacterInfo(int _atelevel = 0)
    {
        hSpeed = 2f;
        vSpeed = 5f;
        cameraPosition = new Vector3(0, -3.5f, -12f);
        ateLevel = _atelevel;
        player = GameObject.Find("Player");
        drop = Resources.Load("Prefabs/drop") as GameObject;
        playerPref = Resources.Load("Prefabs/Player") as GameObject;
    }

    public GameObject drop { get; private set; }
    public GameObject playerPref { get; private set; }
    public float hSpeed { get; private set; }
    public float vSpeed { get; private set; }
    public Vector3 cameraPosition { get; private set; }
    public GameObject player { get; private set; }
    public int ateLevel { get; private set; }
    public readonly float MaxHorizontal = 3.8f;

    public override void LoadData()
    {
        m_DoneLoad.Character = true;
    }

    public override Dictionary<int, CharacterInfo> GetDictionary<CharacterInfo>()
    {
        return null;
    }
}
