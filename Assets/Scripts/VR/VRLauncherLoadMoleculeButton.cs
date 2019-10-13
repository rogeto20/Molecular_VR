using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AUTHOR: Craig Milby
/// DATE: 10 October, 2017
/// PURPOSE: Handles loading molecules in the VR launcher
/// </summary>
public class VRLauncherLoadMoleculeButton : MonoBehaviour {

	public InputField Input;

	[HideInInspector]
	public List<string> LoadMolecules;

	void Start ( ) {
		LoadMolecules = new List<string> ( );

		AddNewLoadText ( "Methane" );
		AddNewLoadText ( "Ethane" );
		AddNewLoadText ( "Cyclohexane" );
	}

	void Update ( ) {
	
	}

	public void AddNewLoadText ( string p_text ) {
		LoadMolecules.Add ( p_text );

		Input.text += p_text + "\n";
	}

	public void HandleLoadMoleculeButtonPress ( ) {
		StartCoroutine ( _OpenLoadDialog ( ) );
	}

	IEnumerator _OpenLoadDialog ( ) {
		yield return SimpleFileBrowser.FileBrowser.WaitForLoadDialog ( false, null, ".cc1 file", "Okay" );
		AddNewLoadText ( SimpleFileBrowser.FileBrowser.Result );
	}
}

