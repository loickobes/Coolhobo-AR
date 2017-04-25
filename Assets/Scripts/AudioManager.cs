using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
		
	
	public void StartAudio() {
		audio = this.GetComponent<AudioSource> ();
		audio.Stop ();
		audio.time = 0f;
		audio.Play ();
	}

	public void StopAudio() {
		audio = this.GetComponent<AudioSource> ();
		audio.Stop ();
	}
}
