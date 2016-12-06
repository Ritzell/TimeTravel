using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//this script functioning GetRailsType and Run
public class Tram : MonoBehaviour {
	/*public List<AxleInfo> axleInfos; // the information about each individual axle
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float maxSteeringAngle; // maximum steer angle the wheel can have

	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

		foreach (AxleInfo axleInfo in axleInfos) {
			if (axleInfo.steering) {
				axleInfo.leftWheel.steerAngle = steering;
				axleInfo.rightWheel.steerAngle = steering;
			}
			if (axleInfo.motor) {
				axleInfo.leftWheel.motorTorque = motor;
				axleInfo.rightWheel.motorTorque = motor;
			}
		}
	}
}

[System.Serializable]
public class AxleInfo {
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
}*/
	
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
			//transform.Rotate (direction);
			transform.LookAt(col.gameObject.transform);
		}
	}
}
