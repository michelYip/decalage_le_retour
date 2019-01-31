using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// raycast corresponding to a given direction
class Raycast
{
    // value default
    // _distance value must be >= to _lenght value. Otherwise a no collision can be spotted as a collision (distance = 0)
    private float _length = 50;
    private float _distance = 50;
    private string _name = "";
    private RaycastHit _hit;
    private Vector3 _normal = Vector3.zero;

    public Raycast(){}

    public Raycast(Transform body, Vector3 direction, float length)
    {
        _length = length;
        Vector3 vector = body.TransformDirection(direction) * length;
        // draws the rays in the Scene
        Debug.DrawRay(body.position, vector, Color.green);
        if(Physics.Raycast(body.position, direction, out _hit))
        {
            _distance = _hit.distance;
            _normal = _hit.normal;
            _name = _hit.collider.gameObject.name;
        }
        // else, takes the default values
    }
    // GETTERS
    public float Distance
    {
        get{
            return _distance;
        }
    }
    public Vector3 Normal
    {
        get
        {
            return _normal;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
    }
}

// cast 6 rays, in every direction, comparable to the 6 faces of a cube
public class RaycastAround : MonoBehaviour
{
    // determines the length of the casted rays
    [SerializeField]
    private float length = 50;
    // array of the casted rays
    private Raycast[] raycasts = new Raycast[6];

    void Update()
    {
        // calculates the rays
        raycasts[0] = new Raycast(transform, Vector3.back, length);
        raycasts[1] = new Raycast(transform, Vector3.down, length);
        raycasts[2] = new Raycast(transform, Vector3.forward, length);
        raycasts[3] = new Raycast(transform, Vector3.left, length);
        raycasts[4] = new Raycast(transform, Vector3.right, length);
        raycasts[5] = new Raycast(transform, Vector3.up, length);
    }

    // gets the normal of the closest object around
    public Vector3 ClosestNormal
    {
        get
        {
            int index = 0;
            float minDistance = raycasts[0].Distance;
            for (int i = 1; i < 6; i++)
            {
                if (raycasts[i].Distance < minDistance)
                {
                    minDistance = raycasts[i].Distance;
                    index = i;
                }
            }
            Debug.Log("name " + raycasts[index].Name);
            return raycasts[index].Normal;
        }
    }
}
