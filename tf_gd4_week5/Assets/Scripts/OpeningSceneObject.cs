using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSceneObject : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 rotationAxis;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed);
    }
}
