/* Defines a Net class for the Unity sim.

The Net class connects components together by dynamically creating ports for them at startup.
Config for this is done by editing the Net.config file.

Each electrical component on the network should have at least 1 Port, usually 2.
See the Port.cs file for the Port class.

//*/

using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Net : MonoBehaviour {
	float atof(string s) { //why isn't this built in?
		float f = 0;
		float f_mag = 1;
		int i;
		bool dp = false;
		bool neg = false;
		
		for (i = 0; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
		if (s[i] == '-') {
			neg = true;
			i++;
		}
		else if (s[i] == '+') i++; //skip a leading + sign if present
		for ( ; i < s.Length; i++) {
			if ((s[i] > '9')||(s[i] < '0')) { //non-numeric char encountered
				if (s[i] == '.') { //decimal encountered
					if (dp) return neg ? -f : f; //second decimal encountered
					dp = true;
					continue;
				}
				return neg ? -f : f;
			}
			else { //number encountered
				if (dp) { //number is to the right of the decimal
					f_mag /= 10;
					f += ((s[i] - '0')*f_mag);
				}
				else { //number is to the left of the decimal
					f *= 10;
					f += (s[i] - '0');
				}
			}
		}
		return neg ? -f : f; //end of string encountered
	}
	
	void Start() {
		GameObject part0, port0_go, part1, port1_go;
		Port port0, port1;
		string[] contents = File.ReadAllLines("Net.config");
		string part_name;
		string port_name;
		float f;
		int i,j,k;
		
		foreach (string s in contents) {
			//line format options:
			//#comment
			//SINK part port current_draw
			//SOURCE part port voltage_supply
			//COND part_0 port_0 part_1 port_1
			
			if (s[0] == '#') continue; //commented line
			if (s.Substring(0,3) == "SINK") { //sink declaration
				for(i = 4; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				part_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				port_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				f = atof(s.Substring(i));
				
				part0 = GameObject.Find("part_name");
				if (part0 == null) { //part0 doesn't exist
					//TODO -- error should be reported
				}
				else { //part0 exists
					//find port0_go if it exists
					port0_go = null;
					for (k = 0; k < part0.transform.childCount; k++) {
						port0_go = part0.transform.GetChild(k).gameObject;
						if ((port0_go)&&(port0_go.name == port_name)) break;
					}
					
					//find port0 in port0_go
					if (port0_go == null) { //port0 doesn't exist
						port0_go = new GameObject();
						port0_go.name = port_name;
						port0 = port0_go.AddComponent<Port>();
					}
					else { //port0 exists
						port0 = port0_go.GetComponent<Port>();
						if (port0 == null) port0 = port0_go.AddComponent<Port>(); //port0_go exists but did not have Port component
					}
					
					port0.MakeSink(f);
				}
			}
			else if (s.Substring(0,5) == "SOURCE") { //source declaration
				for(i = 6; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				part_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				port_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				f = atof(s.Substring(i));
				
				part0 = GameObject.Find("part_name");
				if (part0 == null) { //part0 doesn't exist
					//TODO -- error should be reported
				}
				else { //part0 exists
					//find port0_go if it exists
					port0_go = null;
					for (k = 0; k < part0.transform.childCount; k++) {
						port0_go = part0.transform.GetChild(k).gameObject;
						if ((port0_go)&&(port0_go.name == port_name)) break;
					}
					
					//find port0 in port0_go
					if (port0_go == null) { //port0 doesn't exist
						port0_go = new GameObject();
						port0_go.name = port_name;
						port0 = port0_go.AddComponent<Port>();
					}
					else { //port0 exists
						port0 = port0_go.GetComponent<Port>();
						if (port0 == null) port0 = port0_go.AddComponent<Port>(); //port0_go exists but did not have Port component
					}
					
					port0.MakeSource(f);
				}
			}
			else if (s.Substring(0,3) == "CONN") { //regular connection
				//get port0
				for(i = 4; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				part_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				port_name = s.Substring(i,j);
				
				part0 = GameObject.Find("part_name");
				if (part0 == null) { //part0 doesn't exist
					//TODO -- error should be reported
					port0 = null;
				}
				else { //part0 exists
					//find port0_go if it exists
					port0_go = null;
					for (k = 0; k < part0.transform.childCount; k++) {
						port0_go = part0.transform.GetChild(k).gameObject;
						if ((port0_go)&&(port0_go.name == port_name)) break;
					}
					
					//find port0 in port0_go
					if (port0_go == null) { //port0 doesn't exist
						port0_go = new GameObject();
						port0_go.name = port_name;
						port0 = port0_go.AddComponent<Port>();
					}
					else { //port0 exists
						port0 = port0_go.GetComponent<Port>();
						if (port0 == null) port0 = port0_go.AddComponent<Port>(); //port0_go exists but did not have Port component
					}
				}
				
				//get port1
				for(     ; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				part_name = s.Substring(i,j);
				for(i = j; (((s[i] == ' ')||(s[i] == '\t'))&&(s[i] != '\0')); i++); //skip whitespace
				for(j = i; (((s[j] != ' ')&&(s[j] != '\t'))&&(s[i] != '\0')); j++); //select nonwhitespace
				port_name = s.Substring(i,j);
				
				part1 = GameObject.Find("part_name");
				if (part1 == null) { //part1 doesn't exist
					//TODO -- error should be reported
					port1 = null;
				}
				else { //part1 exists
					//find port1_go if it exists
					port1_go = null;
					for (k = 0; k < part1.transform.childCount; k++) {
						port1_go = part1.transform.GetChild(k).gameObject;
						if ((port1_go)&&(port1_go.name == port_name)) break;
					}
					
					//find port1 in port1_go
					if (port1_go == null) { //part1 doesn't exist
						port1_go = new GameObject();
						port1_go.name = port_name;
						port1 = port1_go.AddComponent<Port>();
					}
					else { //part1 exists
						port1 = port1_go.GetComponent<Port>();
						if (part1 == null) port1 = port1_go.AddComponent<Port>(); //port0_go exists but did not have Port component
					}
				}
				
				if (((port0 != null)&&(port1 != null)&&(port0 != port1))) port0.ConnectTo(port1);
			}
			else {
				//TODO
				//if blank line, okay.  Otherwise, error.
				//Error - invalid line
			}
		}
	}

	void Update() {
		//TODO -- everything
	}
}