using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilPortScript : MonoBehaviour, Interactable {

	public GameObject syringe;
	public Material filled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void interact(GameObject interactor) {
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		inventory.currentTool.syringefull = true;
		syringe.GetComponent<Renderer>().material = filled;
	}

	public void displayInteractionMessage(GameObject interactor)
	{
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		//draw a box containing relevant information about the transformer.
		if ( inventory.currentTool != null ) {
			if ( inventory.currentTool.toolName == "Syringe" ) {
				GUI.color = Color.white;
				GUI.Box(new Rect((Screen.width / 2) - 100, 20, 200, 55), "Press e to extract oil");
			}
		}
	}
}
