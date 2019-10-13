using UnityEditor;
using UnityEngine;

/// <summary>
/// Bezier curve inspector.
/// 
/// This class draws bezier curves in the scene view to make
/// it easier for us to edit them when debugging
/// 
/// This class does not get built when we export the final
/// project to an exe
/// </summary>
[CustomEditor ( typeof ( BezierCurve ) )]
public class BezierCurveInspector : Editor {

	private BezierCurve Curve;
	private Transform HandleTransform;
	private Quaternion HandleRotation;

	private const int LineSteps = 10;

	/// <summary>
	/// Draw the bezier curve every time to sceneGUI
	/// is redrawn
	/// </summary>
	private void OnSceneGUI ( ) {
		Curve = target as BezierCurve;
		HandleTransform = Curve.transform;
		HandleRotation = Tools.pivotRotation == PivotRotation.Local ? HandleTransform.rotation : Quaternion.identity;
	
		Vector3 p0 = ShowPoint ( 0 );
		Vector2 p1 = ShowPoint ( 1 );
		Vector3 p2 = ShowPoint ( 2 );

		Handles.color = Color.gray;
		Handles.DrawLine ( p0, p1 );
		Handles.DrawLine ( p1, p2 );

		Handles.color = Color.white;
		Vector3 lineStart = Curve.GetPoint ( 0.0f );
		for ( int i = 1; i <= LineSteps; i++ ) {
			Vector3 lineEnd = Curve.GetPoint ( i / ( float ) LineSteps );
			Handles.DrawLine ( lineStart, lineEnd );
			lineStart = lineEnd;
		}
	}

	/// <summary>
	/// Show a handle at a specific point in the scene
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="p_index">P index.</param>
	private Vector3 ShowPoint ( int p_index ) {
		Vector3 point = HandleTransform.TransformPoint ( Curve.Points [ p_index ] );
		EditorGUI.BeginChangeCheck ( );
		point = Handles.DoPositionHandle ( point, HandleRotation );

		if ( EditorGUI.EndChangeCheck ( ) ) {
			Undo.RecordObject ( Curve, "Move Point" );
			EditorUtility.SetDirty ( Curve );
			Curve.Points [ p_index ] = HandleTransform.InverseTransformPoint ( point );
		}

		return point;
	}
}
