using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        Parent = GameObject.FindWithTag("tunnel");
        transform.SetParent(Parent.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
