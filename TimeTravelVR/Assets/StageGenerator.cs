using UnityEngine;
using System.Collections;

public class StageGenerator : MonoBehaviour {

	[SerializeField]
	private int LoadDistance = 800;
	[SerializeField]
	private int GeneratorRange = 2;
	public GameObject[] Stages;
	private GameObject player;

	private int nowStage = 0;
	private int minStage = 0;

	void Start(){
		player = FindObjectOfType<Player> ().gameObject;
	}

	// Update is called once per frame
	void Update () {
		float RearDistance = minStage <= Stages.Length - 1 ? Mathf.Abs(Vector3.Distance (Stages [minStage].transform.position, player.transform.position)) : 0;
		float ForwardDistance = nowStage + GeneratorRange <= Stages.Length - 1 ? Mathf.Abs (Vector3.Distance (Stages [nowStage + GeneratorRange].transform.position, player.transform.position)) : Mathf.Infinity;

		if (ForwardDistance < LoadDistance) {
			Stages [nowStage+GeneratorRange].SetActive (true);
			nowStage++;
		}
		if (RearDistance > LoadDistance) {
			Stages [minStage].SetActive (false);
			minStage++;
		}
	}
}
