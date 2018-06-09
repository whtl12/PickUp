using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
    private GameObject prefab = null;
    private int poolCount = 0;
    [SerializeField]
    private List<GameObject> poolList;

    private void Start()
    {
        poolList = new List<GameObject>();
    }
    public void SetObjectPool(GameObject _prefab, int _poolCount)
    {
        prefab = _prefab;
        poolCount = _poolCount;
    }
    private GameObject CreateItem(Transform parent = null)
    {
        GameObject item = Instantiate(prefab) as GameObject;
        item.transform.SetParent(parent);
        item.SetActive(false);
        return item;
    }
    public void Initialize(Transform parent = null)
    {
        for (int i = 0; i < poolCount; ++i)
        {
            poolList.Add(CreateItem(parent));
        }
    }
    public void PushToPool(GameObject item, Transform parent = null)
    {
        //item.transform.SetParent(parent);
        item.SetActive(false);
        poolList.Add(item);
    }
    public GameObject PopFromPool(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (poolList.Count == 0)
            poolList.Add(CreateItem(parent));

        GameObject item = poolList[0];
        item.SetActive(true);
        if (position != Vector3.zero)
            item.transform.SetPositionAndRotation(position, rotation);
        poolList.RemoveAt(0);
        return item;
    }

}
