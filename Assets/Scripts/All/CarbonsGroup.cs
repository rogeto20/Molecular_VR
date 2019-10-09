using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: CarbonGroup groups carbon molecules together
// 			in order to keep track of how they move in
// 			relation to one another.
// ===============================
public class CarbonsGroup : MonoBehaviour {

	public CarbonsGroup OtherGroup;

	public GameObject[] Carbons;
	private VRLinearMapping[] Mappings;
	private VRBezierDrive[] Drives;

	void Awake ( ) {
		Mappings = new VRLinearMapping [ Carbons.Length ];
		Drives = new VRBezierDrive [ Carbons.Length ];

		for ( int i = 0; i < Carbons.Length; i++ ) {
			Mappings [ i ] = Carbons [ i ].GetComponent < VRLinearMapping > ( );
			Drives [ i ] = Carbons [ i ].GetComponent < VRBezierDrive > ( );
		}
	}

	/// <summary>
	/// Update all linear mappings for the cyclohexane molecule
	/// 
	/// The carbons in the same group will all have the same mapping value
	/// the carbons in the otherr gorup will have a mapping value of 1 - value
	/// </summary>
	void Update ( ) {
		for ( int i = 0; i < Mappings.Length; i++ ) {
			if ( Mappings [ i ].Changed ) {
				float value = Mappings [ i ].value;
				for ( int j = 0; j < Mappings.Length; j++ ) {
					if ( i == j ) {
						continue;
					}

					Mappings [ j ].LastValue = value;
					Mappings [ j ].value = value;

					Drives [ j ].UpdatePosition ( );
				}

				if ( OtherGroup ) {
					for ( int j = 0; j < OtherGroup.Mappings.Length; j++ ) {
						OtherGroup.Mappings [ j ].LastValue = 1.0f - value;
						OtherGroup.Mappings [ j ].value = 1.0f - value;

						OtherGroup.Drives [ j ].UpdatePosition ( );
					}
				}

				break;
			}
		}
	}
}

