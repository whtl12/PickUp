using UnityEngine;
using System.Collections;

public class DontDestroyUI : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(this);
	}
  
}
