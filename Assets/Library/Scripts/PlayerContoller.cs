using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    private float horizontalInput;

    [SerializeField] private float speed;
    [SerializeField] private float xRange;

    public bool hasDScorePowerUp = false;
    public int powerUpDuration;
    public GameObject foodPrefab;

    private Coroutine powerUpCoroutine;
    private AudioManager audioManager;
    private GameManager gameManager;

    public string powerUpSound = "PowerUp";

    private void Start()
    {
        audioManager = AudioManager.audioManager;
        gameManager = GameManager.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        FoodProjectile();
    }

    

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        var xClamp = Mathf.Clamp(transform.position.x, -xRange, xRange);
        transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
    }

    void FoodProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Instantiate(foodPrefab, transform.position, foodPrefab.transform.rotation);
            GameObject food = ObjectPooler.Instance.SpawnFromPool("AnimalFood", transform.position, transform.rotation);
            
        }
    }

    public IEnumerator powerUpCoolDown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasDScorePowerUp = false;
        powerUpCoroutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Health"))
        {
            audioManager.PlaySFX(powerUpSound);
            other.gameObject.SetActive(false);
            gameManager.IncreaseHealth();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("DoubleScore"))
        {
            audioManager.PlaySFX(powerUpSound);
            other.gameObject.SetActive(false);
            hasDScorePowerUp = true;

            if (powerUpCoroutine != null)
            {
                StopCoroutine(powerUpCoroutine);
            }
            powerUpCoroutine = StartCoroutine(powerUpCoolDown());
        }
    }

}
