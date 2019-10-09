using UnityEngine;
using System.Collections;

/// <summary>
/// This fixes the rotation of carbon molecules
/// so that they stay correctly oriented with their
/// child-parent components when rotating in VR
/// with the menu knobs
/// </summary>
public class CarbonMappingRotation : MonoBehaviour {

	/// <summary>
	/// Rotation based on the linear mapping values
	/// </summary>
	public VRLinearMapping Mapping;
	public float MoveAngle = 70.5f;
	public bool Inverse = false;

	private float StartAngle;
	private float XAngle;
	private float YAngle;

	void Awake ( ) {
		Vector3 euler = transform.rotation.eulerAngles;

		StartAngle = euler.z;
		XAngle = euler.x;
		YAngle = euler.y;
	}

	void Update ( ) {
		Vector3 euler = transform.rotation.eulerAngles;

		XAngle = euler.x;
		YAngle = euler.y;

		if ( Inverse ) {
			transform.rotation = Quaternion.Euler ( XAngle, YAngle, Mathf.Lerp ( StartAngle + MoveAngle, StartAngle, Mapping.value ) );
		} else {
			transform.rotation = Quaternion.Euler ( XAngle, YAngle, Mathf.Lerp ( StartAngle, StartAngle - MoveAngle, Mapping.value ) );
		}
	}
}

