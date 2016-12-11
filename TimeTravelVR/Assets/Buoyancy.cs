using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	//浮力=pvg
	[SerializeField]
	private AudioClip clip,clip2;

	[SerializeField]
	private AudioSource source;

	void OnTriggerEnter(Collider col){
		foreach (AudioSource audio in FindObjectsOfType<AudioSource>()) {
			audio.pitch = 0.2f;
			audio.volume = 0.5f;
		}
		source.clip = clip2;
		source.Play ();
	}

	void OnTriggerStay(Collider col){
		var StaySize = Mathf.Clamp(col.gameObject.transform.lossyScale.y - (col.gameObject.transform.position.y - transform.position.y),0,1);
		float f = 9f * StaySize * 0.98f;
		col.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * f,ForceMode.Acceleration);
	}

	void OnTriggerExit(Collider col){
		foreach (AudioSource audio in FindObjectsOfType<AudioSource>()) {
			audio.pitch = 1;
			audio.volume = 1f;
		}
		source.clip = clip;
		source.Play ();
	}
}
