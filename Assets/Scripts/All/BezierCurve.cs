using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: BezierCurve is used to track a location along a
//			a curve defined by tow end points and an interpolation
// 			along those points.
// ===============================
public class BezierCurve : MonoBehaviour {

	public Vector3[] Points;

	/// <summary>
	/// Reset to a default bezier curve
	/// </summary>
	public void Reset ( ) {
		Points = new Vector3[] {
			new Vector3 ( 1.0f, 0.0f, 0.0f ),
			new Vector3 ( 2.0f, 0.0f, 0.0f ),
			new Vector3 ( 3.0f, 0.0f, 0.0f )
		};
	}

	/// <summary>
	/// Wrapper for Bezier.GetPoint
	/// 
	/// Scale to local coordinate
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="p_t">P t.</param>
	public Vector3 GetPoint ( float p_t ) {
		return transform.TransformPoint ( Bezier.GetPoint ( Points [ 0 ], Points [ 1 ], Points [ 2 ], p_t ) );
	}

	/// <summary>
	/// Get a vector that is tangental to the curve and time t
	/// </summary>
	/// <returns>The velocity.</returns>
	/// <param name="p_t">P t.</param>
	public Vector3 GetVelocity ( float p_t ) {
		return transform.TransformPoint ( Bezier.GetFirstDerivative ( Points [ 0 ], Points [ 1 ], Points [ 2 ], p_t ) ) - transform.position;
	}

	/// <summary>
	/// Get a normalized vector that is tangental to the curve and time t
	/// </summary>
	/// <returns>The direction.</returns>
	/// <param name="p_t">P t.</param>
	public Vector3 GetDirection ( float p_t ) {
		return GetVelocity ( p_t ).normalized;
	}
}
