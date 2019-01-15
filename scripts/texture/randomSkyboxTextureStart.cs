using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class randomSkyboxTextureStart : MonoBehaviour {

	public Material[] skyboxMats;

	void Start () {
		skyboxMats = Attributes.getSkyboxFromScene(SceneManager.GetActiveScene().name);
		RenderSettings.skybox = skyboxMats[Random.Range (0, skyboxMats.Length)];
	}
	
}