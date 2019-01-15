using UnityEngine;
using System.Collections;
 
public class FloatingObject : MonoBehaviour {

    float amplitude;
	float frequency; 
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    void Start () {
		amplitude = Random.Range(0.5f, 200.0f);
    	frequency = Random.Range(0.5f, 2.0f);
        posOffset = transform.position;
    }
     
    void Update () { 
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}