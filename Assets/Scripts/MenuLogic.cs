using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuLogic : MonoBehaviour {

	#region PRIVATE_PROPERTIES
	private ToggleGroup[] toogleGroupContainer;
	#endregion //PRIVATE_MEMBERS

	// Use this for initialization
	void Start () {
		toogleGroupContainer = this.GetComponentsInChildren<ToggleGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region PUBLIC_METHODS
	public void resetForms() {
		foreach (ToggleGroup toggleGroup in toogleGroupContainer) {
			toggleGroup.SetAllTogglesOff ();
		}
	}
	#endregion //PUBLIC_METHODS
}
