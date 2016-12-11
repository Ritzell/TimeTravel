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

	private GameObject InteractItem;


	void OnTriggerEnter(Collider col){
		TouchObject = col.gameObject;
	}

	void OnTriggerExit(Collider col){
		TouchObject = null;

	}

	public void GetItem(){
		if (TouchObject) {
			if (TouchObject != Story.objective) {
				TouchObject.GetComponent<Item> ().Interact (gameObject);
				InteractItem = TouchObject;
			} else {
				TouchObject.GetComponent<StoryObject> ().StoryCoroutine.MoveNext ();
				Story.NextSequential ().MoveNext ();
			}
		}
	}

	public void ReleaseItem(){
		if (InteractItem) {
			InteractItem.GetComponent<Item> ().Release ();
			InteractItem = null;
		}
	}
}
