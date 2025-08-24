using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPooler;

public class ObjectPooler : MonoBehaviour
{
    //define a pool object template
    [System.Serializable]
    public class Pool // Pool class to hold the tag, prefabs, and size of the pool
    {
        public string tag;
        public GameObject[] prefab;
        public int size;
    }

    public List<Pool> pools; // List of pools to be created in the inspector
    public Dictionary<string, Queue<GameObject>> poolDictonary; // Dictionary to hold the pool of objects with their tags as keys

    public static ObjectPooler Instance; // Singleton instance of the ObjectPooler

    private void Awake() // Awake method to initialize the singleton instance
    {
        Instance = this; // Set the singleton instance to this ObjectPooler
    }

    private void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>(); // Initialize the dictionary to hold the pools 

        foreach (Pool pool in pools) // Iterate through each pool defined in the inspector
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); // Create a new queue for the current pool

            for (int i=0; i<pool.size; i++) // Loop to instantiate the specified number of objects for the pool
            {
                int prefabIndex = Random.Range(0, pool.prefab.Length); // Randomly select a prefab from the pool's prefab array
                GameObject obj = Instantiate(pool.prefab[prefabIndex]); // Instantiate the selected prefab
                obj.SetActive(false); // Set the instantiated object to inactive initially
                objectPool.Enqueue(obj); // Enqueue the object into the queue for the pool
            }
            poolDictonary.Add(pool.tag, objectPool); // Add the created object pool to the dictionary with the pool's tag as the key
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictonary.ContainsKey(tag)) // Check if the tag exists in the dictionary
        {
            return null;
        }
        GameObject objectTSpawn = poolDictonary[tag].Dequeue(); // Dequeue an object from the pool associated with the tag

        objectTSpawn.SetActive(true);
        objectTSpawn.transform.position = position;
        objectTSpawn.transform.rotation = rotation;

        poolDictonary[tag].Enqueue(objectTSpawn); // Re-enqueue the object back into the pool after use

        return objectTSpawn; // Return the spawned object
    }

    
}
