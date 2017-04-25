using UnityEngine;
using System.Collections;
using Vuforia;
public class RandomVoucherTrigger : MonoBehaviour, ITrackableEventHandler {

		public RandomVoucher voucher;
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
		}

		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				voucher.foundTarget (this.gameObject.name);
			stats.FoundTarget (this.gameObject.name);
			Debug.Log ("Found Target: " + this.gameObject.name);
			}
			else
			{
			stats.LostTarget ();
			}
		}   
	}