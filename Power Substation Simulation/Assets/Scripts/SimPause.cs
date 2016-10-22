using System.Collections;
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
	public Dropdown ResDropDown;

	public Resolution[] resolutions;

	public int ScreenResNum=4;
	//preparation for the resolution switch case




	public void Update(){
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//ensure that the other menus are turned off
			canvasresolution.gameObject.SetActive (false);

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
		//do not implement yet!
	}
	public void backButton(){

		canvasresolution.gameObject.SetActive (false);
		canvas.gameObject.SetActive (true);
		//used 
	}




	public void videoSettingsMenu (){
		// still working on the settings to get the screen resolutions to work
		Debug.Log ("Video Settings");

		canvas.gameObject.SetActive (false);
		Time.timeScale = 0;
		canvasresolution.gameObject.SetActive (true);
		//when the settings menu is activated, shut off the first menu and activate the second

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
		void SetVsyncCount() {
			int Vcount=0;
			GameObject inputFieldVsyncFind = GameObject.Find("ChangeVsync");
			InputField inputFieldVsyncBring = inputFieldVsyncFind.GetComponent<InputField>();
			Debug.Log(inputFieldVsyncBring.text);
			
			
			
			QualitySettings.vSyncCount = Vcount;
		}


		void SetFPS () {
			int FPS=200;
			GameObject inputFieldFPSFind = GameObject.Find("ChangeFPS");
			InputField inputFieldFPSBring = inputFieldFPSFind.GetComponent<InputField>();
			Debug.Log(inputFieldFPSBring.text);
			Application.targetFrameRate = FPS;
		}



//800x600, 1024x768, 1152x864, 1600x1200, 1280x720, 1360x768, 
//1366x768, 1536x864, 1600x900, 1920x1080, 2560x1440, 1280x800, 
//1440x900, 1680x1050, 1920x1200, and 2560x1600




	
	
}
