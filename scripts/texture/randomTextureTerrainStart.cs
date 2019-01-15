using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomTextureTerrainStart : MonoBehaviour {

	public Material[] materials;
	public Renderer[] allChildren;
	public int frame = 0;

	void Start () {
		allChildren = GetComponentsInChildren<Renderer> ();
		materials = Attributes.getMaterialsFromScene("main");
	}

	void Update () {
		if (frame == (int)((Attributes.minTimerTexture + Attributes.maxTimerTexture)/2)) {
			foreach(Renderer child in allChildren) {
				Material[] mats = new Material[child.materials.Length];
				for (int i = 0; i < child.materials.Length; i++) {
					mats [i] = materials [Attributes.tileTexture];
				}
				child.material = materials [Attributes.tileTexture];
			}
		}
		frame ++;
	}
}