using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

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

	GameObject player;
	GameObject[] doors;

	GameObject doorParent;

	DC_Door door;

	void createDoor(Vector3 pos) {
        GameObject d = (GameObject) Instantiate(doors[Random.Range(0, doors.Length)], new Vector3(pos.x+20, 0, pos.z+20), Quaternion.Euler(0, 180, 0));
		d.gameObject.transform.SetParent(doorParent.transform);
        door = new DC_Door(d, d.tag);
	}

	void destroyDoor() {
		if (door!=null && Vector3.Distance(door.objDoor.transform.position, player.transform.position) > 400.0f)
			spawnDoor();
	}

	void spawnDoor() {
		door.objDoor.transform.position = new Vector3(player.transform.position.x+20, 0, player.transform.position.z+20);
	}
  
    void Start() {
		doorParent = GameObject.FindWithTag("DoorParent");
		player = GameObject.FindWithTag("User");
		Vector3 userPos = player.transform.position;
		doors = Resources.LoadAll(SceneManager.GetActiveScene().name+"/3d/doors/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
		createDoor(userPos);
    }

    void Update() {
		destroyDoor();
    }

}