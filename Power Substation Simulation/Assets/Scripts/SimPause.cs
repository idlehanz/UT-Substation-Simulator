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
	public InputField ChangeInvert;


	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log (FPS);

			//ensure that the other menus are turned off
			canvasresolution.gameObject.SetActive (false);
			canvasaudio.gameObject.SetActive (false);
			//if the pause menu isn't activated
			if(canvas.gameObject.activeInHierarchy==false)
			{
				//activate the pause menu
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				//the power to stop time
				GameObject.Find("Camera").GetComponent<MouseLook>().enabled = false; // can be done by changing sensitivity to 0.
				//failure of a command

			}
			else{
				canvas.gameObject.SetActive (false);
				Time.timeScale = 1;
				GameObject.Find("Camera").GetComponent<MouseLook>().enabled = true;
				//unpause everything
			}
			canvasresolution.gameObject.SetActive (false);
			canvasaudio.gameObject.SetActive (false);
			//still having some issues turning off the canvas resolution with escape
		}
	}
		
	public void continueSim(){
		Debug.Log ("Continue");
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		GameObject.Find("Camera").GetComponent<MouseLook>().enabled = true;
	}

	public void reloadSim(){
		Debug.Log (SceneManager.GetActiveScene ().name);
		//SceneManager.UnloadScene (0);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		//reload the level. this command is out of date and doesn't quite load right
		//any suggestions would be amazing
	}
		
	public void quitSim(){
		Debug.Log ("Quit");
		Application.Quit ();
	}

	public void backButton(){

		canvasresolution.gameObject.SetActive (false);
		canvasaudio.gameObject.SetActive (false);
		canvas.gameObject.SetActive (true);
		//used 
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


	public void ResChangeDrop() {
		resolutions = Screen.resolutions;

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
		//switch case is not currently attached to anything
		case 0:
			Screen.SetResolution (800, 600, x);
			//full screen
			Debug.Log ("800,600");
			break;
		case 1:
			Screen.SetResolution (1024, 768, x);
			Debug.Log ("1026x768");
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
	//SetVsyncCount not enabled
	public void SetVsyncCount() {
		int Vcount=0;
		GameObject inputFieldVsyncFind = GameObject.Find("ChangeVsync");
		InputField ChangeVsync = inputFieldVsyncFind.GetComponent<InputField>();
		Vcount = Convert.ToInt32 (ChangeVsync.text);
		Debug.Log("Vcount" + ChangeVsync.text);
		if (Vcount<0){
			QualitySettings.vSyncCount = 0;
		}
		else if (Vcount > 3) {
			QualitySettings.vSyncCount = 3;
		}
		else QualitySettings.vSyncCount = Vcount;
	}


	//SetFPS not entirely enabled either. 
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

	public void SetFOV(){
		GameObject inputfieldFOVFind = GameObject.Find ("ChangeFOV");
		ChangeFOV = inputfieldFOVFind.GetComponent<InputField> ();
		FOV = Convert.ToInt32 (ChangeFOV.text);
		Debug.Log ("FOV = " + FOV);

		Camera tmpcam = GameObject.Find ("Camera").GetComponent<Camera>();

		if (FOV > 360) {
			/////this may need to be changed
			//GetComponent<Camera> ().fieldOfView = 360;////////////////////////////
			tmpcam.fieldOfView = 360;
		}

		else if (FOV < 1) {
			//GetComponent<Camera> ().fieldOfView = 1;//////////////////////
			tmpcam.fieldOfView = 1;
		}
		else tmpcam.fieldOfView = FOV;
		//GetComponent<Camera> ().fieldOfView = FOV;
	}

	public void SetMouseInvert(){
		
		GameObject player = GameObject.Find ("Camera");
		MouseLook InvertFieldOBJ = player.GetComponent<MouseLook>() as MouseLook;
		bool InvertBool = GameObject.Find ("InvertMouse").GetComponent<Toggle>().isOn;

		//MouseLook InvertFieldOBJ = gameObject.GetComponent(typeof(MouseLook)) as MouseLook;
		//InvertFieldOBJ.InvertMouse ();
		//MouseLook go = InvertFieldOBJ.GetComponent<MouseLook> ();


		//bool InvertBool = InvertFieldOBJ.GetComponent<Toggle> ().isOn;
		//Debug.Log ("Invert mouse = " + InvertBool);


		if (InvertBool == true) 
		{
			InvertFieldOBJ.InvertMouse ();
		} 

		else 
		{
			InvertFieldOBJ.UninvertMouse ();
		}


		/*
		var inputY = Input.GetAxis("Vertical") * (iflipY ? -1 : 1);
		Debug.Log ("Invert mouse = " + flipY);
		*/
	}




	//800x600, 1024x768, 1152x864, 1600x1200, 1280x720, 1360x768, 
	//1366x768, 1536x864, 1600x900, 1920x1080, 2560x1440, 1280x800, 
	//1440x900, 1680x1050, 1920x1200, and 2560x1600






}

//old script starts here -- testing John's changes.
/*
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
	public InputField ChangeInvert;


	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log (FPS);

			//ensure that the other menus are turned off
			canvasresolution.gameObject.SetActive (false);
			canvasaudio.gameObject.SetActive (false);
			//if the pause menu isn't activated
			if(canvas.gameObject.activeInHierarchy==false)
			{


				//activate the pause menu
				canvas.gameObject.SetActive(true);
				Time.timeScale = 0;
				//the power to stop time
				GetComponent<MouseLook>().enabled = false;
				//failure of a command

			}
			else{
				canvas.gameObject.SetActive (false);
				Time.timeScale = 1;
				GetComponent<MouseLook>().enabled = true;
				//unpause everything
			}
		canvasresolution.gameObject.SetActive (false);
			canvasaudio.gameObject.SetActive (false);
		//still having some issues turning off the canvas resolution with escape
		}
	}




	public void continueSim(){
		Debug.Log ("Continue");
		canvas.gameObject.SetActive (false);
		Time.timeScale = 1;
		//unpause the game with a button rather than escape
	}
	public void reloadSim(){
		Debug.Log ("Reload");

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		//reload the level. this command is out of date and doesn't quite load right
		//any suggestions would be amazing
	}


	public void quitSim(){
		Debug.Log ("Quit");
		Application.Quit ();
	}
	public void backButton(){

		canvasresolution.gameObject.SetActive (false);
		canvasaudio.gameObject.SetActive (false);
		canvas.gameObject.SetActive (true);
		//used 
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


	public void ResChangeDrop() {
		resolutions = Screen.resolutions;

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
		//switch case is not currently attached to anything
		case 0:
			Screen.SetResolution (800, 600, x);
			//full screen
			Debug.Log ("800,600");
			break;
		case 1:
			Screen.SetResolution (1024, 768, x);
			Debug.Log ("1026x768");
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
		//SetVsyncCount not enabled
		public void SetVsyncCount() {
			int Vcount=0;
			GameObject inputFieldVsyncFind = GameObject.Find("ChangeVsync");
			InputField ChangeVsync = inputFieldVsyncFind.GetComponent<InputField>();
		Vcount = Convert.ToInt32 (ChangeVsync.text);
			Debug.Log("Vcount" + ChangeVsync.text);
				if (Vcount<0){
					QualitySettings.vSyncCount = 0;
				}
				if (Vcount > 3) {
					QualitySettings.vSyncCount = 3;
				}

			QualitySettings.vSyncCount = Vcount;
		}


		//SetFPS not entirely enabled either. 
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
		
	public void SetFOV(){
		GameObject inputfieldFOVFind = GameObject.Find ("ChangeFOV");
		ChangeFOV = inputfieldFOVFind.GetComponent<InputField> ();
		FOV = Convert.ToInt32 (ChangeFOV.text);
		Debug.Log ("FOV = " + FOV);

			if (FOV > 360) {
				GetComponent<Camera> ().fieldOfView = 360;
			}

			if (FOV < 1) {
				GetComponent<Camera> ().fieldOfView = 1;
			}
		GetComponent<Camera> ().fieldOfView = FOV;
	}

	public void SetMouseInvert(){
		GameObject player = GameObject.Find ("Camera");
		MouseLook InvertFieldOBJ = player.GetComponent<MouseLook>() as MouseLook;
		bool InvertBool = GameObject.Find ("InvertMouse").GetComponent<Toggle>().isOn;

		//MouseLook InvertFieldOBJ = gameObject.GetComponent(typeof(MouseLook)) as MouseLook;
		//InvertFieldOBJ.InvertMouse ();
		//MouseLook go = InvertFieldOBJ.GetComponent<MouseLook> ();


		//bool InvertBool = InvertFieldOBJ.GetComponent<Toggle> ().isOn;
		//Debug.Log ("Invert mouse = " + InvertBool);


		if (InvertBool == true) 
		{
			InvertFieldOBJ.InvertMouse ();
		} 

		else 
		{
			InvertFieldOBJ.UninvertMouse ();
		}


		
		//var inputY = Input.GetAxis("Vertical") * (iflipY ? -1 : 1);
		//Debug.Log ("Invert mouse = " + flipY);

	}




//800x600, 1024x768, 1152x864, 1600x1200, 1280x720, 1360x768, 
//1366x768, 1536x864, 1600x900, 1920x1080, 2560x1440, 1280x800, 
//1440x900, 1680x1050, 1920x1200, and 2560x1600




	
	
}
*/