/*
 * This script implements zombie behaviour such as health, speed,
 * taking damage, attacking, dying, dropping items
 */
using UnityEngine;
using System.Collections;

public class ZombieBehaviour : MonoBehaviour
{
    private GameObject target;
    private float zombieSpeed = 2.0f;
    private int zombieHealth = 1;
    private float deathDuration = 1.5f;
    private float attackDuration = 2.0f;

    void Start()
    {
        //Get the zombie's target
        target = GameObject.Find("UserHead").transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //Zombie step
        float step = zombieSpeed * Time.deltaTime;
        //Moves towards target with above step
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
    }

    public void Damage(int gunDamage)
    {
        //Reduce zombie health
        zombieHealth -= gunDamage;
        //If health dropes to 0
        if (zombieHealth <= 0)
        {
            //Kill counter
            SpawnManager.instance.CountKill();
            //Chance to drop item
            DropItem();
            //Death animation
            StartCoroutine(Death(deathDuration));
        }
    }

    //Random chance to drop bullets
    void DropItem()
    {
        int dropChance = Random.Range(0, 3);
        switch (dropChance)
        {
            case 0:
                int droppedItemNumber = Random.Range(1, 3);
                ShotgunBehaviour.instance.GainAmmo(droppedItemNumber);
                break;
        }
    }

    //Method to call animations
    void PlayAnimation(string name)
    {
        this.gameObject.GetComponent<Animator>().Play(name);
    }

    //If target is reached and it collides with player
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Zombie stops walking
            zombieSpeed = 0.0f;
            //Plays Attack animation
            PlayAnimation("attack");
            //Performs an attack
            StartCoroutine(Attack(attackDuration));
        }
    }

    IEnumerator Attack(float seconds)
    {
        //Wait for animation
        yield return new WaitForSeconds(seconds);
        //Decrease players health
        PlayerHealth.instance.Damage();
        //Dies after attack
        StartCoroutine(Death(deathDuration));
        //If we want it to perform multiple attacks
        //StartCoroutine(Attack(seconds)); 
    }

    //Death routine
    IEnumerator Death(float seconds)
    {
        //Deactivates the collider
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //Randomly chooses one of the three dying animations to play
        int deathAnimation = Random.Range(0, 2);
        switch (deathAnimation)
        {
            case 0:
                PlayAnimation("left_fall");
                break;
            case 1:
                PlayAnimation("right_fall");
                break;
            case 2:
                PlayAnimation("back_fall");
                break;
        }
        //Waits a few seconds before destroying the gameobject
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
