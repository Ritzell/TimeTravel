using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private GameObject _touchObject;
	public GameObject TouchObject{
		get{
			return _touchObject;
		}
		set{
			_touchObject = value;
		}
	}


	void OnTriggerEnter(Collider col){
		TouchObject = col.gameObject;
	}

	void OnTriggerExit(Collider col){
		TouchObject = null;

	}

	void GetItem(){
		if (TouchObject) {
			TouchObject.transform.parent = gameObject.transform;
		}
	}
}
