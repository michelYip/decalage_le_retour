using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Linq;

public class randomTexture : MonoBehaviour {

	public Material[] materials = new Material[10];
	private Renderer[] allChildren;
	int r;

	void Start () {
		if (this.gameObject.tag == "panel") {
			materials = Attributes.panelMaterials;
		}
		else materials = Attributes.getMaterialsFromScene(SceneManager.GetActiveScene().name);

		allChildren = GetComponentsInChildren<Renderer> ();
        for (int i = 0; i < allChildren.Length; i++) {
            Renderer child = allChildren [i];	
            if (child.gameObject.tag!= "Terrain") {
                Material[] mats = new Material[child.materials.Length];
                for (int j = 0; j < child.materials.Length; j++) {
                    r = UnityEngine.Random.Range (0, materials.Length);
                    mats [j] = materials [r];
                }
                allChildren [i].materials = mats;
            }
        }
	}
}
