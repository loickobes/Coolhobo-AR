using UnityEngine;
using System.Collections;

public class AutoRellover : MonoBehaviour {
	private float firstPage = 2160f;
	private float lastPage = -2160f;
	private float pageWidth = 1080f;

	public float timeout = 3f;
	public float speed = 3f;
	private float lastFrame = 0f;
	private float currentPos;
	private float targetPos;
	private float position;
	private bool animate = false;

	public CloseMenu menu;
	public GameObject menuAppreciation;
	public StatsManagement stats;

	// Use this for initialization
	void Start () {
		this.transform.localPosition = new Vector3 (firstPage, 0, 0);
		currentPos = firstPage;
		targetPos = firstPage;
		position = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (animate) {
			if (position < 1f) {
				position += Time.deltaTime * speed;
				this.transform.localPosition = new Vector3 (Mathf.Lerp(currentPos, targetPos, position) , 0, 0);
			}

			if (Time.time - lastFrame > timeout) {
				nextPage ();
			}
		}
	}

	public void Reset() {
		this.transform.localPosition = new Vector3 (firstPage, 0, 0);
		currentPos = firstPage;
		targetPos = firstPage;
		position = 1f;
	}

	public void StartAnimation() {
		animate = true;
		lastFrame = Time.time;
	}

	public void StopAnimation() {
		animate = false;
	}

	public void nextPage() {
		currentPos = targetPos;
		targetPos -= pageWidth;
		if (targetPos < lastPage) {
			StopAnimation ();
			menu.SetCloseMenu ();	
			menuAppreciation.SetActive (true);
			stats.CloseDescription ();
		} else {
			position = 0f;
		}
		lastFrame = Time.time;
	}
}
