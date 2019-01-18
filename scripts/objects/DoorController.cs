using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	Animator animator;
	
	void Start () {
		animator = GetComponent<Animator>();


	}
	
    void Awake()
    {
        var sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

	void Update () {    
	}

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.CompareTag("User"))
        {
            animator.SetTrigger("Enter");
            SceneManager.LoadScene(this.gameObject.tag);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("In");
        animator.SetBool("isNear", true);
    }
    void OnTriggerExit(Collider col)
    {
        //Debug.Log("Out");
        animator.SetBool("isNear", false);
    }
}
