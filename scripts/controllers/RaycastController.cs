using System.Collections;
using UnityEngine;

/* This class allows the use of the trigger button to shoot a raycast in front of the controller */
/* Different behavior are expected depending on the collided object */
/* @uthor : Michel Yip */

public class RaycastController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private float grabDistanceMin = 1f;
    private float grabDistanceMax = 10.0f;
    private float translationSpeed = 0.1f;
    private float throwSpeed = 5.0f;
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

        if (target != null && target.tag == "Throwable" && device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)) // If the player release the left trigger while holding a throwable object
        {
            target.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            target.transform.SetParent(null);
            ThrowObject(target.GetComponent<Rigidbody>());
            target = null;
        }
        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) // If the player hold the left trigger
        {
            if (target != null) // If the raycast collide with an interactable object
            {
                switch(target.tag)
                {
                    case "character":
                        GoToSeagull();
                        break;
                    case "Throwable":
                        GrabObject();
                        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                        {
                            float length = Vector3.Distance(target.transform.position,transform.position);
                            if (length >= grabDistanceMin && length <= grabDistanceMax)
                            {
                                Vector2 translate = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
                                TranslateObject(translate);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else // Throw a raycast in front of the controller
            {
                ray.SetPosition(0, transform.position + transform.TransformDirection(Vector3.forward) * grabDistanceMin);
                ray.SetPosition(1, transform.position + transform.TransformDirection(Vector3.forward) * grabDistanceMax);
                if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) * grabDistanceMin , transform.TransformDirection(Vector3.forward), out hit, grabDistanceMax - grabDistanceMin))
                {
                    ray.SetPosition(1, hit.point);
                    DrawCollisionPoint();
                    if (hit.collider.gameObject.tag == "Throwable" || hit.collider.gameObject.tag == "character")
                    {
                        target = hit.collider.gameObject;
                        if (hit.collider.gameObject.tag == "Throwable") // Reset the object position if it's a Throwable
                        {
                            target.transform.position = hit.point;
                        }
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

    // Move a throwable object along the raycast
    public void TranslateObject(Vector2 translate)
    {
        Vector3 result = target.transform.position + (target.transform.position - transform.position) * translate.y * translationSpeed;
        target.transform.position = (Vector3.Distance(transform.position, result) < grabDistanceMin) ? target.transform.position :
                                    (Vector3.Distance(transform.position, result) > grabDistanceMax) ? target.transform.position :
                                    result;
    }

    // Throw an object 
    public void ThrowObject(Rigidbody rigidBody)
    {
        rigidBody.velocity = device.velocity * throwSpeed;
        rigidBody.angularVelocity = device.angularVelocity * throwSpeed;
    }

    // Teleport the player position on top of the Seagull
    public void GoToSeagull()
    {
        transform.root.position = target.transform.position + Vector3.up * 3;
        sphere.SetActive(false);
    }

    // Grab a throwable object with the raycast
    public void GrabObject()
    {
        ray.material.color = Color.green;
        ray.SetPosition(0, transform.position + transform.TransformDirection(Vector3.forward) * grabDistanceMin);
        ray.SetPosition(1, target.transform.position);
        DrawCollisionPoint();
        target.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        target.transform.SetParent(transform);
    }
}
