/* Defines a Port class for the Unity sim.

Each electrical component on the network should have at least 1 Port, usually 2.
This is done by editing the Net.config file.
Also see the Net.cs file for the Net class that connects the components at startup.

//*/

using UnityEngine;
using System.Collections;

public class Port : MonoBehaviour {
	private Port Connection;
	private bool is_connected = false;
	private bool is_source = false; //voltage source
	private bool is_sink = false; //current sink
	private float voltage = 0;
	private float current = 0;

	void Start() {
		
	}

	void Update() {
		if (is_connected) {
			if (is_source) {
				//voltage is constant
				current = -(Connection.GetCurrent()); //current depends on supply
			}
			if (is_sink) {
				voltage = Connection.GetVoltage(); //voltage depends on supply
				//current is constant
			}
			if (!is_source && !is_sink) { //conductor -- both current and voltage are dependent
				voltage = Connection.GetVoltage();
				current = -(Connection.GetCurrent()); //current is opposite in sign to other side of connection
			}
		}
	}

	public void ConnectTo(Port p) {
		if (is_connected) this.Disconnect();
		Connection = p;
		if (p.Connection != this) p.ConnectTo(this);
		is_connected = true;
	}

	public void Disconnect(bool disconnect_other_side = true) {
		if (is_connected) {
			if (disconnect_other_side) Connection.Disconnect(false);
			is_connected = false;
		}
	}
	
	public void MakeSource(float v) { voltage = v; current = 0; is_source = true; is_sink = false; }		
	public void MakeSink(float c) { voltage = 0; current = c; is_source = false; is_sink = true; }
	public void MakeConductor() { is_source = false; is_sink = false; }
	public float GetCurrent() { return current; }
	public float GetVoltage() { return voltage; }
}