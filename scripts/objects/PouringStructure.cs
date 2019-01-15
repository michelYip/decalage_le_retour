using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringStructure : MonoBehaviour {

	int duration;
	float amplitude;
	void Start () {
		transform.position = new Vector3(transform.position.x, Random.Range(10,200), transform.position.z);
		duration = Random.Range(400, 500);
		amplitude = Random.Range(0.01f, 0.02f);
	}
	
	// Update is called once per frame
	void Update () {
		if (duration > 0) {
			transform.localScale -= new Vector3 (0, amplitude, 0);
			duration --;
		}
		transform.position -= new Vector3 (0f, 0.1f, 0f);		
	}

}