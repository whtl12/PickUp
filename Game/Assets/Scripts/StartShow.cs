using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartShow : MonoBehaviour {
    public GameObject player;
    public Text txtCount;
    public const int COUNTTIME = 3;
    Vector3 cameraBasicPosition;
    void Start () {
        cameraBasicPosition = GetComponent<PlayManager>().cameraBasicPosition;
        Camera.main.transform.localPosition = new Vector3(0, -3, -13);
        Camera.main.transform.rotation = Quaternion.Euler(-10, 0, 0);
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<CharacterControl>().enabled = false;
        txtCount.text = COUNTTIME.ToString();
        StartCoroutine(cameraMove());
        StartCoroutine(count());
    }

    void Update () {
		
	}
    IEnumerator cameraMove()
    {
        yield return new WaitForSeconds(1.0f);
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("position", cameraBasicPosition, 
                                                "islocal", true,
                                                "time", 3.0f,
                                                "easetype", iTween.EaseType.easeOutQuad
                                                ));
        iTween.RotateTo(Camera.main.gameObject, iTween.Hash("x", 30,
                                        "time", 3.0f,
                                        "islocal", true
                                        ));
    }
    IEnumerator count()
    {
        yield return new WaitForSeconds(1.5f);
        txtCount.enabled = true;
        for(int i = 1; i < COUNTTIME + 1; i++)
        {
            yield return new WaitForSeconds(1.0f);
            txtCount.text = (COUNTTIME - i).ToString();
        }
        yield return new WaitForSeconds(0.8f);
        txtCount.enabled = false;
        yield return new WaitForSeconds(0.2f);
        gameStart();
    }
    void gameStart()
    {
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<CharacterControl>().enabled = true;
    }
}
