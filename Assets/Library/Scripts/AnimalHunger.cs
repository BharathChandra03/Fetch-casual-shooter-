using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topBound;
    [SerializeField] private float lowerBound;

    [SerializeField] private int amountToFed;//total amount to feed an animal
    [SerializeField] private int score;
    [SerializeField] private int Dscore;

    //public GameObject animalPrefab;

    private int currentAmountFed = 0;//initial amount fed
    private int resetAmount = 0;
    private int sliderInitialValue = 0;

    public Slider hungerSlider;
    private GameManager gameManager;
    private AudioManager audioManager;

    public string hurtSound = "Hurt";


    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToFed;
        hungerSlider.value = sliderInitialValue;
        hungerSlider.fillRect.gameObject.SetActive(false);
       
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
    }

    // Update is called once per frame
    void Update()
    {
        AnimalMovements();
    }

    //animal movements function
    public void AnimalMovements()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > topBound)//setting topbound limitation
        {
            gameObject.SetActive(false);
        }
        else if (transform.position.z < lowerBound)//setting lowerbound limitation
        {
            gameManager.loseHealth();
            gameObject.SetActive(false);
            audioManager.PlaySFX(hurtSound);
        }
    }

    // Animal feeding function
    public void FeedAnimal(int amount)
    {
        currentAmountFed += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentAmountFed;
        
       
        if (currentAmountFed >= amountToFed)//checking if the amount of food fed is equal to amount  to be fed to an animal.
        {
            //if current amount fed is equal to amount to be fed.
            gameObject.SetActive(false);
            hungerSlider.value = sliderInitialValue;
            hungerSlider.fillRect.gameObject.SetActive(false);
            currentAmountFed = resetAmount;

            // if 2x powerup is in active state
            if (gameManager.player.hasDScorePowerUp)
            {
                gameManager.UpdateScore(Dscore);
            }
            else
            {
                gameManager.UpdateScore(score);
            }
        }

        
        
    }

}
