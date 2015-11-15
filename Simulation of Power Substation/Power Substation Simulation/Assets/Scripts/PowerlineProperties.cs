using UnityEngine;
using System.Collections;

public class PowerlineProperties : MonoBehaviour {

	public float frequency = 60f;//hertz, HAS TO BE 60!
	public float current;
	public float lineImpedance = 34.5f;
	public float voltage = 69f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		current = voltage / lineImpedance; 
	}

	void OnGUI(){
		GameObject Player = GameObject.Find("Player");
		RayCasting raycasting = Player.GetComponent<RayCasting>();
		
		if (raycasting.InReach == true && raycasting.hitTag == "Powerline")
		{
			GUI.color = Color.white;
			GUI.Box(new Rect(20, 20, 200, 40), "Powerline Voltage: " + voltage.ToString() + 
			        "\nPowerline Frequency: " + frequency.ToString());
		}
		

	}

}
