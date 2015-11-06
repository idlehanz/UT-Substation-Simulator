using UnityEngine;
using System.Collections;

public class BreakerProperties : MonoBehaviour {

	public float threshhold = 0f;
	public float transmissionBus = 69f;
	
	public float voltage;
	public float frequency;
	public float current;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//reference the PowerlineProperties via powerline
		GameObject pl = GameObject.Find ("Powerline");
		PowerlineProperties powerline = pl.GetComponent<PowerlineProperties> ();

		//ensure voltage is within 5% of accepted transmissionBus range
		voltage = powerline.voltage;
		if ((powerline.voltage >= 1.05 * transmissionBus) || (powerline.voltage <= .95 * transmissionBus)) {
			BreakerTrip ();
		}

		//ensure frequency is within .01% of 60 hertz
		frequency = powerline.frequency;
		if ((frequency >= 60.0001) || (frequency <= 59.9999)){
			BreakerTrip ();
		}

		//ensure current is within threshold
		current = powerline.current;
		if (current > threshhold) {
			BreakerTrip ();
		}


	}

	void OnGUI(){
		GameObject pl = GameObject.Find ("Powerline");
		PowerlineProperties powerline = pl.GetComponent<PowerlineProperties> ();
		GUI.Label (new Rect(10,10, 100, 20), powerline.voltage.ToString());
	}

	void BreakerTrip(){
		//removes connection stops power flow
	}
}
