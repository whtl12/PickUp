using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour {
    float h;
    bool direction;
    bool isGround;
    public float hSpeed;
    float vSpeed;
    int ateLevel;
    public Map map;
    MapInfo mapInfo;
    CharacterInfo characterInfo;
    Vector3 cameraPosition;
    GameObject mainCamera;
    float MaxHorizontal = 3.8f;
    public void EatBubble()
    {
        vSpeed += 1;
        if (ateLevel < 10)
        {
            ateLevel++;
            transform.localScale += new Vector3(0.025f, 0.025f, 0.025f);
        }
    }
    private void Awake()
    {
        characterInfo = new CharacterInfo();
        mapInfo = new MapInfo();
        map = GameObject.Find("GameManager").GetComponent<Map>();
    }
    void Start () {
        hSpeed = 2;
        vSpeed = 4;
        cameraPosition = Vector3.zero;
        ateLevel = 0;

        mainCamera = Camera.main.gameObject;
        h = 0;
        direction = isGround = false;
    }

    void Update () {
        mainCamera.transform.position = new Vector3(0, cameraPosition.y + transform.position.y, cameraPosition.z);
        if (Input.GetMouseButtonDown(0))
            direction = !direction;
        h = direction ? 1 : -1;
        transform.Translate(Vector3.right * h * Time.deltaTime * hSpeed);

        if (Mathf.Abs(transform.position.x) > MaxHorizontal)
            transform.position = new Vector3(transform.position.x / Mathf.Abs(transform.position.x) * MaxHorizontal, transform.position.y, transform.position.z);

        if (GetComponent<Rigidbody>().velocity.magnitude > vSpeed)
            GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * vSpeed;


        if (transform.position.z > 0.8f)
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.forward, out hitInfo, 0.5f))
        {
            if(hitInfo.collider.tag == "Background")
                isGround = true;
        } else
        {
            isGround = false;
        }
        if (!isGround)
            GetComponent<Rigidbody>().AddForce(Vector3.forward);

        if (transform.position.y < (map.index - 1) * -mapInfo.dumiHeight && name == "Player")
            map.InitBackground();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            collision.transform.tag = "Finish";
            if (ateLevel > 0)
            {
                ateLevel--;
                transform.localScale -= new Vector3(0.025f, 0.025f, 0.025f);
            }
            //else
            //    Destroy(gameObject);

            //Vector3 normalized = (transform.position - collision.transform.position).normalized;
            //float Deg = Mathf.Atan2(normalized.y, normalized.x) * Mathf.Rad2Deg;

            Vector3 edge = Vector3.zero;
            foreach (ContactPoint contact in collision.contacts)
                edge += contact.point;
            edge /= collision.contacts.Length;
            edge = transform.position - edge;
            //if (Mathf.Abs(edge.x) < 0.3)
            //{
            //    Destroy(gameObject);
            //    Instantiate(characterInfo.playerPref, transform.position, Quaternion.identity).name = "Player"; ;
            //    Instantiate(characterInfo.playerPref, transform.position - Vector3.right * edge.x * 2, Quaternion.identity);
            //}
        }
    }
    IEnumerator Revert()
    {
        yield return new WaitForSeconds(1.0f);
        float distance = (mainCamera.transform.position - cameraPosition).magnitude;
        if (distance > 1)
            iTween.MoveTo(mainCamera, iTween.Hash("position", cameraPosition,"time", 1f, "islocal", true, "movetopath", false, "easetype", iTween.EaseType.easeInOutQuart));
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
