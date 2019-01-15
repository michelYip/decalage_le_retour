using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour {
    float speed;
 
	void Start () {
		transform.position = new Vector3(transform.position.x, Random.Range(10,50), transform.position.z);
		speed = Random.Range(30.0f, 150.0f);
	}

	void Update () {
		transform.Rotate(new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed), Space.World);
	}
}
