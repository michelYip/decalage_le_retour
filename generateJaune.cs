using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class generateJaune : MonoBehaviour {

	GameObject[] buildings;

	void Start () { 
        //Generate many buildings and for each add random texture and moving texture
		buildings = Attributes.getBuildingsFromScene(SceneManager.GetActiveScene().name);
		GameObject j = (GameObject) Instantiate(buildings[Random.Range(0,buildings.Length)], new Vector3(0,0,0), Quaternion.identity);
		j.tag = "planet";
		j.gameObject.transform.SetParent(gameObject.transform);
		Transform[] allChildren = j.GetComponentsInChildren<Transform> ();
		for (int i = 0; i < allChildren.Length; i++) {
			allChildren[i].gameObject.AddComponent<randomTexture>();
			allChildren[i].gameObject.GetComponent<randomTexture>().enabled = true;	
			allChildren[i].gameObject.AddComponent<movingTexture>();
            allChildren[i].gameObject.GetComponent<movingTexture>().enabled = true; //(Random.Range(1,3)==1)?true:false;
		}
	}
}
