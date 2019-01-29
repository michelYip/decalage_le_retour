using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicGravity : MonoBehaviour
{
    public Transform centerOfGravity;
    public float gravity = 1f;
    public float maxDistance;

    private Rigidbody rb;

    private void FixedUpdate()
    {   
        // no gravity if not a rigid body
        if (!GetComponent<Rigidbody>())
            return;
        // no gravity if object too far
        if (Vector3.Distance(transform.position, centerOfGravity.position) > maxDistance)
            return;

        // gets the rigid body of the object
        if (!rb)
            rb = GetComponent<Rigidbody>();
        // sets gravity to false
        if (rb.useGravity)
            rb.useGravity = false;

        Vector3 direction = (centerOfGravity.position - transform.position).normalized;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);

        if (centerOfGravity.GetComponent<Collider>().Raycast(ray, out hit, maxDistance))
            rb.AddForce(-hit.normal * gravity * rb.mass);      
    }
}
