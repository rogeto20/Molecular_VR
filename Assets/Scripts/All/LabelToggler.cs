using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: LabelToggler is used to toggle labels
//			on molecules on and off.
// ===============================
public class LabelToggler : MonoBehaviour {

	public bool DrawLabels = true;
	private bool Last;

	private RotateLabels[] Texts;

	void Awake ( ) {
		Texts = GetComponentsInChildren<RotateLabels> ( true );
		Toggle ( DrawLabels );
	}

	/// <summary>
	/// Debug purposes for inspector functions
	/// </summary>
	void Update ( ) {
		if ( Last != DrawLabels ) {
			Toggle ( DrawLabels );
		}

		Last = DrawLabels;
	}

	/// <summary>
	/// Toggle the labels depending on the params
	/// </summary>
	/// <param name="p_toggle">If set to <c>true</c> p toggle.</param>
	public void Toggle ( bool p_toggle ) {
		foreach ( RotateLabels text in Texts ) {
			text.gameObject.SetActive ( p_toggle );
		}
	}

	/// <summary>
	/// Flip the toggle
	/// </summary>
	public void Toggle ( ) {
		DrawLabels = !DrawLabels;
		Last = DrawLabels;

		Toggle ( DrawLabels );
	}
}
