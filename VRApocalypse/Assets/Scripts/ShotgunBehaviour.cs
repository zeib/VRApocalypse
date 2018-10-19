/*
 * This script is responsible for shooting raycasts from said shotgun
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShotgunBehaviour : MonoBehaviour
{
    public Transform gunEnd;
    public Camera fpsCam;
    public AudioClip gunAudio;
    public AudioClip emptyGunAudio;
    public Text ammoText;
    public Text ammoGainedText;
    public static ShotgunBehaviour instance = null;

    private int ammoSupply = 5;
    private float ammoSupplySeconds = 30.0f;
    private int ammo = 15;
    private int gunDamage = 1;
    private float weaponRange = 30f;
    private float hitForce = 100f;
    private float shotDuration = 0.07f;
    private LineRenderer laserLine;

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
        //Update the ammo text
        UpdateAmmoText();
        //Fade ammo gained text
        ammoGainedText.canvasRenderer.SetAlpha(0.01f);
        //Find the line renderer
        laserLine = GetComponent<LineRenderer>();
        //Initialize ammo helper
        StartCoroutine(AmmoSupply());
    }

    /*void Update()
    {
        gameObject.transform.rotation = new Quaternion(fpsCam.transform.rotation.eulerAngles.x + 90, fpsCam.transform.rotation.eulerAngles.y + 270, fpsCam.transform.rotation.eulerAngles.z, 0);
    }*/

    public void Shoot()
    {
        if (ammo > 0)
        {
            //Shot effects
            StartCoroutine(ShotEffect());
            //Raycast origin accordig to view from camera
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            //A hit
            RaycastHit hit;
            //Set position of the visible line from gun end to said position
            laserLine.SetPosition(0, gunEnd.position);

            //If it hits
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                //End the line at the hit point
                laserLine.SetPosition(1, hit.point);
                //Get the zombies health, raycasts return the hit object as a reference
                ZombieBehaviour health = hit.collider.GetComponent<ZombieBehaviour>();
                //If it has health, damage it
                if (health != null)
                {
                    health.Damage(gunDamage);
                }
                //If the said rigidbody is not null add force to the hit
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            //Else set the line to end at the max range 
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
            //Decrease ammo and update the ammo text
            ammo--;
            UpdateAmmoText();
        }
        //If out of ammo
        else
        {
            SoundManager.instance.PlaySingle(emptyGunAudio);
        }
    }

    //Updates the ammo text
    public void UpdateAmmoText()
    {
        ammoText.text = ammo.ToString();
    }

    //Gain ammo, used from the zombies
    public void GainAmmo(int amount)
    {
        ammo += amount;
        ammoGainedText.text = amount + " Ammo Gained";
        StartCoroutine(FadeUI(ammoGainedText, 1.0f));
        UpdateAmmoText();
    }

    private IEnumerator FadeUI(Text text, float seconds)
    {
        text.CrossFadeAlpha(1.0f, seconds, false);
        yield return new WaitForSeconds(seconds);
        text.CrossFadeAlpha(0.0f, seconds, false);
    }

    //Supplies x ammo every x seconds
    private IEnumerator AmmoSupply()
    {
        yield return new WaitForSeconds(ammoSupplySeconds);
        GainAmmo(ammoSupply);
        StartCoroutine(AmmoSupply());
    }

    //Shot effects, sound and laser lines
    private IEnumerator ShotEffect()
    {
        SoundManager.instance.PlaySingle(gunAudio);
        laserLine.enabled = true;
        yield return new WaitForSeconds(shotDuration);
        laserLine.enabled = false;
    }
}

