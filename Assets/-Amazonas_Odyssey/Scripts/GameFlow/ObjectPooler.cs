using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    [SerializeField]
    public class Pool
    {
        public GameObject prefab;
        public string tag;
        public int size;
    }

    [SerializeField] private List<Pool> _pools = new();
    [SerializeField] private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new();

        foreach (Pool _pool in _pools)
        {
            Queue<GameObject> objectPool = new();

            for (int i = 0; i < _pool.size; i++)
            {
                GameObject obj = Instantiate(_pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.parent = transform;
            }

            _poolDictionary.Add(_pool.tag, objectPool);
        }

    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.Log($"Pool with tag {tag} doesnt exist.");
            return null;
        }

        GameObject objectSpawned = _poolDictionary[tag].Dequeue();

        if (!objectSpawned.activeInHierarchy)
        {
            objectSpawned.SetActive(true);
            objectSpawned.transform.SetPositionAndRotation(position, rotation);
        }

        _poolDictionary[tag].Enqueue(objectSpawned);

        return objectSpawned;
    }
}
