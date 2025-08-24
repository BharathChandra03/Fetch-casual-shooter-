using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerContoller : MonoBehaviour
{
 
    [SerializeField] private float speed; // The speed at which the player moves
    [SerializeField] private float xRange;

    public bool hasDScorePowerUp = false;
    public int powerUpDuration;
    public GameObject foodPrefab;
    public Transform firePoint; // The point from which the food projectile will be fired

    private Coroutine powerUpCoroutine;
    private AudioManager audioManager;
    private GameManager gameManager;

    public string powerUpSound = "PowerUp";
    public string hurtSound = "Hurt";

    public bool moveRight = false; // Flag to indicate if the player is moving right
    public bool moveLeft = false; // Flag to indicate if the player is moving left
    public bool shooting = false; // Flag to indicate if the player is shooting

    private float currentSpeed = 0f; // The current speed of the player
    [SerializeField] private float acceleration; // How quickly the player accelerates
    [SerializeField] private float deceleration; // How quickly the player decelerates


    private void Start()
    {
        audioManager = AudioManager.audioManager;
        gameManager = GameManager.gameManager;
    }
   

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        
    }

    void PlayerMovement()
    {
        float targetSpeed = 0f; // The speed we want to reach based on input

        if (moveLeft)
        { 
            targetSpeed = -speed; // Move left
        }
        else if(moveRight)
        {
            targetSpeed = speed; // Move right
        }

        // Smoothly interpolate the current speed towards the target speed
        if(Mathf.Abs(targetSpeed)>0.01f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime); // Accelerate towards the target speed
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime); // Decelerate towards zero if no input is given
        }
        
        // Update the player's position based on the current speed
        Vector3 position = transform.position; // Get the current position of the player
        position.x += currentSpeed * Time.deltaTime; // Move left or right
        position.x = Mathf.Clamp(position.x, -xRange, xRange); // Clamp the position within the xRange
        transform.position = position; // Update the player's position

    }

    public void FoodProjectile()
    {
        if (foodPrefab != null && firePoint != null)
        {
            //Instantiate(foodPrefab, transform.position, foodPrefab.transform.rotation);
            ObjectPooler.Instance.SpawnFromPool("AnimalFood", firePoint.transform.position, firePoint.transform.rotation);
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

        if (other.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {

            audioManager.PlaySFX(hurtSound);
            gameManager.loseHealth();
            other.gameObject.SetActive(false);
        }
    }

}
