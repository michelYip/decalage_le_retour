using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DC_moveJaune: MonoBehaviour {
    
	[SerializeField]
    private Transform rig;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;
    private Vector2 axis = Vector2.zero;

    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update() {
        if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (controller.GetTouch(touchpad))
        {
            float posY = rig.position.y;
            rig.position += ((transform.right + transform.forward) *(Time.deltaTime+0.5f));
            Vector3 newPos = new Vector3(rig.position.x, posY, rig.position.z);
            rig.position = newPos;
        }

    }
}