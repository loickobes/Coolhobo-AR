using UnityEngine;
using System.Collections;
using Vuforia;

public class RandomObject : MonoBehaviour, ITrackableEventHandler {

	public GameObject[] models;

	public StatsManagement stats;
	private int previousModel = -1;
	private TrackableBehaviour mTrackableBehaviour;

	// Use this for initialization
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		foreach (GameObject obj in models) {
			obj.SetActive (false);
		}
	}
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			int rndObj;

			do {
				rndObj = Random.Range (0, models.Length);
			} while (previousModel == rndObj);

			stats.FoundTarget (models [rndObj].name);
			models [rndObj].SetActive (true);
			previousModel = rndObj;
		}
		else
		{
			stats.LostTarget ();
			foreach (GameObject obj in models) {
				obj.SetActive (false);
			}
		}
	}   
}
