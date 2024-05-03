using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalFood : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topBound;
    [SerializeField] private float lowerBound;

    [SerializeField] private int amount;

    private AudioManager audioManager;
    //private PoolManager poolManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.audioManager;
        //poolManager = PoolManager.poolManager;
    }

    // Update is called once per frame
    void Update()
    {
        FoodMovement();
    }

    void FoodMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            audioManager.AnimalFoodAudio();
            other.GetComponent<AnimalHunger>().FeedAnimal(amount);
            gameObject.SetActive(false);
        }
    }
}
