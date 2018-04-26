using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKit : MonoBehaviour, Interactable {

	public GameObject syringe;
	public GameObject syringeplacement;
	public GameObject glass;
	public Material glassmat;
	bool oilsent = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void interact(GameObject interactor) {
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		if ( inventory.currentTool.toolName == "Syringe" && inventory.currentTool.syringefull == true) {
			inventory.currentTool.syringefull = false;
			inventory.currentTool.parentObject = null;
			inventory.currentTool.transform.parent = null;
			inventory.currentTool = null;
			Vector3 returnpt = syringeplacement.transform.position;
			syringe.transform.position = returnpt;
			syringe.transform.rotation = syringeplacement.transform.rotation;
			glass.GetComponent<Renderer>().material = glassmat;
			oilsent = true;
		}
	}

	public void displayInteractionMessage(GameObject interactor)
	{
		PlayerInventoryScript inventory = interactor.GetComponent<PlayerInventoryScript>();
		if ( inventory.currentTool != null ) {
			if ( inventory.currentTool.toolName == "Syringe" && inventory.currentTool.syringefull == true) {
				GUI.color = Color.white;
				GUI.Box(new Rect((Screen.width / 2) - 100, 20, 200, 55), "Press e to send oil to lab.");	
			}
		}
		if ( oilsent ) {
			GUI.color = Color.white;
			GUI.Box(new Rect((Screen.width / 2) - 100, 20, 200, 55), "Oil sent to lab.");	
			StartCoroutine(Reset());
		}
	}

	IEnumerator Reset() {
		yield return new WaitForSeconds(5.0f);
		oilsent = false;
	}
}
