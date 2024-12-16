using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Transform focalPoint;
    [SerializeField] float moveSpeed;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the desired velocity by projecting transform.down onto 
        //Physics.SphereCast()
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 3f);
        Vector3 vel = Vector3.ProjectOnPlane(Vector3.down, hit.normal);

        Debug.DrawRay(transform.position, -transform.up * 3f, Color.yellow);
        Debug.DrawRay(transform.position, vel * 3f, Color.red);

        rb.velocity = vel * 3f;  

    }
}
