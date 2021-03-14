using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Pool;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    [System.Serializable]
    public class Pool
    {
        public string name;
        public GameObject prefab;
        public int sizeOfPool;
    }

    public static Pooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public List<Pool> PoolObjects;
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in PoolObjects)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject parentObj = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            parentObj.name = pool.name + "s";
            
            for (int i = 0; i < pool.sizeOfPool; i++)
            {
                GameObject gm = Instantiate(pool.prefab);
                gm.SetActive(false);
                gm.transform.parent = parentObj.transform;
                
                objectPool.Enqueue(gm);
            }
            
            PoolDictionary.Add(pool.name, objectPool);
        }
        
    }

    public GameObject SpawnPoolObject(string name, Vector3 position, Quaternion quaternion)
    {
        if (!PoolDictionary.ContainsKey(name))
        {
            Debug.Log($"Key {name} doesn't exist in {PoolDictionary} ");
            return null;
        }
        GameObject spawnGM = PoolDictionary[name].Dequeue();
        
        spawnGM.SetActive(true);
        spawnGM.transform.position = position;
        spawnGM.transform.rotation = quaternion;

        IPooledObj pooledObj = spawnGM.GetComponent<IPooledObj>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        
        
        PoolDictionary[name].Enqueue(spawnGM);

        return spawnGM;
    }
}
