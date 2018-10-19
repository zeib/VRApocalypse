using UnityEngine;
using System.Collections;

public class KnifeBehaviour : MonoBehaviour
{
    int slashDamage = 2;

    void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            collision.gameObject.GetComponent<ZombieBehaviour>().Damage(slashDamage);
        }
    }

    public void Slash()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Animator>().Play("Slash");
        StartCoroutine(ResetAnimation(0.5f));
    }

    IEnumerator ResetAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<Animator>().Play("Idle");
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

}
