using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum Scenes{
	entrance,
	medieval,
	tram
}

public class TimeMachine : MonoBehaviour {

	[SerializeField]
	private Player player;

	public IEnumerator FlashLoadScene(Scenes scene)
	{
		bool isOut = false;
		player.StartCoroutine(player.Flash(3f,  1, SceneManager.GetActiveScene().name, fadeout => isOut = fadeout));
		while (!isOut) {
			yield return null;
		}
		SceneManager.LoadSceneAsync((int)scene);
		yield return null;
	}
}
