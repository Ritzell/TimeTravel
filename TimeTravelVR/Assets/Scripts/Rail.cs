using UnityEngine;
using System.Collections;

public enum railsType{
	Straight,
	RightCurve15,
	RightCurve30,
	RightCurve45,
	RightCurve60,
	LeftCurve15,
	LeftCurve30,
	LeftCurve45,
	LeftCurve60,
	none
}

public class Rail : MonoBehaviour {
	public railsType type;
	// Use this for initialization
	void Start () {
	
	}

	//translate forward Everyframe

	public static Vector3 DirectionOfType(railsType type){
		switch (type) {
		case railsType.Straight:
			return new Vector3 (0, 0, 0);
		case railsType.RightCurve15:
			return new Vector3 (0, 5, 0);
		case railsType.LeftCurve15:
			return new Vector3 (0, -15, 0);
		default :
			return Vector3.zero;
		}
	}
}
