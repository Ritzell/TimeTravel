using UnityEngine;
using System.Collections;

public enum ItemType{
	carry,
	talk,
	controll
}

public class Item : MonoBehaviour {
	[SerializeField]
	private bool LockPositionX = false;
	[SerializeField]
	private bool LockPositionY = false;
	[SerializeField]
	private bool LockPositionZ = false;
	[SerializeField]
	private bool LockRotateX = false;
	[SerializeField]
	private bool LockRotateY = false;
	[SerializeField]
	private bool LockRotateZ = false;

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
		case ItemType.controll:
			transform.parent = ob.transform;
			break;
		default :
			break;
		}
	}

	public void Move(Vector3 target){
		//座標を保存
		Vector3 origin = transform.position;
		transform.Translate (origin.x-target.x,origin.y - target.y,origin.z - target.z);
		transform.position = new Vector3 (	LockPositionX ? origin.x : transform.position.x,
											LockPositionY  ? origin.y : transform.position.y,
											LockPositionZ ? origin.z : transform.position.z);
	}

	public void Rotate(Transform target){
		//回転角度を保存
		Vector3 origin = transform.eulerAngles;
		transform.LookAt (target);
		transform.eulerAngles = new Vector3 (	LockRotateX ? origin.x : transform.eulerAngles.x ,
												LockRotateY ? origin.y : transform.eulerAngles.y,
												LockRotateZ ? origin.z : transform.eulerAngles.z);
	}

	void OnDestroy(){
		player.interactionItem.Remove (gameObject);
	}
}