using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Haptic Pulse allows the controller to vibrate for a short (millisecond)
//			period of time.
// ===============================
public class VRHapticPulse : MonoBehaviour {

	public int MinimumPulseDuration = 500;
	public int MaximunPulseDuration = 900;

	public UnityEngine.Events.UnityEvent OnPulse;

	public SteamVR_TrackedObject TrackedObject;

	private SteamVR_Controller.Device HandController {
		get {
			return SteamVR_Controller.Input ( ( int ) TrackedObject.index );
		}
	}

	/// <summary>
	/// Pulse controller for a small random amount
	/// </summary>
	public void Pulse ( ) {
		ushort duration = ( ushort ) Random.Range ( MinimumPulseDuration, MaximunPulseDuration + 1 );
		HandController.TriggerHapticPulse ( duration );

		OnPulse.Invoke ( );
	}
}

