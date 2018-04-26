using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolWall : MonoBehaviour, Interactable {

	public GameObject scraper;
	public GameObject scraperplace;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void interact(GameObject interactor) {
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		if ( inventory.currentTool.toolName == "Scraper" ) {
			inventory.currentTool.parentObject = null;
			inventory.currentTool.transform.parent = null;
			inventory.currentTool = null;
			Vector3 returnpt = scraperplace.transform.position;
			scraper.transform.position = returnpt;
			scraper.transform.rotation = scraperplace.transform.rotation;
		}
	}
	public void displayInteractionMessage(GameObject interactor) {
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		if ( inventory.currentTool != null ) {
			if ( inventory.currentTool.toolName == "Scraper") {
				GUI.color = Color.white;
				GUI.Box(new Rect((Screen.width/2) - 100, 20, 200, 55), "Press e to return scraper.");	
			}
		}
	}
}
