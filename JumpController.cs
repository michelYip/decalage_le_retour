using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float jumpPower = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        trackedObject = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Rigidbody rb = this.transform.root.GetComponent<Rigidbody>();
            rb.velocity = transform.root.up * jumpPower;
        }
    }
}
