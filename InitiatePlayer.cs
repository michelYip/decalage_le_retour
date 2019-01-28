using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePlayer : MonoBehaviour
{
    GameObject player;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("User");
        Debug.Log(" pos Player : " + player.transform.position);
        Debug.Log(player.name);
        player.transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
