using UnityEngine;
using System.Collections;

public class BreakerRightProperties : MonoBehaviour {

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
		GameObject pl = GameObject.Find ("Powerline Right");
		PowerlineRightProperties powerline = pl.GetComponent<PowerlineRightProperties> ();

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
			//BreakerTrip ();
		}


	}

	void OnGUI(){
		GameObject Player = GameObject.Find("Player");
		RayCasting raycasting = Player.GetComponent<RayCasting>();
		
		if (raycasting.InReach == true && raycasting.hitTag == "BreakerRight")
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 55), "Breaker Voltage: " + voltage.ToString() + "\n" + 
			        "Breaker Frequency: " + frequency.ToString() + "\nBreaker Current: " + current.ToString());
		}

		/*
		GameObject pl = GameObject.Find ("Powerline");
		PowerlineProperties powerline = pl.GetComponent<PowerlineProperties> ();
		GUI.Label (new Rect(10, 10, 1000, 20), "Powerline Voltage: " + powerline.voltage.ToString());
		GUI.Label (new Rect(10, 20, 1000, 20), "Breaker Voltage:   " + voltage.ToString());
		*/
	}

	void BreakerTrip(){
		//removes connection stops power flow
		voltage = 0;
		frequency = 0;
		current = 0;
	}
}
