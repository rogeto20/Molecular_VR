using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Hovereable calls unity events when a hover starts and ends. This allows
//			us to have different interactions simply when "selecting" it.
// ===============================
public class VRHoverable : MonoBehaviour {

	public UnityEngine.Events.UnityEvent HoverStart;
	public UnityEngine.Events.UnityEvent HoverEnd;

	[HideInInspector]
	public bool IsHovering = false;

	void Start ( ) {
		
	}

	void Update ( ) {
		
	}

	/// <summary>
	/// Call a unity event when hovering begins
	/// 
	/// This is called from interactable
	/// UnityEvents are handled by Unity internally
	/// </summary>
	public void HoverStarted ( ) {
		HoverStart.Invoke ( );
		IsHovering = true;
	}

	/// <summary>
	/// Call a unity event when hovering ends
	/// 
	/// This is called from interactable
	/// UnityEvents are handled by Unity internally
	/// </summary>
	public void HoverEnded ( ) {
		HoverEnd.Invoke ( );
		IsHovering = false;
	}
}
