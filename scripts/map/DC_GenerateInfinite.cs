using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DC_Tile {

	public GameObject tile;
	public float creationTime;

	public DC_Tile() {}

	public DC_Tile(GameObject t, float ct) {
		tile = t;
		creationTime = ct;
	}
}

public class DC_GenerateInfinite : MonoBehaviour {

	public GameObject plane;
	public GameObject player;
	Vector3 startPos;

	Hashtable tiles = new Hashtable();

	void createTile(bool start, Vector3 pos, float updateTime) {
		GameObject t = (GameObject) Instantiate(plane, pos, Quaternion.identity);
		t.gameObject.transform.SetParent(gameObject.transform);

		if (start) {
			t.gameObject.AddComponent<randomTextureTerrainStart>();
			t.gameObject.GetComponent<randomTextureTerrainStart>().enabled = true;	
		}
		else {
			t.gameObject.AddComponent<randomTextureTerrain>();
			t.gameObject.GetComponent<randomTextureTerrain>().enabled = true;	
		}
		
		string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString(); 
		t.name = tilename;
		DC_Tile newTile = new DC_Tile(t, updateTime);
		tiles.Add(tilename, newTile);
	}

	void destroyTiles(float updateTime) {

		Hashtable newTerrain = new Hashtable();
		foreach(DC_Tile tls in tiles.Values) {
			if (tls.creationTime != updateTime) 
				Destroy(tls.tile);
			else newTerrain.Add(tls.tile.name, tls);
		}
		tiles = newTerrain;
	}



	void Start () {
		float updateTime = Time.realtimeSinceStartup;
		Vector3 pos = Vector3.zero;

		for (int x = -Attributes.halfTileX; x < Attributes.halfTileX; x++) {			
			for (int z = -Attributes.halfTileZ; z < Attributes.halfTileZ; z++) {
				pos = new Vector3((x * Attributes.planeSize+startPos.x),
								  0,
								  (z * Attributes.planeSize+startPos.z));
				createTile(true, pos, updateTime);
			}	
		}
	}
	
	void Update () {
		
		int xMove = (int)(player.transform.position.x - startPos.x);
		int zMove = (int)(player.transform.position.z - startPos.z);

		if (Mathf.Abs(xMove) >= Attributes.planeSize || Mathf.Abs(zMove) >= Attributes.planeSize) {

			float updateTime = Time.realtimeSinceStartup;

			int playerX = (int)(Mathf.Floor(player.transform.position.x/Attributes.planeSize)*Attributes.planeSize);
			int playerZ = (int)(Mathf.Floor(player.transform.position.z/Attributes.planeSize)*Attributes.planeSize);
			Vector3 pos = Vector3.zero;
			string tilename = "";
			for (int x = -Attributes.halfTileX; x < Attributes.halfTileX; x++) {			
				for (int z = -Attributes.halfTileZ; z < Attributes.halfTileZ; z++) {
					pos = new Vector3((x * Attributes.planeSize+playerX),
									  0,
									  (z * Attributes.planeSize+playerZ));

					tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
					if (!tiles.ContainsKey(tilename)) {
						createTile(false, pos, updateTime);
					}
					else {
						(tiles[tilename] as DC_Tile).creationTime = updateTime;
					}
				}	
			}
			destroyTiles(updateTime);
			startPos = player.transform.position;
		}
	}
}
