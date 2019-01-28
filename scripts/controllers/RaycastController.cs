using System.Collections;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float grabDistance = 20.0f;
    private float speed = 5f;
    private float throwSpeed = 2.0f;
    private LineRenderer ray;
    private GameObject sphere;
    private GameObject target;

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

        if (target != null && target.tag == "Throwable" && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            target.transform.SetParent(null);
            ThrowObject(target.GetComponent<Rigidbody>());
            target = null;
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (target != null && target.tag == "character")
            {
                transform.root.position = target.transform.position + Vector3.up * 3;
                sphere.SetActive(false);
            }
            else if (target != null && target.tag == "Throwable")
            {
                ray.material.color = Color.green;
                ray.SetPosition(0, transform.position);
                ray.SetPosition(1, target.transform.position);
                DrawCollisionPoint();
                target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                target.transform.SetParent(transform);
            }
            else
            {
                ray.SetPosition(0, transform.position + transform.TransformDirection(Vector3.forward) * 0.1f);
                ray.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * grabDistance);
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
                {
                    ray.SetPosition(1, hit.point);
                    DrawCollisionPoint();
                    if (hit.collider.gameObject.tag == "character")
                    {
                        target = hit.collider.gameObject;
                    }
                    //If grabable
                    if (hit.collider.gameObject.tag == "Throwable")
                    {
                        target = hit.collider.gameObject;
                    }
                }
                else
                {
                    sphere.SetActive(false);
                    target = null;
                }
            }
            
        }
        else
        {
            sphere.SetActive(false);
            target = null;
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

    //Throw an object 
    public void ThrowObject(Rigidbody rigidBody)
    {
        rigidBody.velocity = device.velocity * throwSpeed;
        rigidBody.angularVelocity = device.angularVelocity;
    }
}
