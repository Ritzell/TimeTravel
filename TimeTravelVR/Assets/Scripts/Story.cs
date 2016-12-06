using UnityEngine;
using System.Collections;


public class Story : MonoBehaviour {
	[SerializeField]
	private GameObject[] objects;

	public static GameObject objective;
	private static int objectsNumber;
	private static Story story;
	private static int checkPoint = 0;

	void Start(){
		story = FindObjectOfType<Story> ();
		objectsNumber = 0;
		objective = objects [objectsNumber];
	}

	public static IEnumerator NextSequential(){
		if (objectsNumber++ < story.objects.Length-1) {
			objective = story.objects [objectsNumber];
		}
		yield return null;
	}
}

