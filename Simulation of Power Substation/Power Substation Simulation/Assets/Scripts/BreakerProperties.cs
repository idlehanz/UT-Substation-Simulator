using UnityEngine;
using System.Collections;

public class BreakerProperties : MonoBehaviour {

	public float transmissionBus = 69f;
	//GameObject frequency = PowerlineProperties;
	//public PowerlineProperties powerline;
	//powerline = GameObject.GetComponent<PowerlineProperties>();

	// Use this for initialization
	void Start () {
		//reference the PowerlineProperties via powerline
		GameObject pl = GameObject.Find ("Powerline");
		PowerlineProperties powerline = pl.GetComponent<PowerlineProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		//checks voltage, frequency, current

		//ensure voltage is within 5% of accepted range (69 kV)
		if ((powerline.voltage != .95 * 69) || (powerline.voltage != 1.05 * 69)) {
			//TRIPS ITSELF -> Shuts down breaker
		}
	}
}
