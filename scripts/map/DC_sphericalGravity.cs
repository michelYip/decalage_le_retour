using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_sphericalGravity : MonoBehaviour {
	
	public GameObject planet;
	public GameObject player;
	public int gravity = 2;
	void  FixedUpdate() {
		Vector3 v = new Vector3( ((-player.transform.position.x + planet.transform.position.x)*gravity),
								 ((-player.transform.position.y + planet.transform.position.y)*gravity),
								 ((-player.transform.position.z + planet.transform.position.z)*gravity));

		player.GetComponent<Rigidbody>().velocity = v;
		transform.LookAt(planet.transform.position);
	}
}
