using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class LobbyGyro : MonoBehaviour {
    Vector3 gyroscope_rotation;
    Vector3 base_rotation;
    float delay;
    void Awake()
    {
        Input.gyro.enabled = true;
    }
    void Start () {
        base_rotation = transform.localRotation.eulerAngles;
        gyroscope_rotation = Vector3.zero;
    }
#if UNITY_ANDROID
//#if UNITY_EDITOR
    void Update () {
        if(transform.localRotation.x < 33f && transform.localRotation.x > -11f)
            gyroscope_rotation.x += Input.gyro.rotationRateUnbiased.x * 0.1f;
        if (Mathf.Abs(transform.localRotation.y) < 30f)
            gyroscope_rotation.y += Input.gyro.rotationRateUnbiased.y * 0.1f;
        
        transform.localRotation = Quaternion.Euler(base_rotation + gyroscope_rotation);

        if (Mathf.Abs(Input.gyro.rotationRateUnbiased.x) > 0.2 || Mathf.Abs(Input.gyro.rotationRateUnbiased.y) > 0.2)
        {
            StopCoroutine("rollbackRotation");
            StartCoroutine("rollbackRotation");
        }
        if(delay > 1)
        {
            if(transform.localRotation.eulerAngles != base_rotation)
            {
                Vector3 rotation = (base_rotation - transform.localRotation.eulerAngles).normalized * Time.deltaTime;
                gyroscope_rotation.x -= rotation.x;
                gyroscope_rotation.y -= rotation.y;
            }
        }
    }
#endif
    IEnumerator rollbackRotation()
    {
        print("Start rollbackRotation coroutine");
        delay = 0;
        yield return new WaitForSeconds(1.0f);
        while(true)
        {
            delay += Time.deltaTime;
            if (delay > 1)
                break;
            yield return null;
        }
    }
}
