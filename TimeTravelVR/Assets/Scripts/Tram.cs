using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this script functioning GetRailsType and Run
public class Tram : MonoBehaviour {
	[SerializeField]
	private float Speed;
	private static railsType GetOnRailType;

	void Start(){
		StartCoroutine (AlongTheRail ());
	}

	public IEnumerator AlongTheRail(){
		while (true) {
			transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			yield return null;
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.GetComponent<Rail>()){
			GetOnRailType = col.gameObject.GetComponent<Rail> ().type;
			Vector3 direction = Rail.DirectionOfType (GetOnRailType);
			transform.LookAt(col.gameObject.transform);
		}
	}
}
