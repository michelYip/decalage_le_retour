using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GenerateTunnel : MonoBehaviour {
    
	GameObject[] tunnelPrefabs;
	GameObject middle, up, down;
	GameObject user;
	int randIndex;
	Vector3 tunnelSize;
    string destination;

	void Start () {
		user = GameObject.FindWithTag("User");
		tunnelPrefabs = Attributes.getBuildingsFromScene(SceneManager.GetActiveScene().name);
		randIndex = Random.Range(0, tunnelPrefabs.Length);
		up = (GameObject) Instantiate(tunnelPrefabs[randIndex], user.transform.position, Quaternion.identity);
		middle = (GameObject) Instantiate(tunnelPrefabs[randIndex],  new Vector3(0, user.transform.position.y - 2862, 0), Quaternion.identity);
		down = (GameObject) Instantiate(tunnelPrefabs[randIndex], new Vector3 (0, user.transform.position.y - (2862*2),0) , Quaternion.identity);
        destination = Attributes.Destination;
    }
	
	void Update () {
        Debug.Log("destination : " + destination);
		if (user.transform.position.y < (down.transform.position.y+100)) {
			SceneManager.LoadScene(destination);
		}
	}

}
