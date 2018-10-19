/*
 * This script is responsible for the player's health, 
 * visual representation of health
 * and restarting the game after death
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance = null;
    public GameObject bloodLeft;
    public GameObject bloodRight;
    public GameObject gameOverLeft;
    public GameObject gameOverRight;
    public Text countdownLeft;
    public Text countdownRight;

    private Image bloodImageLeft;
    private Image bloodImageRight;
    private int health = 10;
    private int countdownSeconds = 5;

    //There is always in instance of this object
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        //Find and fade blood images 
        bloodImageLeft = bloodLeft.GetComponent<Image>();
        bloodImageLeft.canvasRenderer.SetAlpha(0.01f);

        bloodImageRight = bloodRight.GetComponent<Image>();
        bloodImageRight.canvasRenderer.SetAlpha(0.01f);

        //Disable the gameover panel
        gameOverLeft.SetActive(false);
        gameOverRight.SetActive(false);
    }

    //Taking damage from zombies
    public void Damage()
    {
        health--;
        //While health is more than 0
        if (health > 0)
        {
            //Slowly make blood visible
            bloodImageLeft.canvasRenderer.SetAlpha(bloodImageLeft.canvasRenderer.GetAlpha() + 0.1f);
            bloodImageRight.canvasRenderer.SetAlpha(bloodImageRight.canvasRenderer.GetAlpha() + 0.1f);
        }
        //Else if health drops to 0, countdown to restart
        else if (health == 0)
            StartCoroutine(Countdown(countdownSeconds));

    }

    //Countdown to restart
    IEnumerator Countdown(int count)
    {
        //Set the gameover panel to true
        gameOverLeft.SetActive(true);
        gameOverRight.SetActive(true);
        //While counting
        while (count > 0)
        {
            //Update the countdown text
            countdownLeft.text = count.ToString();
            countdownRight.text = count.ToString();
            //Wait for a second
            yield return new WaitForSeconds(1.0f);
            //Decrease countdown
            count--;
        }
        //When countdown is over reload level
        Application.LoadLevel(Application.loadedLevel);
    }
}
