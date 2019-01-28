using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTunnel : MonoBehaviour
{
    GameObject user;
    string destination;
    public int indice;

    void Start()
    {
        user = GameObject.FindWithTag("User");
        destination = Attributes.Destination;
        indice = -1000;
    }
    
    void Update()
    {
        //Debug.Log(user.transform.position.y);
        if(user.transform.position.y < indice)
        {
            SceneManager.LoadScene(destination);
        }
    }
}
