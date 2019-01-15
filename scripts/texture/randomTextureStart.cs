using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class randomTextureStart : MonoBehaviour {

	public Material[] materials = new Material[10];
	private Renderer[] allChildren;
	private int frame = 0;
	private int[] timers;
	int size;
	int currentPosition = 0;
	int r;

	void initTimers() {
		allChildren = GetComponentsInChildren<Renderer> ();
		size = allChildren.Length;
		timers = new int[size];
		for (int j = 0; j < size; j++) {
			timers [j] = UnityEngine.Random.Range (Attributes.minTimerTexture, Attributes.maxTimerTexture);
		}
		Array.Sort(timers);
	}

	void Start () {
		if (this.gameObject.tag == "panel") {
			materials = Attributes.panelMaterials;
		}
		else materials = Attributes.getMaterialsFromScene(SceneManager.GetActiveScene().name);
		initTimers();
	}

	void Update () {
		
		if (isDone () == false) { 
			if (frame == timers [currentPosition]) {
				Renderer child = allChildren [currentPosition];	
				if (child.gameObject.tag!= "Terrain") {
					Material[] mats = new Material[child.materials.Length];
					for (int i = 0; i < child.materials.Length; i++) {
						r = UnityEngine.Random.Range (0, materials.Length);
						mats [i] = materials [r];
					}
					allChildren [currentPosition].materials = mats;
					currentPosition++;
				}
			} else
				frame++;
		}
	}

	bool isDone() {
		return (currentPosition >= size);
	}
}
