using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateControllers : MonoBehaviour {

	public GameObject left;
	public GameObject right;

	// Use this for initialization
	void Start () {
		
		left.SetActive(true);
		right.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
