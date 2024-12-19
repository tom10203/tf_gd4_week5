using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    [SerializeField] float mouseSensitivity;
    void Start()
    {
        //Cursor.visible = false; 
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontalMovement = Input.GetAxis("Mouse X");
        //transform.Rotate(Vector3.up * horizontalMovement * mouseSensitivity * Time.deltaTime);
    }
}
