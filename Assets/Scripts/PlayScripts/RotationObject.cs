using UnityEngine;
using System.Collections;

public class RotationObject : MonoBehaviour
{
    public float speed = 1.0f;

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }
}
