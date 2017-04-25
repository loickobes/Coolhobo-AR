using UnityEngine;
using System.Collections;
using Vuforia;

public class DisplayUi : MonoBehaviour, ITrackableEventHandler {

	public GameObject menu;
	private TrackableBehaviour mTrackableBehaviour;

	private bool uiOpened = false;

	public StatsManagement stats;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		menu.SetActive (false);
	}
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			if (!uiOpened) {
				menu.SetActive (true);
				uiOpened = true;
				stats.FoundTarget (this.gameObject.name);
				this.gameObject.SetActive (false);
			}
		}
		else
		{

		}
	}   
}
