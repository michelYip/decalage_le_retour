using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;

public static class Attributes {

    // WORLDS / SCENE MANAGER

    private static string destinationName;
    public static string Destination
    {
        get
        {
            return destinationName;
        }
        set
        {
            destinationName = value;
        }
    }

	public static string[] worldsList() {
		
		DirectoryInfo dir = new DirectoryInfo("Assets/scenes/");
		FileInfo[] info = dir.GetFiles("*.unity");
		string[] list = new string[info.Length];
		string fileName;
		for (int i = 0; i < info.Length; i++) {
			fileName = Path.GetFileName(info[i].ToString());
			fileName = fileName.Substring(0,fileName.LastIndexOf("."));
			list[i] = fileName;
		}
		return list;
	}

	// MATERIALS MANAGER
	
	public static Material[] mainMaterials = Resources.LoadAll("main/materials/troiscarres/Materials", typeof(Material)).Cast<Material>().ToArray();
	public static Material[] jauneMaterials = Resources.LoadAll("jaune/materials/troiscarres/Materials", typeof(Material)).Cast<Material>().ToArray();
	public static Material[] tunnelMaterials = Resources.LoadAll("tunnel/materials/troiscarres/Materials", typeof(Material)).Cast<Material>().ToArray();
	public static Material[] panelMaterials = Resources.LoadAll("main/materials/panels/Materials", typeof(Material)).Cast<Material>().ToArray();
	
	public static Material[] getMaterialsFromScene(string scene) {
		switch (scene) {
			case "main" : return mainMaterials;
			case "jaune" : return jauneMaterials;
			case "tunnel" : return tunnelMaterials;
			case "panel" : return panelMaterials;
			default: Material[] newSceneMaterials = Resources.LoadAll("new/"+scene+"/materials/troiscarres/Materials", typeof(Material)).Cast<Material>().ToArray();
				return newSceneMaterials;
		}
	}

	// SKYBOXES MANAGER

	
	public static Material[] mainSkybox = Resources.LoadAll("main/materials/skybox/Materials", typeof(Material)).Cast<Material>().ToArray();
	public static Material[] jauneSkybox = Resources.LoadAll("jaune/materials/skybox/Materials", typeof(Material)).Cast<Material>().ToArray();
	public static Material[] tunnelSkybox = Resources.LoadAll("tunnel/materials/skybox/Materials", typeof(Material)).Cast<Material>().ToArray();
	
	public static Material[] getSkyboxFromScene(string scene) {
		switch (scene) {
			case "main" : return mainSkybox;
			case "jaune" : return jauneSkybox;
			case "tunnel" : return tunnelSkybox;
			default: Material[] newSceneSkybox = Resources.LoadAll("new/"+scene+"/materials/skybox/Materials", typeof(Material)).Cast<Material>().ToArray();
				return newSceneSkybox;
		}
	}

	// BUILDING MANAGER

	
	public static GameObject[] mainBuildings = Resources.LoadAll("main/3d/buildings/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
	public static GameObject[] jauneBuildings = Resources.LoadAll("jaune/3d/buildings/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
	public static GameObject[] tunnelBuildings = Resources.LoadAll("tunnel/3d/buildings/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
	
	public static GameObject[] getBuildingsFromScene(string scene) {
		switch (scene) {
			case "main" : return mainBuildings;
			case "jaune" : return jauneBuildings;
			case "tunnel" : return tunnelBuildings;
			default: GameObject[] newSceneBuildings = Resources.LoadAll("new/"+scene+"/3d/buildings/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();
				return newSceneBuildings;
		}
	}

	
	// TERRAIN MANAGER

	public static int planeSize = 10;
	public static int halfTileX = 10;
	public static int halfTileZ = 10;
	public static int terrainSize = planeSize * (halfTileX + halfTileZ);
	public static int midTerrainSize = terrainSize / 2;
	public static int tileTexture = Random.Range(0, mainMaterials.Length);

	// OBJECTS MANAGER

	public static int maxObjects = 300;

	// TIMER MANAGER

	public static int minTimerTexture = 500;
	public static int maxTimerTexture = 1000;

	// SCRIPTS MANAGER

	public static void addScriptToGameObject(GameObject obj, string scriptName) {
		obj.AddComponent<randomTextureStart>();
		obj.gameObject.GetComponent<randomTextureStart>().enabled = true;
	}

}
