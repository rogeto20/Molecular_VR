using UnityEngine;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AUTHOR: Craig Milby
/// DATE: 30 October, 2017
/// PURPOSE: Handles the functionality of pressing the launch
/// 		 button in the VR launcher scene
/// </summary>
public class VRLauncherLaunchButton : MonoBehaviour {

	public VRLauncherLoadMoleculeButton LoadMolecules;

	public void HandleLaunchButtonPressed ( ) {
		List<string> molecules = LoadMolecules.LoadMolecules;


		for ( int i = 0; i < molecules.Count; i++ ) {
			SceneManager.SetParameter ( i.ToString ( ), molecules [ i ] );
		}

		SceneManager.Load ( "VRScene", true );
	}
}

