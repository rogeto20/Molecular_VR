using UnityEngine;
using UnityEngine.UI;

using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Handles button presses on the phone screen
// ===============================
public class ViewerSceneUIManager : MonoBehaviour {

	private VRCameraController VRCamera;
	private Text ButtonText;

	void Awake ( ) {
		VRCamera = Camera.main.GetComponent<VRCameraController> ( );
		ButtonText = GetComponentInChildren<Text> ( );
	}

	/// <summary>
	/// Load the main scene when button is clicked
	/// Called from a UnityEvent
	/// </summary>
	public void HandleMainMenuButtonClick ( ) {
		UnityEngine.SceneManagement.SceneManager.LoadScene ( "MainMenuScene" );
	}

	/// <summary>
	/// Lock the camera when the lock button is pressed
	/// Called from a UnityEvent
	/// </summary>
	public void HandleLockButtonClick ( ) {
		VRCamera.IsLocked = !VRCamera.IsLocked;
		if ( VRCamera.IsLocked ) { 
			ButtonText.text = "Unlock";
		} else {
			ButtonText.text = "Lock";
		}
	}
}

