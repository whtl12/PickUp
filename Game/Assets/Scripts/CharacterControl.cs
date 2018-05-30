using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    float h, v;
    bool direction;
    float StartHeight;
    public float hSpeed;
    public Map map;
    public GameObject drop;
    CharacterInfo characterInfo;
    // Use this for initialization
    private void Awake()
    {
        characterInfo = new CharacterInfo();
    }
    void Start () {
        hSpeed = characterInfo.hSpeed;
        StartHeight = characterInfo.startPosition.y;


        h = v = 0;
        direction = false;
        GetComponent<Rigidbody>().maxDepenetrationVelocity = 3;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
            direction = !direction;

        h = direction ? 1 : -1;
        transform.Translate(Vector3.right * h * Time.deltaTime * hSpeed);
        if (transform.position.x > 3)
            transform.position = new Vector3(3, transform.position.y, transform.position.z);
        else if (transform.position.x < -3)
            transform.position = new Vector3(-3, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            float shakeSize = map.speed * Random.Range(-2, 2);
            iTween.ShakePosition(Camera.main.gameObject, new Vector3(shakeSize, shakeSize), 0.5f);
            if (map.speed > 0.05f)
            {
                map.ChangeSpeed(-0.01f);
                hSpeed -= 0.2f;
                transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
                Instantiate(drop, transform.position, Quaternion.identity).GetComponent<Rigidbody>().AddForce((transform.position - collision.transform.position).normalized);

                //iTween.MoveAdd(gameObject, (transform.position - collision.transform.position).normalized * map.speed, 1);
            }

            iTween.MoveAdd(collision.transform.parent.gameObject, Vector3.down * map.speed * 20, 2);
            StartCoroutine(revert());
            StartCoroutine(ObstacleHit());
        }
    }
    IEnumerator revert()
    {
        yield return new WaitForSeconds(1.0f);
        while (transform.position.y + 0.03 > StartHeight || transform.position.y - 0.03 < StartHeight)
        {
            transform.position += new Vector3(0, StartHeight - transform.position.y) * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator ObstacleHit()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        
        for(int i = 0; i < 2; i++)
        {
            GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(0.4f);
        }
        GetComponent<CapsuleCollider>().enabled = true;

        yield return null;
    }
}
