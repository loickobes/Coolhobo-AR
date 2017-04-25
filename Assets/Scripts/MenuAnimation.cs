using UnityEngine;
using System.Collections;

public class MenuAnimation : MonoBehaviour {

	#region PRIVATE_MEMBERS
	private Vector3 page1X = new Vector3(1080f, 0, 0);
	private Vector3 page2X = new Vector3(0f, 0, 0);
	private Vector3 page3X = new Vector3(-1080f, 0, 0);
	private Vector3 currentPosition = Vector3.zero;
	private Vector3 targetPosition = Vector3.zero;
	private int currentPage = 1;
	private int targetPage = 1;
	private float position = 0;
	#endregion //PRIVATE_MEMBERS


	#region PUBLIC_PROPERTIES
	[Range(0,4)]
	public float SlidingTime = 0.3f;// seconds
	#endregion //PUBLIC_PROPERTIES


	#region MONOBEHAVIOUR_METHODS
	void Start () 
	{
		//page1X = new Vector3 (Screen.width, 0, 0);
		//page2X = new Vector3(0f, 0, 0);
		//page1X = new Vector3 (-Screen.width, 0, 0);

		//RectTransform rt = this.GetComponent<RectTransform>();
		//rt.sizeDelta = new Vector2(Screen.width*3, Screen.height);

		Debug.Log ("ScreenWidth: " + Screen.width);

		this.transform.localPosition = page1X;

		currentPosition = page1X;
		targetPosition = page1X;
	}
	

	void Update () 
	{
		if (currentPage != targetPage) {
			if (position < 1f) {
				position += Time.deltaTime * SlidingTime;
				this.transform.localPosition = Vector3.Lerp (currentPosition, targetPosition, position);
			} else {
				currentPage = targetPage;
				currentPosition = targetPosition;
			}
		}
	}
	#endregion //MONOBEHAVIOUR_METHODS


	#region PUBLIC_METHODS
	public void SetPage(int page) {
		targetPage = page;
		position = 0f;

		switch (page) {
		case 1:
			{
				targetPosition = page1X;
				break;
			}
		case 2:
			{
				targetPosition = page2X;
				break;
			}
		case 3:
			{
				targetPosition = page3X;
				break;
			}
		};
	}

	public void ForcePage(int page) {
		currentPage = targetPage = page;
		position = 1f;

		switch (page) {
		case 1:
			{
				currentPosition = page1X;
				targetPosition = page1X;
				break;
			}
		case 2:
			{
				currentPosition = page2X;
				targetPosition = page2X;
				break;
			}
		case 3:
			{
				currentPosition = page3X;
				targetPosition = page3X;
				break;
			}
		};

		this.transform.localPosition = currentPosition;
	}
	#endregion //PUBLIC_METHODS
}
