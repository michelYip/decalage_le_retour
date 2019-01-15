using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadGrey : MonoBehaviour {
	public Renderer[] allChildren;
	public Material greyMat;

	// Use this for initialization
	void Start () {
		Renderer[] allChildren = GetComponentsInChildren<Renderer> ();

		foreach (Renderer child in allChildren) {
			Material[] mats = new Material[child.materials.Length];
			for (int j = 0; j < child.materials.Length; j++) {
				mats [j] = greyMat;
			}
			child.materials = mats;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
