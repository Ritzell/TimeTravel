using UnityEngine;
using System.Collections;

public class EntranceRoom : MonoBehaviour {
    [SerializeField]
	private GameObject smoke = null;
	// Use this for initialization
	void Start () {
        StartCoroutine(AirLeak(0.1f,3.5f));
	}
	
	private IEnumerator AirLeak(float MinDelay,float MaxDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(MinDelay,MaxDelay));
            var smokeObject = (GameObject)Object.Instantiate(smoke, new Vector3(Random.Range(-24, 24), 0, Random.Range(-24, 24)), Quaternion.identity);
            smokeObject.transform.eulerAngles = new Vector3(Random.Range(-90,-20),0,0);
        }
    }
}
