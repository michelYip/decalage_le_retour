using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour {

	public float moveSpeed = 6; // move speed
	public float turnSpeed = 90; // turning speed (degrees/second)
	public float lerpSpeed = 10; // smoothing speedfloatar gravity = 10; // gravity acceleration
	public bool isGrounded;
	public float deltaGround = 0.2f; // character is grounded up to this distance
	public float jumpSpeed = 10; // vertical jump initial speed
	public float jumpRange = 10; // range to detect target wall

	private Vector3 surfaceNormal; // current surface normal
	private Vector3 myNormal; // character normal
	private float distGround; // distance from character position to ground
	private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
	private float vertSpeed = 0; // vertical jump current speed 

	void  Start() {
		myNormal = transform.up; // normal starts as character up direction 
		GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation
		// distance from transform.position to ground
		distGround = GetComponent<Collider>().bounds.extents.y - GetComponent<Collider>().bounds.center.y;  
	}

	void FixedUpdate() {
		// apply constant weight force according to character normal:
		//GetComponent<Rigidbody>().AddForce(-Physics.gravity * GetComponent<Rigidbody>().mass * myNormal);
	}

	void Update() {
		// jump code - jump to wall or simple jump
		if (jumping) return;  // abort Update while jumping to a wall
		Ray r;
		RaycastHit hit;
		if (Input.GetButtonDown("Jump")){ // jump pressed:
			r = new Ray(transform.position, transform.forward);
			if (Physics.Raycast(r, out hit, jumpRange)){ // wall ahead?
				JumpToWall(hit.point, hit.normal); // yes: jump to the wall
			}
			else if (isGrounded){ // no: if grounded, jump up
				GetComponent<Rigidbody>().velocity += jumpSpeed * myNormal;
			}                
		}
		
		// movement code - turn left/right with Horizontal axis:
		transform.Rotate(0, Input.GetAxis("Horizontal")*turnSpeed*Time.deltaTime, 0);
		// update surface normal and isGrounded:
		r = new Ray(transform.position, -myNormal); // cast ray downwards
		if (Physics.Raycast(r, out hit)){ // use it to update myNormal and isGrounded
			isGrounded = hit.distance <= distGround + deltaGround;
			surfaceNormal = hit.normal;
		}
		else {
			isGrounded = false;
			// assume usual ground normal to avoid "falling forever"
			surfaceNormal = Vector3.up; 
		}
		myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed*Time.deltaTime);
		// find forward direction with new myNormal:
		Vector3 myForward = Vector3.Cross(transform.right, myNormal);
		// align character to the new myNormal while keeping the forward direction:
		Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, lerpSpeed*Time.deltaTime);
		// move the character forth/back with Vertical axis:
		transform.Translate(0, 0, Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime); 
	}

	void JumpToWall(Vector3 point, Vector3 normal){
		// jump to wall 
		jumping = true; // signal it's jumping to wall
		GetComponent<Rigidbody>().isKinematic = true; // disable physics while jumping
		Vector3 orgPos = transform.position;
		Quaternion orgRot = transform.rotation;
		Vector3 dstPos = new Vector3(point.x, point.y, point.z) + new Vector3(normal.x, normal.y, normal.z) * (distGround + 0.5f); // will jump to 0.5 above wall
		Vector3 myForward = Vector3.Cross(transform.right, normal);
		Quaternion dstRot = Quaternion.LookRotation(myForward, normal);
		for (float t = 0.0f; t < 1.0f;  t += Time.deltaTime) {
			transform.position = Vector3.Lerp(orgPos, dstPos, t);
			transform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
			return;
		}
		myNormal = normal; // update myNormal
		GetComponent<Rigidbody>().isKinematic = false; // enable physics
		jumping = false; // jumping to wall finished
	}

}
