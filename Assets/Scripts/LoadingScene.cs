using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

	private int countClose = 0;
	private float lastClick = 0f;

	// Use this for initialization
	void Start () {
	
	}

	public void LoadScene(string scene) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
	}

	public void forceClose() {
		if (Time.time - lastClick > 1f) {
			countClose = 0;
		}

		countClose++;
		if (countClose == 3) {
			LoadScene ("UiIntroScene");
		}
		lastClick = Time.time;
	}
}
