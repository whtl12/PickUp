using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour {
	void Start () {
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            print(col.impulse.magnitude + " , " + col.impulse + "  ,  " + col.transform.name);
            if (col.gameObject.GetComponent<CharacterControl>() != null)
                if (col.impulse.y > col.gameObject.GetComponent<CharacterControl>().characterData.vSpeed)
                {
                    StartCoroutine(ObstacleHit(col.gameObject));
                    PlayManager.m_Instance.HP += col.gameObject.GetComponent<CharacterControl>().characterData.vSpeed * 0.5f - col.impulse.y;
                    InGameUI.m_Instance.SetValue("sldHPbar", PlayManager.m_Instance.HP);
                    FXManager.m_Instance.PlayFX(FXManager.FXList.PLAY_CRASH, col.gameObject);
                }
        }
    }
    IEnumerator ObstacleHit(GameObject target)
    {
        target.GetComponent<CapsuleCollider>().enabled = false;

        //for (int i = 0; i < 2; i++)
        //{
        //    GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        //    yield return new WaitForSeconds(0.2f);
        //    GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        //    yield return new WaitForSeconds(0.4f);
        //}

        yield return new WaitForSeconds(1.5f);

        target.GetComponent<CapsuleCollider>().enabled = true;

        yield return null;
    }
}
