using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = -12;
    Vector3 gravityUp;

    public void Attract(Transform body)
    {
        Transform raycast = body.GetChild(3);
        RaycastAround raycasts = raycast.GetComponent<RaycastAround>();
        Vector3 normal = raycasts.ClosestNormal;

        Debug.DrawRay(body.position, normal, Color.red);

        if (normal != Vector3.zero)
        {
            gravityUp = normal.normalized;
        }
        else
        {
            gravityUp = (body.position - transform.position).normalized;
            Debug.Log("center");
        }
        
        //Debug.Log("gravity Up " + gravityUp);
        body.GetComponent<Rigidbody>().AddForce(gravity * gravityUp);

        Vector3 localUp = body.up;
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, Time.deltaTime * 0.5f);
    }
}
