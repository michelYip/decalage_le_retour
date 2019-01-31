using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    // Use this for initialization
    void Start () {
    }

    void update()
    {
        /*
        Vector3 pos = transform.position;
        pos.x -= 20;
        transform.position = pos;
        */

        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Rigidbody rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, 10, 0);
        }

    }
}
