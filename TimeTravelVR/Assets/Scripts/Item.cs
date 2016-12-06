using UnityEngine;
using System.Collections;

public enum ItemType{
	carry,
	talk
}

public class Item : MonoBehaviour {
	public ItemType type;

	public Vector3 CarryPosition;
	private Player player;

	// Use this for initialization
	void Awake () {
		player = FindObjectOfType<Player> ();
	}

	void Start(){

		player.interactionItem.Add (gameObject);
	}
	
	public void Interact(GameObject ob){
		switch (type) {
		case ItemType.carry:
			transform.parent = ob.transform;
			transform.localPosition = CarryPosition;
			break;
		
		default :
			break;
		}
	}

	void OnDestroy(){
		player.interactionItem.Remove (gameObject);
	}
}
