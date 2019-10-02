using UnityEditor;
using UnityEngine;

using System.IO;

[CustomEditor ( typeof ( MoleculeCreateScript ) )]
public class MoleculeCreateInspector : Editor {

	public override void OnInspectorGUI ( ) {
		if ( GUILayout.Button ( "Import Molecule" ) ) {
			MoleculeCreateScript molecule = target as MoleculeCreateScript;

			string path = EditorUtility.OpenFilePanel ( "", "", "" );
			molecule.CreateMolecule ( path );
		}
	}
}
