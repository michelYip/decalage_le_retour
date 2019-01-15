using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fallProtect : MonoBehaviour {

	Vector3 newPos;
	
	void upY() {
		newPos = new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z);
		transform.position = newPos;
		Debug.Log("upY()");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10.0f) {
			GetComponent<Rigidbody>().useGravity = false;
			upY();
			Debug.Log("< -10.0f");
		}
		if (!GetComponent<Rigidbody>().useGravity && transform.position.y < 5.0f) {
			upY();
			Debug.Log("< -5.0f");
		}
		if (!GetComponent<Rigidbody>().useGravity && transform.position.y >= 5.0f) {
			GetComponent<Rigidbody>().useGravity = true;
			Debug.Log("sommet atteint gravité on");
		}
	}
}
