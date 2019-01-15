using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCCreator: MonoBehaviour
{

    [SerializeField]
    private Transform[] prefabObj;

    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;


    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (controller == null) {
            Debug.Log("Controller not initialized");
            return;
        }
        if (controller.GetTouchDown(trigger)) {
            int r = Random.Range(0, prefabObj.Length);
			Instantiate (prefabObj[r], transform.position, transform.rotation);
		}
    }

}