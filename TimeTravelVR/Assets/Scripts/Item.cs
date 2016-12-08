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

	[SerializeField]
	private float LimitMaxAngle = 90;
	[SerializeField]
	private float LimitMinAngle = 0;

	public ItemType type;
	public Vector3 CarryPosition;
	private Player player;
	private Coroutine coroutine;

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
			coroutine = StartCoroutine (UpdateTransform(ob));
			break;
		default :
			break;
		}
	}

	public void Release(){
		if(type == ItemType.carry){
		transform.parent = null;
		}
		StopCoroutine (coroutine);
	}

	private IEnumerator UpdateTransform(GameObject parent){
		while (true) {
			Move (parent.transform.position);
			Rotate (parent.transform);
			yield return null;
		}
	}

	public void Move(Vector3 target){
		//座標を保存
		Vector3 origin = transform.position;
		transform.Translate (target.x - origin.x, target.y - origin.y, target.z-origin.z);
		transform.position = new Vector3 (	LockPositionX ? origin.x : transform.position.x,
											LockPositionY  ? origin.y : transform.position.y,
											LockPositionZ ? origin.z : transform.position.z);
	}

	public void Rotate(Transform target){
		//回転角度を保存
		Vector3 origin = transform.eulerAngles;
		transform.LookAt (target,transform.up);
		transform.eulerAngles = new Vector3 (	LockRotateX || transform.eulerAngles.x >= LimitMaxAngle ||  transform.eulerAngles.x <= LimitMinAngle ? origin.x : transform.eulerAngles.x ,
												LockRotateY || transform.eulerAngles.y >= LimitMaxAngle ||  transform.eulerAngles.y <= LimitMinAngle ? origin.y : transform.eulerAngles.y,
												LockRotateZ || transform.eulerAngles.z >= LimitMaxAngle ||  transform.eulerAngles.z <= LimitMinAngle ? origin.z : transform.eulerAngles.z);

	}

	void OnDestroy(){
		player.interactionItem.Remove (gameObject);
	}
}