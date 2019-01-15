using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour {

	GameObject user;
	void Start() {
		user = GameObject.FindWithTag("User");
	}

	void Update () {
		if (Vector3.Distance(transform.position, user.transform.position) >= 1000.0f)
			SceneManager.LoadScene("main");
	}
}
