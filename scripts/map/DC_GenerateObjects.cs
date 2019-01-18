using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

class DC_Object {

    public enum ObjectType { THROWABLE, ANIMATED, CHARACTER, PANEL, FREE, NULL }

    public static ObjectType intToEnum(int n) {
        switch (n) {
            case 0: return ObjectType.THROWABLE; 
            case 1: return ObjectType.ANIMATED;
            case 2: return ObjectType.CHARACTER;
            case 3: return ObjectType.PANEL;
            case 4: return ObjectType.FREE;
            default: return ObjectType.NULL;
        }
    }
    
	public GameObject obj;
	public ObjectType type;
	
    public DC_Object() {}

	public DC_Object(GameObject o, ObjectType objType) {
		obj = o;
		type = objType;
	}
}

public class DC_GenerateObjects : MonoBehaviour {

	GameObject player;
	GameObject[] animatedObjects;
	GameObject[] freeObjects;
	GameObject[] throwableObjects;
	GameObject[] characters;
	GameObject[] panels;

	GameObject objectParent;

	List<DC_Object> objects = new List<DC_Object>();
	List<DC_Object> free = new List<DC_Object>();

	void createObject(bool start, Vector3 pos, DC_Object.ObjectType type) {
        
        switch (type) {
            case DC_Object.ObjectType.THROWABLE : 
                GameObject th_o = (GameObject) Instantiate(throwableObjects[Random.Range(0, throwableObjects.Length)], pos, Quaternion.identity);
				th_o.gameObject.tag = "Throwable";
				th_o.gameObject.transform.SetParent(objectParent.transform);
				if (start) {	
					th_o.gameObject.AddComponent<randomTextureStart>();
					th_o.gameObject.GetComponent<randomTextureStart>().enabled = true;
				}
				else {
					th_o.gameObject.AddComponent<randomTexture>();
					th_o.gameObject.GetComponent<randomTexture>().enabled = true;
				}
				DC_Object th_newObject = new DC_Object(th_o, type);
				objects.Add(th_newObject);
                break;

            case DC_Object.ObjectType.FREE : 
                GameObject f_o = (GameObject) Instantiate(freeObjects[Random.Range(0, freeObjects.Length)], pos, Quaternion.identity);
				f_o.gameObject.tag = "free";
				f_o.gameObject.transform.SetParent(objectParent.transform);
				if (start) {	
					f_o.gameObject.AddComponent<randomTextureStart>();
					f_o.gameObject.GetComponent<randomTextureStart>().enabled = true;
				}
				else {
					f_o.gameObject.AddComponent<randomTexture>();
					f_o.gameObject.GetComponent<randomTexture>().enabled = true;
				}
				DC_Object f_newObject = new DC_Object(f_o, type);
				free.Add(f_newObject);
                break;

            case DC_Object.ObjectType.ANIMATED : 
                GameObject a_o = (GameObject) Instantiate(animatedObjects[Random.Range(0, animatedObjects.Length)], pos, Quaternion.identity);
				a_o.gameObject.tag = "animated";
				a_o.gameObject.transform.SetParent(objectParent.transform);
				if (start) {	
					a_o.gameObject.AddComponent<randomTextureStart>();
					a_o.gameObject.GetComponent<randomTextureStart>().enabled = true;
				}
				else {
					a_o.gameObject.AddComponent<randomTexture>();
					a_o.gameObject.GetComponent<randomTexture>().enabled = true;
				}
				addAnimation(a_o);
				DC_Object a_newObject = new DC_Object(a_o, type);
				objects.Add(a_newObject);
                break;

            case DC_Object.ObjectType.CHARACTER : 
                GameObject c_o = (GameObject) Instantiate(characters[Random.Range(0, characters.Length)], new Vector3(pos.x, Random.Range(25,150), pos.z), Quaternion.identity);
				c_o.gameObject.tag = "character";
				c_o.gameObject.transform.SetParent(objectParent.transform);
				if (start) {	
					c_o.gameObject.AddComponent<randomTextureStart>();
					c_o.gameObject.GetComponent<randomTextureStart>().enabled = true;
				}
				else {
					c_o.gameObject.AddComponent<randomTexture>();
					c_o.gameObject.GetComponent<randomTexture>().enabled = true;
				}
				c_o.gameObject.AddComponent<CharacterWander>();
				c_o.gameObject.GetComponent<CharacterWander>().enabled = true;
				DC_Object c_newObject = new DC_Object(c_o, type);
				objects.Add(c_newObject);
                break;

            case DC_Object.ObjectType.PANEL : 
                GameObject p_o = (GameObject) Instantiate(panels[Random.Range(0, panels.Length)], pos, Quaternion.Euler(90, Random.Range(0,180), 0));
				p_o.gameObject.tag = "panel";
				p_o.gameObject.transform.SetParent(objectParent.transform);
				if (start) {	
					p_o.gameObject.AddComponent<randomTextureStart>();
					p_o.gameObject.GetComponent<randomTextureStart>().enabled = true;
				}
				else {
					p_o.gameObject.AddComponent<randomTexture>();
					p_o.gameObject.GetComponent<randomTexture>().enabled = true;
				}
				DC_Object p_newObject = new DC_Object(p_o, type);
				objects.Add(p_newObject);
                break;

            default:break;    
        }
	}

	void addAnimation(GameObject obj) {
		
		int nbAnimation = Random.Range(1,3);
		int[] randomAnimation = new int[nbAnimation];
		for (int i = 0; i < nbAnimation; i++) {
			int rand = Random.Range(0,5);
			randomAnimation[i] = rand;
		}

		for (int i = 0; i < nbAnimation; i++) {
			switch (randomAnimation[i]) {
				case 0: obj.gameObject.AddComponent<ObjRotate>();
					obj.gameObject.GetComponent<ObjRotate>().enabled = true;
					break;
				case 1: obj.gameObject.AddComponent<ObjRotate>();
					obj.gameObject.GetComponent<ObjRotate>().enabled = true;
					break;
				case 2: obj.gameObject.AddComponent<PouringStructure>();
					obj.gameObject.GetComponent<PouringStructure>().enabled = true;
					break;
				case 3: obj.gameObject.AddComponent<FloatingObject>();
					obj.gameObject.GetComponent<FloatingObject>().enabled = true;
					break;
				case 4: obj.gameObject.AddComponent<FloatingObject>();
					obj.gameObject.GetComponent<FloatingObject>().enabled = true;
					break;
				case 5: obj.gameObject.AddComponent<FloatingObject>();
					obj.gameObject.GetComponent<FloatingObject>().enabled = true;
					break;
				case 6: obj.gameObject.AddComponent<ObjScale>();
					obj.gameObject.GetComponent<ObjScale>().enabled = true;
					break;
				default:break;
			}
		}
	}

	void destroyObjects() {
        Vector3 userPos = player.transform.position;
        float minX, minZ, maxX, maxZ;
        minX = userPos.x - Attributes.halfTileX * Attributes.planeSize;
        maxX = userPos.x + Attributes.halfTileX * Attributes.planeSize;
        minZ = userPos.z - Attributes.halfTileZ * Attributes.planeSize;
        maxZ = userPos.z + Attributes.halfTileZ * Attributes.planeSize;

        float posX, posZ, posY;
        foreach (DC_Object o in objects) {
            posX = Random.Range(minX, maxX);
            posY = Random.Range(7, 12);
            posZ = Random.Range(minZ, maxZ);
            switch (o.type) {
                case DC_Object.ObjectType.THROWABLE : 
                    if (o.obj!=null && o.obj.transform.position.y < -1) {
                        Destroy(o.obj);
						objects.Remove(o);
                        createObject(false, new Vector3(posX, posY, posZ), DC_Object.ObjectType.THROWABLE);
                    }
					break;
				
				case DC_Object.ObjectType.ANIMATED : 
                	if (o.obj!=null && o.obj.transform.position.y < -20){
                        Destroy(o.obj);
						objects.Remove(o);
                        createObject(false, new Vector3(posX, posY, posZ), DC_Object.ObjectType.ANIMATED);
                    }
                    break;
				
				case DC_Object.ObjectType.CHARACTER : 
                	if (o.obj!=null && Vector3.Distance(o.obj.transform.position, player.transform.position) > 200.0f) {
                        Destroy(o.obj);
						objects.Remove(o);
                        createObject(false, new Vector3(posX, posY, posZ), DC_Object.ObjectType.CHARACTER);
                    }
                    break;
				
				case DC_Object.ObjectType.PANEL : 
                	if (o.obj!=null && Vector3.Distance(o.obj.transform.position, player.transform.position) > 200.0f) {
                        Destroy(o.obj);
						objects.Remove(o);
                        createObject(false, new Vector3(posX, posY, posZ), DC_Object.ObjectType.CHARACTER);
                    }
                    break;
                
				default: return;
            }
		}
	}

	void destroyFreeObjects() {
		foreach(DC_Object o in free) {
			if (o.obj!=null && Vector3.Distance(o.obj.transform.position, player.transform.position) > 200.0f) {
				Destroy(o.obj);
				free.Remove(o);
			}
		}
	}

	void spawnObjects(bool start) {
		Vector3 userPos = player.transform.position;
		if (objects.Count <= Attributes.maxObjects) {				
			float minX, minZ, maxX, maxZ;
			minX = userPos.x - Attributes.halfTileX * Attributes.planeSize;
			maxX = userPos.x + Attributes.halfTileX * Attributes.planeSize;
			minZ = userPos.z - Attributes.halfTileZ * Attributes.planeSize;
			maxZ = userPos.z + Attributes.halfTileZ * Attributes.planeSize;

			float posX, posZ, posY;
			for (int i = 0; i < Attributes.maxObjects - objects.Count; i++) { 	
				posX = Random.Range(minX, maxX);
				posY = Random.Range(7, 12);
				posZ = Random.Range(minZ, maxZ);
				createObject(start, new Vector3(posX, posY, posZ), DC_Object.intToEnum(Random.Range(0,4)));
			}
		}
	}
  
	void spawnFreeObjects(bool start) {

		Vector3 userPos = player.transform.position;			
		float minX, minZ, maxX, maxZ;
		minX = userPos.x - Attributes.halfTileX * Attributes.planeSize;
		maxX = userPos.x + Attributes.halfTileX * Attributes.planeSize;
		minZ = userPos.z - Attributes.halfTileZ * Attributes.planeSize;
		maxZ = userPos.z + Attributes.halfTileZ * Attributes.planeSize;

		float posX, posZ, posY;
		for (int i = 0; i < 3-free.Count; i++) { 	
			posX = Random.Range(minX, maxX);
			posY = Random.Range(7, 12);
			posZ = Random.Range(minZ, maxZ);
			createObject(start, new Vector3(posX, posY, posZ), DC_Object.ObjectType.FREE);
		}
	}

    void Start() {
		
		string sceneName;
		if (SceneManager.GetActiveScene().name == "main") sceneName = "main";
		else sceneName = "new/"+SceneManager.GetActiveScene().name;
		characters = Resources.LoadAll(sceneName+"/3d/objects/characters/prefab", typeof(GameObject)).Cast<GameObject>().ToArray(); 
		panels = Resources.LoadAll(sceneName+"/3d/objects/panels/prefab", typeof(GameObject)).Cast<GameObject>().ToArray(); 
		animatedObjects = Resources.LoadAll(sceneName+"/3d/objects/animated/prefab", typeof(GameObject)).Cast<GameObject>().ToArray(); 
		freeObjects = Resources.LoadAll(sceneName+"/3d/objects/free/prefab", typeof(GameObject)).Cast<GameObject>().ToArray(); 
		throwableObjects = Resources.LoadAll(sceneName+"/3d/objects/throwable/prefab", typeof(GameObject)).Cast<GameObject>().ToArray();  
		objectParent = GameObject.FindWithTag("ObjectParent");
		player = GameObject.FindWithTag("User");
		spawnObjects(true);
		spawnFreeObjects(true);
    }

    void Update() {

		destroyFreeObjects();
		spawnFreeObjects(false);
        destroyObjects();
        //spawnObjects(false);
    }


}
