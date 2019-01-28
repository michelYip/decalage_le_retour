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

        if (normal != Vector3.zero)
        {
            while(normal != gravityUp)
            {
                body.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                if (gravityUp.x < normal.x) gravityUp.x += 0.1f;
                else gravityUp.x -= 0.1f;
            }
            gravityUp = normal.normalized;

        }
        else
        {
            gravityUp = (body.position - transform.position).normalized;
        }
        Vector3 localUp = body.up;

        body.GetComponent<Rigidbody>().AddForce(gravity * gravityUp);

        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50f * Time.deltaTime);
    }
}
