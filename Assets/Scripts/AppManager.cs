using UnityEngine;
using System.Collections;

public class AppManager {

	static public int sessionId = -1;
	static public float startSessionTime = 0f;
	static public float startTestTime = 0f;
	static public float foundTargetTime = 0f;
	static public int nbTargetFound = 0;
	static public int currentTest = 0;
	static public float startDescriptionTime = 0f;
	static public string currentDescription = "";
	static public string currentTargetName = "";
}
