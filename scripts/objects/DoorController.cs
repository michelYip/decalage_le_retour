using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

	Animator animator;
	
	void Start () {
		animator = GetComponent<Animator>();
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
}
