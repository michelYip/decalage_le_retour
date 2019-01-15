using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTextureTerrain : MonoBehaviour {

	public Material[] materials;
	public Renderer[] allChildren;

	void Start () {
		materials = Attributes.getMaterialsFromScene("main");
		allChildren = GetComponentsInChildren<Renderer> ();
		foreach(Renderer child in allChildren) {
				Material[] mats = new Material[child.materials.Length];
				for (int i = 0; i < child.materials.Length; i++) {
					mats [i] = materials [Attributes.tileTexture];
				}
				child.material = materials [Attributes.tileTexture];
		}
	}
}