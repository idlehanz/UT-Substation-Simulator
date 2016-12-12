using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimPause : MonoBehaviour{

	public Transform canvas;
	//this contains the menu information
	public Transform Player;
	//information on the player. Still no way to stop the mouse movement during pause yet
	public Transform canvasresolution;
	//this has the screen resolution menu information
	public Transform canvasaudio;
	//Audio menu information
	public Transform canvascredits;
	//Credits page 
	public Dropdown ResDropDown;
	//resolution drop down switch case
	public Resolution[] resolutions;
	//declare a variable for resolutions
	public int ScreenResNum=4;
	//preparation for the resolution switch case

	public InputField ChangeFOV;
	public InputField ChangeFPS;

	public int FPS=60;
	public int FOV;
	public Toggle InvertTogg;

	public void Start()
	{
		#if !UNITY_EDITOR
		Cursor.lockState = CursorLockMode.Locked;
		#endif
	}

	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			
			//if the pause menu isn't activated, activate it
			Debug.Log(canvas.gameObject.activeInHierarchy);
			if (canvas.gameObject.activeInHierarchy == false)
			{
				pauseSim ();
			}
			else
			{
				continueSim ();
			}
			canvasresolution.gameObject.SetActive (false);
			canvasaudio.gameObject.SetActive (false);
			canvascredits.gameObject.SetActive (false);
		}
	}

	public void pauseSim(){
		//activate the pause menu
		canvas.gameObject.SetActive(true);
		//the power to stop time
		Time.timeScale = 0;
		//this command doesn't work, using lockState. this will stop the camera from moving during menu operations
		GameObject.Find("Camera").GetComponent<MouseLook>().enabled = false; 
		#if !UNITY_EDITOR
		Cursor.lockState = CursorLockMode.None;
		#endif
	}

	public void continueSim(){
		//drops the menu canvas, turns on time, and unlocks the camera
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		GameObject.Find("Camera").GetComponent<MouseLook>().enabled = true;

		#if !UNITY_EDITOR
		Cursor.lockState = CursorLockMode.Locked;
		#endif
	}

	public void reloadSim(){
		Debug.Log (SceneManager.GetActiveScene ().name);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		Time.timeScale = 1;
		//reload the level. 
	}
		
	public void quitSim(){
		Debug.Log ("Quit");
		Application.Quit ();
	}

	public void backButton(){
		canvasresolution.gameObject.SetActive (false);
		canvasaudio.gameObject.SetActive (false);
		canvascredits.gameObject.SetActive (false);
		canvas.gameObject.SetActive (true);
		//removes all canvases and turns on the main menu canvas
	}

	public void videoSettingsMenu (){
		Debug.Log ("Video Settings");

		canvas.gameObject.SetActive (false);
		canvasaudio.gameObject.SetActive (false);
		Time.timeScale = 0;
		canvasresolution.gameObject.SetActive (true);
		//when the settings menu is activated, shut off the main menu and activate video

	}

	public void audioSettingsMenu ()
	{
		Debug.Log ("Audio Settings");
		canvas.gameObject.SetActive (false);
		canvasresolution.gameObject.SetActive (false);
		Time.timeScale = 0;
		canvasaudio.gameObject.SetActive (true);
		//when the audio menu is activated, shut off main, activate audio
	}
	public void creditsSettingsMenu ()
	{
		Debug.Log ("Credits Settings");
		canvas.gameObject.SetActive (false);
		canvasresolution.gameObject.SetActive (false);
		canvasaudio.gameObject.SetActive (false);
		Time.timeScale = 0;
		canvascredits.gameObject.SetActive (true);
		//when the canvas menu is activated, shut off main, activate audio
	}


	public void ResChangeDrop() {
		resolutions = Screen.resolutions;
		//creates a listener for the screen resolutions, allowing for the drop down to work
		ResDropDown.onValueChanged.AddListener (delegate {
			ProcessResDrop(ResDropDown);
		});

	}



	public void ProcessResDrop (Dropdown ResDropTemp){
		Debug.Log ("dropdown" + ResDropTemp.value);
		bool x;


		if (Screen.fullScreen == true) {
			x = true;
		} else {
			x = false;
		}
		//check to see if the screen is full sized or windowed.



		switch (ResDropTemp.value) {
		//each case is a different resolution
		case 0:
			Screen.SetResolution (800, 600, x);
			break;
		case 1:
			Screen.SetResolution (1024, 768, x);
			break;
		case 2:
			Screen.SetResolution (1152, 864, x);
			break;
		case 3:
			Screen.SetResolution (1600, 1200, x);
			break;
		case 4:
			Screen.SetResolution (1280, 720, x);
			break;
		case 5:
			Screen.SetResolution (1360, 768, x);
			break;
		case 6:
			Screen.SetResolution (1366, 768, x);
			break;
		case 7:
			Screen.SetResolution (1536, 864, x);
			break;
		case 8:
			Screen.SetResolution (1600, 900, x);
			break;
		case 9:
			Screen.SetResolution (1920, 1080, x);
			break;
		case 10:
			Screen.SetResolution (2560, 1440, x);
			break;
		case 11:
			Screen.SetResolution (1280, 800, x);
			break;
		case 12:
			Screen.SetResolution (1440, 900, x);
			break;
		case 13:
			Screen.SetResolution (1680, 1050, x);
			break;
		case 14:
			Screen.SetResolution (1920, 1200, x);
			break;
		case 15:
			Screen.SetResolution (2560, 1600, x);
			break;


		}
	}

	//setting vsync will control the ability to change FOV and FPS
	//Vsync can only be set between 0-2
	//if statement ensures input field stays within acceptable bounds
	public void SetVsyncCount() {
		int Vcount=0;
		GameObject inputFieldVsyncFind = GameObject.Find("ChangeVsync");
		InputField ChangeVsync = inputFieldVsyncFind.GetComponent<InputField>();
		Vcount = Convert.ToInt32 (ChangeVsync.text);
		Debug.Log("Vcount" + ChangeVsync.text);
		if (Vcount<0){
			QualitySettings.vSyncCount = 0;
		}
		else if (Vcount > 2) {
			QualitySettings.vSyncCount = 2;
		}
		else QualitySettings.vSyncCount = Vcount;
	}

	//set the FPS
	//if statement ensures input field stays within acceptable bounds
	public void SetFPS () {
		Debug.Log(FPS);

		GameObject inputFieldFPSFind = GameObject.Find("ChangeFPS");
		ChangeFPS = inputFieldFPSFind.GetComponent<InputField>();
		FPS = Convert.ToInt32(ChangeFPS.text);			
		Debug.Log ("FPS = " + FPS);

		if (FPS > 200) {
			Application.targetFrameRate = 200;  
		}
		if (FPS < 1) {
			Application.targetFrameRate = 1;  
		}
		Application.targetFrameRate = FPS;  



	}

	//sets the FOV
	//if statement ensures input field stays within acceptable bounds
	public void SetFOV(){
		GameObject inputfieldFOVFind = GameObject.Find ("ChangeFOV");
		ChangeFOV = inputfieldFOVFind.GetComponent<InputField> ();
		FOV = Convert.ToInt32 (ChangeFOV.text);
		Debug.Log ("FOV = " + FOV);

		Camera tmpcam = GameObject.Find ("Camera").GetComponent<Camera>();

		if (FOV > 360) {
			tmpcam.fieldOfView = 360;
		}

		else if (FOV < 1) {
			tmpcam.fieldOfView = 1;
		}
		else tmpcam.fieldOfView = FOV;
	}

	public void SetMouseInvert(){
		GameObject player = GameObject.Find ("Camera");
		MouseLook InvertFieldOBJ = player.GetComponent<MouseLook>() as MouseLook;
		bool InvertBool = InvertTogg.isOn;

		if (InvertBool == true) 
		{
			InvertFieldOBJ.InvertMouse ();
		} 

		else 
		{
			InvertFieldOBJ.UninvertMouse ();
		}



	}



}
