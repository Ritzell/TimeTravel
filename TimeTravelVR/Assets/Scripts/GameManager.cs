using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	void Start(){
		DontDestroyOnLoad (gameObject);
	}

    public float Veje(float t, float p1, float p2, float p3, float p4)
    {
        float pos = (1 - t) * (1 - t) * (1 - t) * p1 + 3 * (1 - t) * (1 - t) * t * p2 + 3 * (1 - t) * t * t * p3 + t * t * t * p4;
        return pos;
    }
}
