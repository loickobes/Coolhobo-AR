using UnityEngine;
using System.Collections;

public class SetCameraSettings : MonoBehaviour {

	private CameraSettings settings;

	private bool initialized = false;

	// Use this for initialization
	void Start () {
		settings = this.GetComponent<CameraSettings> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!initialized) {
			settings.SwitchAutofocus (true);
			initialized = true;
		}
	}
}
