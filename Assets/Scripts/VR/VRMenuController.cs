using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This keeps track of the menu attached to the left hand controller.
// 			It handles interactions and updates the molecules accordingly.
// ===============================
public class VRMenuController : MonoBehaviour {

	public SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	public GameObject Container;
	public bool ShouldShowMenu = false;
	private bool PressDown = false;

	void Awake ( ) {
		
	}

	void Start ( ) {
		ShowMenu ( ShouldShowMenu );
	}

	/// <summary>
	/// Check if the controllers interactors 
	/// </summary>
	void Update ( ) {
		if ( HandController.GetPress ( SteamVR_Controller.ButtonMask.Touchpad ) ) {
			PressDown = true;
		} else {
			if ( HandController.GetPressUp ( SteamVR_Controller.ButtonMask.Touchpad ) && PressDown ) {
				ShowMenu ( );
				PressDown = false;
			}
		}
	}

	/// <summary>
	/// Handle the left pad click and show the menu
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="p_event">P event.</param>
	private void HandlePadClick ( object sender, ClickedEventArgs p_event ) {
		ShowMenu ( );
	}

	/// <summary>
	/// Shows the menu
	/// </summary>
	public void ShowMenu ( ) {
		ShouldShowMenu = !ShouldShowMenu;
		ShowMenu ( ShouldShowMenu );
	}

	/// <summary>
	/// Container gameobject is parent of all menu components
	/// hide this until we click the pad
	/// </summary>
	/// <param name="p_show">If set to <c>true</c> p show.</param>
	public void ShowMenu ( bool p_show ) {
		Container.SetActive ( p_show );
	}


}

