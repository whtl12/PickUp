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
    void Update () {
        if(Mathf.Abs(gyroscope_rotation.x) <= 20f)
            gyroscope_rotation.x += Input.gyro.rotationRateUnbiased.x * 0.2f;
        else
        {
            if (gyroscope_rotation.x > 20f)
                gyroscope_rotation.x = 20f;
            else if (gyroscope_rotation.x < -20f)
                gyroscope_rotation.x = -20f;
        }

        if (Mathf.Abs(gyroscope_rotation.y) <= 30f)
            gyroscope_rotation.y += Input.gyro.rotationRateUnbiased.y * 0.2f;
        else
        {
            if (gyroscope_rotation.y > 30f)
                gyroscope_rotation.y = 30f;
            else if (gyroscope_rotation.y < -30f)
                gyroscope_rotation.y = -30f;

        }

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
                Vector3 rotation = (base_rotation - transform.localRotation.eulerAngles).normalized * Time.deltaTime * 3;
                gyroscope_rotation.x += rotation.x;
                gyroscope_rotation.y += rotation.y;
            }
        }
    }
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
