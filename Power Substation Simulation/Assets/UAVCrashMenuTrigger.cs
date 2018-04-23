using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVCrashMenuTrigger : MonoBehaviour {
	public GameObject drone; 
	public GameObject[] nodes;
	Vector3 moveVector, aimVector;
	//int i = 0;
	public int m_score;
	//bool eventTriggered;
	public float speed, targetSpeed;
	public float rotateSpeed;
	Vector3 targetPosition;

	void SetText() {
		m_score = PlayerPrefs.GetInt("Event", 0);
	}
	// Use this for initialization
	void Start () {
		SetText();
		if ( drone == null ) {
			Debug.Log("drone object not set."); 
		}
		if ( nodes == null ) {
			Debug.Log("no nodes set in uavcrashmenutrigger.cs");
		}

		if ( m_score == 4 ) {
			BeginEvent();
		}
	}

	void BeginEvent() {
		drone.SetActive(true); //drone already exists in substation scene at start, deactivated at start position.

	}
	
	// Update is called once per frame
	void Update () {
	}
}
