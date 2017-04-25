using UnityEngine;
using System.Collections;

public class RandomVoucher : MonoBehaviour {

	public GameObject[] targets;
	public GameObject tryAgain;
	public GameObject founded;

	private string luckyTarget;

	public StatsManagement stats;

	// Use this for initialization
	void Start () {
		luckyTarget = targets[Random.Range (0, targets.Length)].name;
		tryAgain.SetActive (false);
		founded.SetActive (false);

		Debug.Log ("LuckTarget: " + luckyTarget);
	}
	
	public void foundTarget(string targetName) {
		Debug.Log ("FoundTarget: " + targetName);
		if (targetName == luckyTarget) {
			founded.SetActive (true);
			stats.FoundVoucher (targetName);
		} else {
			tryAgain.SetActive (true);
		}
	}
}
