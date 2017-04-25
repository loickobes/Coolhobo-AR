using UnityEngine;
using System.Collections;

public class CloseMenu : MonoBehaviour {

	public bool autoClose = false;
	public float delay = 4f;

	// Use this for initialization
	void Start () {
	
	}

	void OnEnable() {
		if (autoClose) {
			StartCoroutine(AutoClose());
		}
	}

	IEnumerator AutoClose()
	{
		yield return new WaitForSeconds(delay);
		this.gameObject.SetActive(false);
	}

	public void SetCloseMenu() {
		this.gameObject.SetActive(false);
		StopCoroutine("AutoClose"); 
	}
}
