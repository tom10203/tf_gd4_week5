using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class islandMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    public Transform focalPoint;
    public Transform islandPivot;
    //public bool useFocalPointRotation = true;
    //public bool useIslandPivotRotation = false;
    
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(focalPoint.forward * -horizontalInput * rotationSpeed * Time.deltaTime);
        transform.Rotate(focalPoint.right * verticalInput * rotationSpeed * Time.deltaTime);

        //if (useFocalPointRotation)
        //{
        //    transform.Rotate(focalPoint.forward * horizontalInput * rotationSpeed * Time.deltaTime);
        //    transform.Rotate(focalPoint.right * verticalInput * rotationSpeed * Time.deltaTime);
        //}

        //else if (useIslandPivotRotation)
        //{
        //    transform.Rotate(islandPivot.forward * verticalInput * rotationSpeed * Time.deltaTime);
        //    transform.Rotate(islandPivot.right * horizontalInput * rotationSpeed * Time.deltaTime);
        //}
        //else
        //{
        //    transform.Rotate(Vector3.forward * verticalInput * rotationSpeed * Time.deltaTime);
        //    transform.Rotate(Vector3.right * horizontalInput * rotationSpeed * Time.deltaTime);
        //}
    }
}
