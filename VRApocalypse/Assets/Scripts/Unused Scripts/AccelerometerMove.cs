using UnityEngine;
using System.Collections;

public class AccelerometerMove : MonoBehaviour
{

    private float rotationSpeed = 100.0f;

    void Update()
    {
        float z = Input.acceleration.z;
        if (gameObject.transform.rotation.x < 50 && gameObject.transform.rotation.y > -85)
            gameObject.transform.Rotate(-z * Time.deltaTime * rotationSpeed, 0, 0);
    }
}
