using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour {
 /* 
	public Transform head;
	public SteamVR_TrackedObject leftHand;
	public SteamVR_TrackedObject rightHand;

	private bool isFlying = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		var lDevice = SteamVR_Controller.Input((int)leftHand.index);
		var rDevice = SteamVR_Controller.Input((int)rightHand.index);
		if ( lDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) || rDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) ) {
			isFlying = !isFlying;
		}
			Vector3 direction = transform.forward;
			direction.y = 0;
			direction.Normalize();
			transform.Translate(direction * Time.deltaTime * 10f, Space.World);
		if (isFlying) {
			Vector3 rightDir = rightHand.transform.position - head.position;
			Vector3 leftDir = leftHand.transform.position - head.position;

			Vector3 dir = leftDir + rightDir;

			//transform.position += dir;
		}
	}*/
}
