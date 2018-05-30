using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterInfo : Data
{
    public CharacterInfo()
    {
        hSpeed = 2f;
        startPosition = new Vector3(0, 4.5f);
    }

    public float hSpeed { get; private set; }

    public Vector3 startPosition { get; private set; }

    public override void LoadData()
    {
        throw new System.NotImplementedException();
    }
}
