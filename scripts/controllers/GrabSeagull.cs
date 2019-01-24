using System.Collections;
using UnityEngine;

public class GrabSeagull : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float grabDistance = 20.0f;
    public float speed = 0.25f;
    public LineRenderer ray;
    public GameObject sphere;
    

    // Use this for initialization
    void Start()
    {
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        sphere.SetActive(false);
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
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabDistance))
            {
                sphere.SetActive(true);
                ray.SetPosition(1, hit.point);
                drawCollisionPoint();
                if (hit.collider.gameObject.tag == "character") {
                    ray.material.color = Color.green;
                    ray.SetPosition(1, hit.transform.position);
                    drawCollisionPoint();
                    transform.root.position += transform.TransformDirection(Vector3.forward) * speed;
                }   
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

    void drawCollisionPoint()
    {
        sphere.GetComponent<Renderer>().material.color = ray.material.color;
        sphere.transform.position = ray.GetPosition(1);
    }
}
  