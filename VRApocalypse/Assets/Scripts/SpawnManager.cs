/*
 * This script is responsible for spawning zombies and increasing 
 * the game's difficulty according to killed zombies number
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public Text countText;
    public Text levelText;
    public static SpawnManager instance = null;
    public float spawnTime = 12.0f;

    private int killedZombies = 0;
    private int spawnsAmount = 10;
    private List<Vector3> spawnPoints = new List<Vector3>();

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
        //Register spawn points
        for (int i = 0; i <= spawnsAmount; i++)
        {
            spawnPoints.Add(this.gameObject.transform.Find("SpawnPoint (" + i + ")").gameObject.transform.position);
        }
        //Fade level text
        levelText.canvasRenderer.SetAlpha(0.01f);
        //Initialize kill count
        countText.text = 0.ToString();
        //Start spawning
        StartCoroutine(SpawnZombie());
    }

    public void LevelChange(string s, float seconds)
    {
        spawnTime = seconds;
        levelText.text = s;
        StartCoroutine(FadeUI(levelText, 1.0f));
    }

    //Count the dead zombies and update the text
    //Based on killed zombies number increases the difficulty
    public void CountKill()
    {
        killedZombies++;
        switch (killedZombies)
        {
            case 10:
                LevelChange("Level 1", 11.0f);
                break;
            case 20:
                LevelChange("Level 2", 10.0f);
                break;
            case 30:
                LevelChange("Level 3", 8.0f);
                break;
            case 40:
                LevelChange("Level 4", 7.0f);
                break;
            case 50:
                LevelChange("Level 5", 6.0f);
                break;
            case 60:
                LevelChange("Level 6", 4.0f);
                break;
            case 70:
                LevelChange("HELL", 2.0f);
                break;
        }
        countText.text = killedZombies.ToString();
    }

    private IEnumerator FadeUI(Text text, float seconds)
    {
        text.CrossFadeAlpha(1.0f, seconds, false);
        yield return new WaitForSeconds(seconds);
        text.CrossFadeAlpha(0.0f, seconds, false);
    }

    //Spawn a zombie in a random spawn point every x seconds
    IEnumerator SpawnZombie()
    {
        int point = Random.Range(0, spawnsAmount);
        Instantiate(zombie, spawnPoints[point], new Quaternion(0, 180, 0, 0));
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(SpawnZombie());
    }

}
