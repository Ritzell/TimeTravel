using UnityEngine;
using System.Collections;

public class SoulGenerator : MonoBehaviour {
	[SerializeField]
	private int SpawnCount;
	[SerializeField]
	private GameObject soul;
	// Use this for initialization
	void Start () {
		for (int count = 0; count < SpawnCount; count++) {
			var InstantSoul = (GameObject)Instantiate(soul,new Vector3(Random.Range(-100,100),Random.Range(-100,100),Random.Range(-100,100)),Quaternion.identity);
			InstantSoul.transform.parent = gameObject.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
