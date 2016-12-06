using UnityEngine;
using System.Collections;

public class StoryObject : MonoBehaviour {
	[SerializeField]
	public AudioClip[] clip;
	[SerializeField]
	public AnimationClip[] animclip;

	private int storyNumber;
	private AudioSource source;
	private Animation anim;

	public IEnumerator StoryCoroutine;

	// Use this for initialization
	void Start () {
		if (GetComponent<AudioSource> ()) {
			source = gameObject.GetComponent<AudioSource> ();
		}
		if (GetComponent<Animation> ()) {
			anim = GetComponent<Animation> ();
		}
		StoryCoroutine = Story();
		storyNumber = -1;
	}

	public IEnumerator Story(){
		if (storyNumber++ < clip.Length-1 && clip [storyNumber]) {
			source.clip = clip [storyNumber];
			source.Play ();
		}
		if(storyNumber < animclip.Length-1 && animclip[storyNumber]){
			anim.clip = animclip [storyNumber];
			anim.Play ();
		}
		yield return null;

		if (storyNumber++ < clip.Length-1 && clip [storyNumber]) {
			source.clip = clip [storyNumber];
			source.Play ();
		}
		if(storyNumber < animclip.Length-1 && animclip[storyNumber]){
			anim.clip = animclip [storyNumber];
			anim.Play ();
		}

		TimeMachine machine = FindObjectOfType<TimeMachine> ();
		machine.StartCoroutine (machine.FlashLoadScene (Scenes.medieval));

		yield return null;



		if (storyNumber++ < clip.Length-1 && clip [storyNumber]) {
			source.clip = clip [storyNumber];
			source.Play ();
		}
		if(storyNumber < animclip.Length-1 && animclip[storyNumber]){
			anim.clip = animclip [storyNumber];
			anim.Play ();
		}
		machine.StartCoroutine (machine.FlashLoadScene (Scenes.tram));

		yield return null;
	}
}

