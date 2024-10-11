using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPrefab;
    [SerializeField] private GameObject[] collectablePrefab;
    

    [SerializeField] private float spawnPosX;
    [SerializeField] private float spawnposY;
    [SerializeField] private float spawnPosZ;

    [SerializeField] private float aSpawnStartTime;
    [SerializeField] private float aSpawnRepeatRate;
    [SerializeField] private float cSpawnStartTime;
    [SerializeField] private float cSpawnRepeatRate;

    //private PoolManager poolmanager;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("animalSpawning", aSpawnStartTime, aSpawnRepeatRate);
        InvokeRepeating("collectablesSpawning", cSpawnStartTime, cSpawnRepeatRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        //poolmanager = PoolManager.poolManager;
    }

    void animalSpawning()
    {
        Vector3 aSpawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), spawnposY, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefab.Length);
        //Instantiate(animalPrefab[animalIndex], cSpawnPos, animalPrefab[animalIndex].transform.rotation);
        GameObject animal = ObjectPooler.Instance.SpawnFromPool("Animals", aSpawnPos, animalPrefab[animalIndex].transform.rotation);
    }

    void collectablesSpawning()
    {
        Vector3 cSpawnPos = new Vector3(Random.Range(-spawnPosX, spawnPosX), spawnposY, spawnPosZ);
        int collectableIndex = Random.Range(0, collectablePrefab.Length);
        //Instantiate(collectablePrefab[collectableIndex], cSpawnPos, collectablePrefab[collectableIndex].transform.rotation);
        GameObject collectable = ObjectPooler.Instance.SpawnFromPool("Collectables", cSpawnPos, collectablePrefab[collectableIndex].transform.rotation);
    }
   

    
    
}
