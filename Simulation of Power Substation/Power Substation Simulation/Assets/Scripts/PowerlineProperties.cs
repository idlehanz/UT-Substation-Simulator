using UnityEngine;
using System.Collections;

public class PowerlineProperties : MonoBehaviour {

	public float powerFrequency = 60f;//hertz, HAS TO BE 60!
	public float current;
	public float lineImpedance = 34.5f;
	public float voltage = 69f;

	// Use this for initialization
	void Start () {
		voltage = current * lineImpedance;

	}
	
	// Update is called once per frame
	void Update () {
		current = voltage / lineImpedance; 
	}
}
