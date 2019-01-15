using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class randomSkyboxTexture : MonoBehaviour {

	Material[] skyboxMats;
	int frame;
	int timer;
	int indexSkybox;

	void Start () {
		skyboxMats = Resources.LoadAll(SceneManager.GetActiveScene().name+"/materials/skybox", typeof(Material)).Cast<Material>().ToArray();
		timer = Random.Range (Attributes.minTimerTexture, Attributes.maxTimerTexture);
		indexSkybox = Random.Range (0, skyboxMats.Length);
	}
	
	void Update () {
		frame++;
		if (frame == timer) {
			RenderSettings.skybox = skyboxMats[indexSkybox];
		}
	}
}
