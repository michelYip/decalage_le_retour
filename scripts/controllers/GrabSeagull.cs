using System.Collections;
using UnityEngine;

public class GrabSeagull : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float grabDistance = 10.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        Debug.Log(this.transform.root.gameObject);
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (Physics.Raycast(transform.position, transform.up, out hit, grabDistance)){
                Debug.DrawRay(transform.position, transform.up * grabDistance, Color.red);
                if(hit.collider.gameObject.tag == "seagull")
                {
                    this.transform.root.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                }
            }
        }
    }
}
  