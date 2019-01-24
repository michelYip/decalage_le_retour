using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

class DC_Building {

	public GameObject building;
	public bool moving;
	public DC_Building() {}

	public DC_Building(GameObject b) {
		building = b;
		moving = (Random.Range(1,4)>1?false:true);
	}
}

public class DC_GenerateBuildings : MonoBehaviour {

	GameObject[] buildingsPrefabs;
	GameObject buildingParent;
	GameObject player;
	List<DC_Building> buildings = new List<DC_Building>();

	void createBuilding(bool start, Vector3 pos) {

		GameObject b = (GameObject) Instantiate(buildingsPrefabs[Random.Range(0, buildingsPrefabs.Length)], pos, Quaternion.identity);
		b.gameObject.transform.SetParent(buildingParent.transform);
		DC_Building newBuilding = new DC_Building(b);
		Transform[] allChildren = newBuilding.building.GetComponentsInChildren<Transform> ();
		for (int j = 0; j < allChildren.Length; j++) {
			if (start) {	
				allChildren[j].gameObject.AddComponent<randomTextureStart>();
				allChildren[j].gameObject.GetComponent<randomTextureStart>().enabled = true;
			}
			else {
				allChildren[j].gameObject.AddComponent<randomTexture>();
				allChildren[j].gameObject.GetComponent<randomTexture>().enabled = true;
			}
			allChildren[j].gameObject.AddComponent<movingTexture>();
			allChildren[j].gameObject.GetComponent<movingTexture>().enabled = newBuilding.moving?true:false;
		}
		buildings.Add(newBuilding);
	}

	
	void destroyBuildings() {
		List<DC_Building> newBuildings = new List<DC_Building>();
		foreach(DC_Building b in buildings) {

			float playerX, playerZ;
			float buildingX, buildingZ;
            //checking building's position 
			if (b!=null && Vector3.Distance(b.building.transform.position, player.transform.position) > 500.0f) {
				playerX = player.transform.position.x;
				playerZ = player.transform.position.z;
				buildingX = b.building.transform.position.x;
				buildingZ = b.building.transform.position.z;
				Destroy(b.building);
				buildings.Remove(b);
                //if total number of buildings is not enough, create building according to user's position 
				if (buildings.Count <= 12) {
					if (playerX < buildingX) {
						createBuilding(false, new Vector3(buildingX-3*Attributes.terrainSize, 0, buildingZ));
					}
					else {
						createBuilding(false, new Vector3(buildingX+3*Attributes.terrainSize, 0, buildingZ));
					}
					if (playerZ < buildingZ) {
						createBuilding(false, new Vector3(buildingX, 0, buildingZ-3*Attributes.terrainSize));
					}
					else {
						createBuilding(false, new Vector3(buildingX, 0, buildingZ+3*Attributes.terrainSize));
					}
				}
			}
			else  newBuildings.Add(b);
		}
		buildings = newBuildings;
	}


	void spawnBuildings(bool start) {

		Vector3 userPos = player.transform.position;
		if (buildings.Count == 0) {
			// middle
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize, 0, userPos.z + Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize, 0, userPos.z - Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize, 0, userPos.z - Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize, 0, userPos.z + Attributes.midTerrainSize));

			// up
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize + Attributes.terrainSize, 0, userPos.z + Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize + Attributes.terrainSize, 0, userPos.z - Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize + Attributes.terrainSize, 0, userPos.z + Attributes.midTerrainSize + Attributes.terrainSize));
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize + Attributes.terrainSize, 0, userPos.z - Attributes.midTerrainSize - Attributes.terrainSize));
			
			// down
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize - Attributes.terrainSize, 0, userPos.z + Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize - Attributes.terrainSize, 0, userPos.z - Attributes.midTerrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize - Attributes.terrainSize, 0, userPos.z + Attributes.midTerrainSize + Attributes.terrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize - Attributes.terrainSize, 0, userPos.z - Attributes.midTerrainSize - Attributes.terrainSize));
			
			//right
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize, 0, userPos.z + Attributes.midTerrainSize + Attributes.terrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize, 0, userPos.z + Attributes.midTerrainSize + Attributes.terrainSize));
			
			//left
			createBuilding(start, new Vector3(userPos.x + Attributes.midTerrainSize, 0, userPos.z - Attributes.midTerrainSize - Attributes.terrainSize));
			createBuilding(start, new Vector3(userPos.x - Attributes.midTerrainSize, 0, userPos.z - Attributes.midTerrainSize - Attributes.terrainSize));
		}
	}

	void Start () {
		
		buildingsPrefabs = Attributes.getBuildingsFromScene(SceneManager.GetActiveScene().name); 
		buildingParent = GameObject.FindWithTag("BuildingParent");
		player = GameObject.FindWithTag("User");
		spawnBuildings(true);
	}
	
	void Update () {
		
		destroyBuildings();
	}
}
