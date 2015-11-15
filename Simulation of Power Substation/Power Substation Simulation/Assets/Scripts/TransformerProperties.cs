using UnityEngine;
using System.Collections;

public class TransformerProperties : MonoBehaviour {
	public float step = 3.6315f;
	
	public float voltage;
	public float frequency;
	public float current;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject br = GameObject.Find ("Breaker");
		BreakerProperties breaker = br.GetComponent<BreakerProperties> ();

		//pass through variables
		voltage = breaker.voltage;
		frequency = breaker.frequency;
		current = breaker.current;

		voltage = voltage / step;
	}

	void OnGUI(){
		GameObject Player = GameObject.Find("Player");
		RayCasting raycasting = Player.GetComponent<RayCasting>();
		
		if (raycasting.InReach == true && raycasting.hitTag == "Transformer")
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 25), "Transformer Voltage: " + voltage.ToString());
		}
		
		/*
		GameObject pl = GameObject.Find ("Powerline");
		PowerlineProperties powerline = pl.GetComponent<PowerlineProperties> ();
		GUI.Label (new Rect(10, 10, 1000, 20), "Powerline Voltage: " + powerline.voltage.ToString());
		GUI.Label (new Rect(10, 20, 1000, 20), "Breaker Voltage:   " + voltage.ToString());
		*/
	}
}
