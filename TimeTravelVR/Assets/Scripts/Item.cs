using UnityEngine;
using System.Collections;
using UnityEditor;

public enum ItemType{
	carry,
	talk,
	controll
}

public class Item : MonoBehaviour {
	[SerializeField]
	public bool LockPositionX = false;
	[SerializeField]
	public bool LockPositionY = false;
	[SerializeField]
	public bool LockPositionZ = false;
	[SerializeField]
	public bool LockRotateX = false;
	[SerializeField]
	public bool LockRotateY = false;
	[SerializeField]
	public bool LockRotateZ = false;

	[SerializeField]
	public string[] MethodName = new string[2];
	[SerializeField]
	private GameObject SendObject;

	[SerializeField]
	public float LimitMaxAngle = 90;
	[SerializeField]
	public float LimitMinAngle = 0;

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
		if (coroutine != null) {
			StopCoroutine (coroutine);
		}
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
		transform.eulerAngles = new Vector3 (	LockRotateX || transform.eulerAngles.x >= LimitMaxAngle ||  transform.eulerAngles.x <= LimitMinAngle ? Limit(LockRotateX,transform.eulerAngles.x,origin.x) : transform.eulerAngles.x ,
												LockRotateY || transform.eulerAngles.y >= LimitMaxAngle ||  transform.eulerAngles.y <= LimitMinAngle ? Limit(LockRotateY,transform.eulerAngles.y,origin.y)  : transform.eulerAngles.y,
												LockRotateZ || transform.eulerAngles.z >= LimitMaxAngle ||  transform.eulerAngles.z <= LimitMinAngle ? Limit(LockRotateZ,transform.eulerAngles.z,origin.z) : transform.eulerAngles.z);

	}

	/// <summary>
	/// If value"End" is over limit,sendMessage to Object and return origin.
	/// </summary>
	/// <param name="Lock">If set to <c>true</c> lock.</param>
	/// <param name="eulerAngle">Euler angle.</param>
	/// <param name="origin">Origin.</param>
	float Limit(bool Lock, float End,float origin){
		if(Lock){
			return origin;
		}
		if (End >= LimitMaxAngle) {
			SendObject.SendMessage (MethodName [0]);
		} else {
			SendObject.SendMessage(MethodName[1]);
		}
		return origin;
	}



	void OnDestroy(){
		player.interactionItem.Remove (gameObject);
	}

}
