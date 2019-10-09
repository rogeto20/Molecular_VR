using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: HapticRack functions as a "ladder" of vibrations up and down
// 			a linear drive. Image "teeth" where each tooth causes a
//			haptic pulse when it is moved over.
// ===============================
public class VRHapticRack : MonoBehaviour {

	public VRLinearDrive Drive;

	public int TeethCount = 128;
	public int MinimumPulseDuration = 500;
	public int MaximunPulseDuration = 900;

	public UnityEngine.Events.UnityEvent OnPulse;

	private int PreviousToothIndex = -1;

	public SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	/// <summary>
	/// Pulse when updating and "teeth" are moving
	/// 
	/// Should be paired with a linear drive
	/// </summary>
	void Update ( ) {
		int currentToothIndex = Mathf.RoundToInt ( Drive.LinearMapping.value * TeethCount - 0.5f );
		if ( currentToothIndex != PreviousToothIndex ) {
			Pulse ( );
			PreviousToothIndex = currentToothIndex;
		}
	}

	/// <summary>
	/// Pulse for a small random amount
	/// </summary>
	private void Pulse ( ) {
		if ( Drive.WasHovering ) {
			ushort duration = ( ushort ) Random.Range ( MinimumPulseDuration, MaximunPulseDuration + 1 );
			HandController.TriggerHapticPulse ( duration );

			OnPulse.Invoke ( );
		}
	}
}

