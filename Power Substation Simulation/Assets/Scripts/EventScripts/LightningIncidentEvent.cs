using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


class LightningIncidentEvent : SimulationEvent
{
	public GameObject SimpleLightningBoltPrefab;

	protected GameObject lightningContainer;
	LeverScript lever;

	public void Start()
	{
		canCancel = true;
		lever = GetComponent<LeverScript>();
		if (lever == null)
			Debug.Log("did not find lever");
		if (SimpleLightningBoltPrefab == null)
			Debug.Log("ERROR: could not find lightning :-(");
	}

	public void Update()
	{
		if (eventTriggered == true)
		{
			if (SimpleLightningBoltPrefab == null)
			{
				eventTriggered = false;
			}
		}
	}



	public void OnGUI()
	{

	}




	public override void beginEvent()
	{
		if (eventTriggered == false && SimpleLightningBoltPrefab != null)
		{
			eventTriggered = true;
			StartCoroutine (processTask ());

		}

	}

	IEnumerator processTask(){
		yield return new WaitForSeconds (1);
		lightningContainer = Instantiate(SimpleLightningBoltPrefab);
		Destroy(lightningContainer, 3);
		yield return new WaitForSeconds (3);
		//Debug.Log("Lightning BOOM");
		eventTriggered = false;
	}


	public override void endEvent()
	{
		if (lightningContainer!=null)
		{
			Destroy(lightningContainer);
		}
		eventTriggered = false;
	}

	public override void displayMessage()
	{
		GUI.color = Color.white;
		if (!eventTriggered)
			GUI.Box(new Rect(20, 20, 200, 55), "Press e for lightning.");
	}
}

