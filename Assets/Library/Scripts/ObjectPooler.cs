using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPooler;

public class ObjectPooler : MonoBehaviour
{
    //define a pool object template
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject[] prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictonary;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i=0; i<pool.size;i++)
            {
                int prefabIndex = Random.Range(0, pool.prefab.Length);
                GameObject obj = Instantiate(pool.prefab[prefabIndex]);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictonary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictonary.ContainsKey(tag))
        {
            return null;
        }
        GameObject objectTSpawn = poolDictonary[tag].Dequeue();

        objectTSpawn.SetActive(true);
        objectTSpawn.transform.position = position;
        objectTSpawn.transform.rotation = rotation;

        poolDictonary[tag].Enqueue(objectTSpawn);

        return objectTSpawn;
    }

    /*public GameObject objPrefab;//the prefab of the object in pool   
    public int poolSize;//Initial pool size

    private List<GameObject> pool;//list to store pool objects

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        pool = new List<GameObject>();

        for(int i = 0; i<poolSize; i++)
        {
            GameObject obj = Instantiate(objPrefab);
            obj.SetActive(false);//set objects as inactive initially
            pool.Add(obj);// add to pool
        }
    }

    public GameObject GetPooledbject()
    {
        foreach(GameObject obj in pool)
        {
            if(!obj.activeInHierarchy)//look for an inactive object
            {
                return obj;
            }
        }

        //optional, you can expand the pool if all the objects are in use
        GameObject newObj = Instantiate(objPrefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }*/
}
