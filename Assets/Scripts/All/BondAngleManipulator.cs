using UnityEngine;
using System.Collections;

public class BondAngleManipulator : MonoBehaviour {

	public Transform Atom1;
	public Transform Atom2;

	void Update ( ) {
		transform.position = Vector3.Lerp ( Atom1.position, Atom2.position, 0.5f ) ;
		transform.up = Atom2.position - Atom1.position;
        //got rid of the scaling changes to the bonds to fix the scaling for the cyclohexane molecule
		//transform.localScale = new Vector3 ( transform.localScale.x, Vector3.Distance ( Atom1.position, Atom2.position ), transform.localScale.z);
	}
}

