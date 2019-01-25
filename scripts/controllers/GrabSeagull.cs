using System.Collections;
using UnityEngine;

public class GrabSeagull : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float grabDistance = 20.0f;
    private float speed = 0.25f;
    private LineRenderer ray;
    private GameObject sphere;
    private float dragDistance = 10.0f;

    // Use this for initialization
    void Start()
    {
        trackedObject = gameObject.GetComponent<SteamVR_TrackedObject>();
        ray = gameObject.GetComponent<LineRenderer>();
        sphere = transform.GetChild(0).gameObject;
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere.SetActive(false);
    }

    // Update is called once per frame
    //
    void Update()
    {
        RaycastHit hit;
        device = SteamVR_Controller.Input((int)trackedObject.index);
        ray.material.color = Color.red;
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            ray.SetPosition(0, transform.position + transform.TransformDirection(Vector3.forward) * 0.1f);
            ray.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * grabDistance);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
            {
                ray.SetPosition(1, hit.point);
                DrawCollisionPoint();
                if (hit.collider.gameObject.tag == "character")
                {
                    ray.material.color = Color.green;
                    ray.SetPosition(1, hit.transform.position);
                    DrawCollisionPoint();
                    DragBySeagull(hit.transform);
                }
                //If grabable
            }
            else
                sphere.SetActive(false);
        }
        else
        {
            sphere.SetActive(false);
            ray.SetPosition(0, Vector3.zero);
            ray.SetPosition(1, Vector3.zero);
        }
    }

    // Draw a sphere centered to the intersection of the collision between the raycast and a collider
    void DrawCollisionPoint()
    {
        sphere.SetActive(true);
        sphere.GetComponent<Renderer>().material.color = ray.material.color;
        sphere.transform.position = ray.GetPosition(1);
    }

    // Make the seagull drag the player behind it
    void DragBySeagull(Transform seagull)
    {
        Vector3 destination = (seagull.TransformDirection(Vector3.back) + seagull.TransformDirection(Vector3.down)) * dragDistance;
        transform.position += (destination - transform.position) * speed;
    }
}
