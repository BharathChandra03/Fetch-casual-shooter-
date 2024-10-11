using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topBound;
    [SerializeField] private float lowerBound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CollectableMovements();

    }

    void CollectableMovements()
    {
        transform.Translate(Vector3.forward.normalized * speed * Time.deltaTime);

        if (transform.position.z > topBound)
        {
            gameObject.SetActive(false);
        }
        else if (transform.position.z < lowerBound)
        {
            gameObject.SetActive(false);
        }
    }
}   

