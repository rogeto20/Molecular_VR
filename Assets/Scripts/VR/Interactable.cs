using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Interactable script is placed on the controllers so track
//			when they interact with the world.
// ===============================
public class Interactable : MonoBehaviour {

	public SteamVR_TrackedObject TrackedObject;

	// Let's get the controller object
	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	// These handle when a trigger is pressed
	// vs when it is held
	private bool TriggerPressed = false;
	private bool TriggerReset = false;

	void Awake ( ) {
		
	}

	void Update ( ) {
		if ( TriggerPressed ) {
			TriggerReset = HandController.GetHairTriggerUp ( );
		}

		TriggerPressed = HandController.GetHairTriggerDown ( );
	}

	/// <summary>
	/// When the controller enters another collider 
	/// do the necessary interctions
	/// </summary>
	/// <param name="p_collider">P collider.</param>
	void OnTriggerEnter ( Collider p_collider ) {
		VRHoverable hover = p_collider.GetComponent<VRHoverable> ( );
		if ( hover ) {
			hover.HoverStarted ( );
		}

		VRButton button = p_collider.GetComponent <VRButton > ( );
		if ( button ) {
			button.ButtonHoverStart ( );
		}
	}

	/// <summary>
	/// Some interactions happen every frame when the controller is
	/// in a collider
	/// </summary>
	/// <param name="p_collider">P collider.</param>
	void OnTriggerStay ( Collider p_collider ) {
		if ( TriggerPressed && !TriggerReset ) {
			VRButton button = p_collider.GetComponent < VRButton > ( );
			if ( button ) {
				button.ButtonPressed ( );
			}
		}
	}

	/// <summary>
	/// Handle when the controller leaves the collider
	/// </summary>
	/// <param name="p_collider">P collider.</param>
	void OnTriggerExit ( Collider p_collider ) {
		VRHoverable hover = p_collider.GetComponent<VRHoverable> ( );
		if ( hover ) {
			hover.HoverEnded ( );
		}

		VRButton button = p_collider.GetComponent <VRButton > ( );
		if ( button ) {
			button.ButtonHoverEnd ( );
		}
	}
}
