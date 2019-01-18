using System.Collections;
using UnityEngine;

public class GrabSeagull : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float grabDistance = 100.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance)){
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * grabDistance, Color.red);
                if(hit.collider.gameObject.tag == "character")
                {
                    this.transform.root.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                }
            }
        }
    }
}
  