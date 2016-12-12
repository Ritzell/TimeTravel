using UnityEngine;
using System.Collections;

public class ElectricalSwitch : MonoBehaviour {
	private Light light;
	// Use this for initialization
	void Start () {
		light.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	public void  TurnOff() {
		light.enabled = false;
	}

	public void TurnOn(){
		light.enabled = true;
	}
}
