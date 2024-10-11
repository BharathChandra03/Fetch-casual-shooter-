using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalHunger : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float topBound;
    [SerializeField] private float lowerBound;

    [SerializeField] private int amountToFed;
    [SerializeField] private int score;
    [SerializeField] private int Dscore;

    //public GameObject animalPrefab;

    private int currentAmountFed = 0;
    private int resetAmount = 0;
    private int minValue = 0;

    public Slider hungerSlider;
    private GameManager gameManager;
    private AudioManager audioManager;

    public string hurtSound = "Hurt";


    // Start is called before the first frame update
    void Start()
    {
        hungerSlider.maxValue = amountToFed;
        hungerSlider.value = minValue;
        hungerSlider.fillRect.gameObject.SetActive(false);
       
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
    }

    // Update is called once per frame
    void Update()
    {
        AnimalMovements();
    }

    public void AnimalMovements()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            gameManager.loseHealth();
            gameObject.SetActive(false);
            audioManager.PlaySFX(hurtSound);
        }
    }

    public void FeedAnimal(int amount)
    {
        currentAmountFed += amount;
        hungerSlider.fillRect.gameObject.SetActive(true);
        hungerSlider.value = currentAmountFed;
        
       
        if (currentAmountFed >= amountToFed)
        {
            Destroy(gameObject);
            hungerSlider.value = minValue;
            hungerSlider.fillRect.gameObject.SetActive(false);
            currentAmountFed = resetAmount;
        }

        if(gameManager.player.hasDScorePowerUp)
        {
            gameManager.UpdateScore(Dscore);
        }
        else
        {
            gameManager.UpdateScore(score);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            audioManager.PlaySFX(hurtSound);
            gameManager.loseHealth();
            Destroy(gameObject);
        }
    }

}
