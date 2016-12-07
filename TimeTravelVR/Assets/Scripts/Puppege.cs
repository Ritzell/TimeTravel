using UnityEngine;
using System.Collections;
using System;

public class Puppege : MonoBehaviour {
    private enum CoroutineNumber
    {
        excitement,
        naturalLookAt,
        UpdateEyeLine
    }

    [SerializeField]
	private GameObject Player;
	[SerializeField]
	private GameObject PlayerCamera;
    
	private AudioSource audiosource;
	public Texture[] Expressions;
    private static GameManager Manager;
    Vector3 EyeLine;
    private static bool WatchingMyself = false;


    private static Coroutine[] coroutines = new Coroutine[Enum.GetNames(typeof(CoroutineNumber)).Length];
    // Use this for initialization
    void Awake () {
		audiosource = GetComponent<AudioSource> ();
	}

	void Start(){
		DontDestroyOnLoad (gameObject);
        Manager = FindObjectOfType<GameManager>();
        Resurrection();
		//StartCoroutine(Nod (0.8f));
    }

	public IEnumerator RequestSightAnim(){
		float time = 0;
        StopCoroutine(coroutines[(int)CoroutineNumber.naturalLookAt]);
		while (time < 53) {
			if (WatchingMyself) {
				if (time <= 0) {
					audiosource.Play ();
				}
				time += Time.deltaTime;
				transform.RotateAround (Player.transform.position, Vector3.up, 5 * Time.deltaTime);
				transform.LookAt (Player.transform);
			} else {
				audiosource.Stop ();
				time = 0;
			}
			yield return null;
		}
		audiosource.Stop ();
		coroutines[(int)CoroutineNumber.naturalLookAt] = StartCoroutine(NaturalLookAt(0.5f, PlayerCamera));
        yield return null;
	}

	private IEnumerator excitement(){
		float[] AudioWaveDate = new float[30];
		while (true) {
			audiosource.GetOutputData(AudioWaveDate,1);
			float wavePoint = 0;
			for (int count = 0; count < AudioWaveDate.Length; count++) {
				wavePoint += AudioWaveDate [count];
			}
			wavePoint = wavePoint / AudioWaveDate.Length;
			if (wavePoint > 0.06f) {
				GetComponent<Renderer> ().material.SetTexture ("_EmissionMap", Expressions [2]);
				yield return new WaitForSeconds (0.3f);
			} else if (wavePoint > 0.008f) {
                GetComponent<Renderer> ().material.SetTexture ("_EmissionMap", Expressions [1]);
				yield return new WaitForSeconds (0.4f);
			} else {
				GetComponent<Renderer> ().material.SetTexture ("_EmissionMap", Expressions [0]);

			}
            yield return null;
		}
	}

	public IEnumerator NaturalLookAt(float time, GameObject tgt){
        while (true)
        {
            if (!WatchingMyself)
            {
                yield return null;
                continue;
            }
            Quaternion StartRot = transform.rotation;
            transform.LookAt(tgt.transform);
            Quaternion EndRot = transform.rotation;
            transform.rotation = StartRot;
            for (float t = 0; t < time; t += Time.deltaTime)
            {
                float rt = Manager.Veje(t, 0, 1, 0.5f, 1);
                transform.rotation = Quaternion.Slerp(StartRot, EndRot, rt);
                yield return null;
            }
        }
    }

	public void Resurrection(){
        coroutines[(int)CoroutineNumber.excitement] = StartCoroutine(excitement());
		coroutines[(int)CoroutineNumber.naturalLookAt] = StartCoroutine(NaturalLookAt(0.5f, PlayerCamera));
        coroutines[(int)CoroutineNumber.UpdateEyeLine] = StartCoroutine(UpdateEyeLine());

    }

    public void StopFunction()
    {
        foreach (Coroutine coroutine in coroutines)
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator UpdateEyeLine()
    {
        while (true)
        {
            EyeLine = Camera.main.WorldToViewportPoint(transform.position);
            if(EyeLine.z > -0.1f && EyeLine.x >= 0.4f && EyeLine.y >= 0.4f && EyeLine.x <= 0.6f && EyeLine.y <= 0.6f)
            {
                WatchingMyself = true;
            }else
            {
                WatchingMyself = false;
            }
            yield return null;
        }
    }

	public IEnumerator Nod(float time){
		yield return new WaitForSeconds(4);
		iTween.RotateAdd (gameObject, iTween.Hash ("x", 30, "easeType","easeInOutBack","time", time));
		yield return new WaitForSeconds (time);
		iTween.RotateAdd (gameObject, iTween.Hash ("x", -30, "easeType","linearTween","time", time/1.5f));
		yield return null;

	}
}
