using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour {
    void OnGUI() {
        if (Event.current.Equals(Event.KeyboardEvent("return")))
			SceneManager.LoadScene("main");
    }
}
