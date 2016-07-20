using UnityEngine;
using System.Collections;

public class OutputLeftProperties : MonoBehaviour {

	public float frequency;//hertz, HAS TO BE 60!
	public float current;
	public float lineImpedance;
	public float voltage;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		//current = voltage / lineImpedance; 
	}

	void OnGUI(){
		GameObject Player = GameObject.Find("Player");
		RayCasting raycasting = Player.GetComponent<RayCasting>();
		
		if (raycasting.InReach == true && raycasting.hitTag == "OutputLeft")
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 55), "Powerline Voltage: " + voltage.ToString() + 
			        "\nPowerline Frequency: " + frequency.ToString() + "\nPowerline Current: " + current.ToString());
		}
		

	}

}
