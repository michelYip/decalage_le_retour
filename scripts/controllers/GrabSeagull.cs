using System.Collections;
using UnityEngine;

public class GrabSeagull : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float grabDistance = 20.0f;
    public LineRenderer ray;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        ray.material.color = Color.red;
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            ray.SetPosition(0, transform.position + transform.TransformDirection(Vector3.forward) * 0.1f);
            ray.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * grabDistance);
            if (Physics.Raycast(transform.position, transform.position + transform.TransformDirection(Vector3.forward), out hit, grabDistance)){
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * grabDistance, Color.red);
                ray.SetPosition(1, hit.point);
                if (hit.collider.gameObject.tag == "character")
                {
                    ray.material.color = Color.green;
                    ray.SetPosition(1, hit.transform.position);
                    this.transform.root.gameObject.transform.SetParent(hit.collider.gameObject.transform);
                }
            }
        }
        else
        {
            ray.SetPosition(0, Vector3.zero);
            ray.SetPosition(1, Vector3.zero);
        }
    }
}
  