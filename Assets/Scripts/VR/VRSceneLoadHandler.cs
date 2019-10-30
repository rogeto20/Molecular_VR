using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

public class VRSceneLoadHandler : MonoBehaviour {

	public GameObject MoleculeButtonPrefab;

	public GameObject MoleculeCanvas;
	public GameObject MoleculeHolder;

	public VRUIController UIController;
	public SteamVR_TrackedObject TrackedObject;

	void Awake ( ) {
		Dictionary<string, string> molecules = SceneManager.GetSceneParameters ( );

		int index = 0;
		foreach ( KeyValuePair<string, string> mol in molecules ) {
			GameObject but = Instantiate ( MoleculeButtonPrefab, MoleculeCanvas.transform );
			GameObject molecule = null;
			if ( mol.Value == "Methane" ) {
				molecule = Resources.Load ( "Methane" ) as GameObject;
				molecule = Instantiate ( molecule, MoleculeHolder.transform );
				molecule.name = mol.Value;
			} else if ( mol.Value == "Ethane" ) {
				molecule = Resources.Load ( "Ethane" ) as GameObject;
				molecule = Instantiate ( molecule, MoleculeHolder.transform );
				molecule.name = mol.Value;
			}
            else if ( mol.Value == "Cyclohexane" ) {
				molecule = Resources.Load ( "Cyclohexane" ) as GameObject;
				molecule = Instantiate ( molecule, MoleculeHolder.transform );
				molecule.name = mol.Value;

				VRBezierDrive[] curve = molecule.GetComponentsInChildren<VRBezierDrive> ( );
				for ( int i = 0; i < curve.Length; i++ ) {
					curve [ i ].TrackedObject = TrackedObject;
					curve [ i ].TrackTransform = TrackedObject.transform;
				}
			}

            else {
				molecule = new GameObject ( );
				molecule.AddComponent<MoleculeCreateScript > ( ).CreateMolecule ( mol.Value );
                molecule.transform.parent = MoleculeHolder.transform;

				string[] pathTokens = mol.Value.Split ( new string[] { "/", "\\" }, System.StringSplitOptions.RemoveEmptyEntries );
				molecule.name = pathTokens [ pathTokens.Length - 1 ].Substring ( 0,
				                                                                 pathTokens [ pathTokens.Length - 1 ].IndexOf ( ".cc1" ) );
			}

            MoleculeHolder.tag = "Mol"; //adding tag to molecule so I can find it later.

			but.GetComponentInChildren<Text> ( ).text = molecule.name;
			but.GetComponentInChildren<VRButton> ( ).ButtonClick.AddListener ( ( ) => UIController.LoadMolecule ( molecule.name ) );
            but.GetComponentInChildren<VRHapticPulse>().TrackedObject = TrackedObject;

            if ( index > 0 ) {
				but.GetComponent<RectTransform> ( ).localPosition = new Vector3 ( ( index % 2 == 0 ) ? -( index / 2 ) : ( index / 2 ) + 1,
				                                                                  0.0f,
				                                                                  0.0f );
			}

			index++;
		}
	}
}

