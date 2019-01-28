using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

class DC_tunnelsBuildings
{
    public GameObject building;
    public bool moving;
    public DC_tunnelsBuildings() {}

    public DC_tunnelsBuildings(GameObject b)
    {
        building = b;
        moving = (Random.Range(1, 4) > 1 ? false : true);
    }
}

public class DC_GenerateTunnelBuildings : MonoBehaviour
{
    GameObject[] buildingsPrefabs;
    GameObject player;
    List<DC_tunnelsBuildings> buildings = new List<DC_tunnelsBuildings>();

    void createBuilding(bool start)
    {
        GameObject b = (GameObject)Instantiate(buildingsPrefabs[Random.Range(0, buildingsPrefabs.Length)]);
        DC_tunnelsBuildings newBuilding = new DC_tunnelsBuildings(b);
        Transform[] allChildren = newBuilding.building.GetComponentsInChildren<Transform>();
        for (int j = 0; j < 2; j++)
        {
            Debug.Log("là");
            if (start)
            {
                allChildren[j].gameObject.AddComponent<randomTextureStart>();
                allChildren[j].gameObject.GetComponent<randomTextureStart>().enabled = true;
                allChildren[j].gameObject.AddComponent<movingTexture>();
                allChildren[j].gameObject.GetComponent<movingTexture>().enabled = newBuilding.moving ? true : false;
            }
        }
        buildings.Add(newBuilding);
    }


    void destroyBuildings()
    {
        List<DC_tunnelsBuildings> newBuildings = new List<DC_tunnelsBuildings>();
        foreach (DC_tunnelsBuildings b in buildings)
        {

            float playerX, playerZ, playerY;
            if (b != null && Vector3.Distance(b.building.transform.position, player.transform.position) > 900.0f)
            {
                if (buildings.Count > 1)
                {
                    playerX = player.transform.position.x;
                    playerZ = player.transform.position.z;
                    playerY = player.transform.position.y;
                    Destroy(b.building);
                    buildings.Remove(b);
                }

            }
        }
    }

    void spawnBuildings(bool start)
    {
        Vector3 userPos = player.transform.position;
        createBuilding(start);
    }

    void Start()
    {
           
        buildingsPrefabs = Attributes.getBuildingsFromScene(SceneManager.GetActiveScene().name);
        player = GameObject.FindWithTag("User");
        spawnBuildings(true);
    }
    
    void Update()
    {
      destroyBuildings();
    }
}
