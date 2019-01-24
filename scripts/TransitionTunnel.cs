using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTunnel : MonoBehaviour
{
    GameObject user;
    string destination;

    void Start()
    {
        user = GameObject.FindWithTag("User");
        destination = Attributes.Destination;
    }
    
    void Update()
    {
        if(user.transform.position.y < -300)
        {
            SceneManager.LoadScene(destination);
        }
    }
}
