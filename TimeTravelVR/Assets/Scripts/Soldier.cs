using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Soldier : MonoBehaviour {
	private static float Speed = 10;
	private static List<GameObject> soldiers = new List<GameObject>();
	// Use this for initialization
	void Awake(){
		soldiers.Add (gameObject);
	}

	//transform.Translate (Vector3.forward*Speed*Time.deltaTime);
	public static IEnumerator SoldierAI(){
		while (true) {
			int count = 0;
			for (; count < soldiers.Count / 3; count++) {
				soldiers [count].transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			}
			yield return null;
			for (; count < soldiers.Count / 2; count++) {
				soldiers [count].transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			}
			yield return null;
			for (; count < soldiers.Count; count++) {
				soldiers [count].transform.Translate (Vector3.forward * Speed * Time.deltaTime);
			}
			yield return null;
		}
	}
	
}
