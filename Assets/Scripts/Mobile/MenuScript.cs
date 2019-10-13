using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: This class loads new scenes from the main menu
// ===============================
public class MenuScript : MonoBehaviour {

	public void LoadScene ( string p_moleculeName ) {
		SceneManager.Load ( "ViewerScene", "Molecule", p_moleculeName );
	}
}
