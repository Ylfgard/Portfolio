using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private GameObject _prefab;
    private Transform _container;
    private List<GameObject> _pool;

    public ObjectPool(GameObject prefab, Transform container)
    {
        _prefab = prefab;
        _container = container;
        _pool = new List<GameObject>();
    }

    private GameObject CreateObject()
    {
        GameObject createdObject = GameObject.Instantiate(_prefab, _container);
        _pool.Add(createdObject);
        return createdObject;
    }

    public GameObject GetObject(out bool objectIsNew)
    {
        foreach(GameObject obj in _pool)
        {
            if(obj.activeInHierarchy == false)
            {
                obj.SetActive(true);
                objectIsNew = false;
                return obj;
            }
        }

        objectIsNew = true;
        return CreateObject();
    }
}
