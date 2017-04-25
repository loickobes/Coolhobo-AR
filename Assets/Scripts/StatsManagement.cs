using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsManagement : MonoBehaviour {

	public GameObject ageContainer;
	public GameObject sexContainer;
	private Toggle[] statsAge;
	private Toggle[] statsSex;

	public int testNum = -1;

	//private SqliteDbManager sqliteManager;

	// Use this for initialization
	void Start () {
		if (ageContainer != null) {
			statsAge = ageContainer.GetComponentsInChildren<Toggle> ();
		} else {
			statsAge = new Toggle[0];
		}

		if (sexContainer != null) {
			statsSex = sexContainer.GetComponentsInChildren<Toggle> ();
		} else {
			statsSex = new Toggle[0];
		}

		//sqliteManager = this.GetComponent<SqliteDbManager> ();

		SqliteDbManager.Init ();

		if (testNum != -1) {
			StatTest (testNum);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void StartSession() {
		AppManager.sessionId = Random.Range (10000, 99999);
		ShowSessionId ();
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "StartSession", "");
		AppManager.startSessionTime = Time.time;
	}

	public void StopSession() {
		if (AppManager.startTestTime != 0f) {
			StopTest ();
		}

		float duration = Time.time - AppManager.startSessionTime;
		AppManager.startSessionTime = 0f;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "StopSession", duration.ToString());
	}

	public void ShowSessionId() {
		Debug.Log ("SessionId: " + AppManager.sessionId);
	}

	public void ValidateAge() {
		StartSession ();

		foreach (Toggle age in statsAge) {
			if (age.isOn) {
				SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Age", age.gameObject.name);
			}
		}
	}

	public void ValidateSex() {
		foreach (Toggle sex in statsSex) {
			if (sex.isOn) {
				SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Sex", sex.gameObject.name);
			}
		}
	}

	public void StatTest(int nb) {
		if (AppManager.startTestTime != 0f) {
			StopTest ();
		}

		AppManager.startTestTime = Time.time;
		AppManager.nbTargetFound = 0;
		AppManager.currentTest = nb;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "StartTest_"+nb.ToString(), "");
	}

	public void StopTest() {
		if (AppManager.foundTargetTime != 0f) {
			LostTarget ();
		}

		float duration = Time.time - AppManager.startTestTime;
		AppManager.startTestTime = 0f;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "NbTargetFound_"+AppManager.currentTest, AppManager.nbTargetFound.ToString());
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "StopTest_"+AppManager.currentTest.ToString(), duration.ToString());
	}

	public void FoundTarget(string name) {
		AppManager.foundTargetTime = Time.time;
		AppManager.nbTargetFound++;
		AppManager.currentTargetName = name;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "FoundTarget", name);
	}

	public void LostTarget() {
		if (AppManager.currentTargetName != "") {
			float duration = Time.time - AppManager.foundTargetTime;
			AppManager.foundTargetTime = 0f;
			SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "LostTarget_" + AppManager.currentTargetName, duration.ToString ());
			AppManager.currentTargetName = "";
		}
	}

	public void Like() {
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Result_"+AppManager.currentTest, "like");
	}

	public void DontLike() {
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Result_"+AppManager.currentTest, "dont-like");
	}

	public void OpenDescription(string content) {
		AppManager.startDescriptionTime = Time.time;
		AppManager.currentDescription = content;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "OpenDescription", content);
	}

	public void CloseDescription() {
		float duration = Time.time - AppManager.startDescriptionTime;
		AppManager.startDescriptionTime = 0f;
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "CloseDescription_"+AppManager.currentDescription, duration.ToString());
	}

	public void LikeDescription() {
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Result_"+AppManager.currentDescription+AppManager.currentTest, "like");
	}

	public void DontLikeDescription() {
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "Result_"+AppManager.currentDescription+AppManager.currentTest, "dont-like");
	}

	public void FoundVoucher(string target) {
		SqliteDbManager.insertStat ("", AppManager.sessionId.ToString (), "FoundVoucher", target);
	}
}
