using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DC_GeneratePanelsJaune : MonoBehaviour {

	GameObject[] panels;
	GameObject panelParent;
	GameObject player;

	void Start () {		
		player = GameObject.FindWithTag("User");
		panelParent = GameObject.FindWithTag("PanelParent");
		panels = Resources.LoadAll("jaune/3d/objects/panels/prefab", typeof(GameObject)).Cast<GameObject>().ToArray(); 
		spawnPanels();
	}

	void createPanel(Vector3 pos) {
		GameObject p_o = (GameObject) Instantiate(panels[Random.Range(0, panels.Length)], pos, Quaternion.Euler(90, Random.Range(0,180), 0));
		p_o.gameObject.tag = "panel";
		p_o.gameObject.transform.SetParent(panelParent.transform);
		p_o.gameObject.AddComponent<randomTexture>();
		p_o.gameObject.GetComponent<randomTexture>().enabled = true;
	}

	void spawnPanels() {
		Vector3 userPos = player.transform.position;				
		float minX, minZ, maxX, maxZ, minY, maxY;
		minX = -500;
		maxX = 500;
		minZ = -500;
		maxZ = 500;
		minY = -500;
		maxY = 500;

		float posX, posZ, posY;
		for (int i = 0; i < 70; i++) { 	
			posX = Random.Range(minX, maxX);
			posY = Random.Range(minY, maxY);
			posZ = Random.Range(minZ, maxZ);
			createPanel(new Vector3(posX, posY, posZ));
		}
	}


}
