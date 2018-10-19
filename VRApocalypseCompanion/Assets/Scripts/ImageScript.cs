using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageScript : MonoBehaviour
{
    float scaleSpeed = 1.2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * scaleSpeed, gameObject.transform.localScale.y * scaleSpeed, gameObject.transform.localScale.z * scaleSpeed);
        }
        if (Input.GetKey(KeyCode.X))
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / scaleSpeed, gameObject.transform.localScale.y / scaleSpeed, gameObject.transform.localScale.z / scaleSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(SwitchImage(0.5f));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator SwitchImage(float seconds)
    {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("m1");
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("m0");
    }
}
