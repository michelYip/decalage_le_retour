using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;

class DC_Door {

	public GameObject objDoor;
	public string exit;
	
    public DC_Door() {}

	public DC_Door(GameObject d, string exitScene) {
		objDoor = d;
		exit = exitScene;
	}
}

public class DC_GenerateDoor : MonoBehaviour {

    private Vector3 pos;

    private float degresEnRadian, x, z, espacement;

    GameObject player;
	GameObject[] doors;

	GameObject doorParent;

	DC_Door door;

	void createDoor(Vector3 pos) {
        GameObject d = (GameObject) Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(pos.x+20, 0, pos.z+20), Quaternion.Euler(0, 180, 0));
		d.gameObject.transform.SetParent(doorParent.transform);
        door = new DC_Door(d, d.tag);
	}
  
    void Start() {
		doorParent = GameObject.FindWithTag("DoorParent");
		player = GameObject.FindWithTag("User");
        pos = Vector3.zero;

		doors = Resources.LoadAll(SceneManager.GetActiveScene().name+"/3d/doors/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();

        if (SceneManager.GetActiveScene().name == "main")
        {
            //distance between each turn
            espacement = 60;

            for (var degres = 0; degres < (360 * 6); degres++)
            {
                degresEnRadian = degres * (Mathf.PI / 180);
                x = espacement * degresEnRadian * Mathf.Cos(degresEnRadian);
                z = espacement * degresEnRadian * Mathf.Sin(degresEnRadian);
                // distanc between 2 doors 
                if ((degres % 34) == 0)
                {
                    createDoor(new Vector3(x, 0, z));
                }
            }
        }
        else
        {
            createDoor(doorParent.transform.position);
        }

        
    }

    void update()
    {
    }

    void destroyDoor(){

    }
}