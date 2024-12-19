using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform focalPoint;
    public float skinWidth = 0.015f;
    public float moveSpeed;
    public float shpereDistance;
    Rigidbody rb;
    SphereCollider sphereCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the desired velocity by projecting transform.down onto 
        //Physics.SphereCast()
        RaycastHit hit;
        bool sphereCast = Physics.SphereCast(transform.position, (sphereCollider.bounds.size.x - skinWidth) / 2, Vector3.down, out hit, shpereDistance);


        AdjustPosition(hit);

        Vector3 currentVelocity = rb.velocity;

        Vector3 downwardsVelocity = Vector3.ProjectOnPlane(Vector3.down, hit.normal);

        float angle = 90f - Vector3.Angle(Vector3.down, downwardsVelocity); //0 rotation == 90 degrees, hence 1-90 is needed. The bigger the angle the higher the influence the projected velocity will have.

        Vector3 newVelocity = currentVelocity + (downwardsVelocity * angle * moveSpeed * Time.deltaTime);

        rb.velocity = newVelocity;

    }

    void AdjustPosition(RaycastHit hitInfo)
    {
        Vector3 pos = transform.position;
        float y = hitInfo.distance;
        Vector3 adjustedPos = new Vector3(pos.x, pos.y - y, pos.z);

        transform.position = adjustedPos;
    }
}
