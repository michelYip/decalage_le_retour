using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjScale : MonoBehaviour {
    float scaleX;
    float scaleY;
    float scaleZ;

	void Start () {
		transform.position = new Vector3(transform.position.x, Random.Range(10,50), transform.position.z);
	}
	
	void Update () {
        scaleX = Random.Range(-5.0f, 5.0f);
        scaleY = Random.Range(-5.0f, 5.0f);
        scaleZ = Random.Range(-5.0f, 5.0f);
		transform.localScale += new Vector3(Time.deltaTime * scaleX, Time.deltaTime * scaleY, Time.deltaTime * scaleZ);
	}
}
