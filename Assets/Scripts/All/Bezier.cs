using UnityEngine;
using System.Collections;

// ===============================
// AUTHOR: Craig Milby
// DATE: 25 October, 2017
// PURPOSE: Bezier is a helper class for bezier curve functions.
// ===============================
public class Bezier {

    /*public static Vector3 GetPoint ( Vector3 p_0, Vector3 p_1, Vector3 p_2, float p_t ) {
		return Vector3.Lerp ( Vector3.Lerp ( p_0, p_1, p_t ), Vector3.Lerp ( p_1, p_2, p_t ), p_t );
	}*/

    /// <summary>
    /// Get an interpolated point based on the provided curve
    /// </summary>
    /// <returns>The point.</returns>
    /// <param name="p_0">P 0.</param>
    /// <param name="p_1">P 1.</param>
    /// <param name="p_2">P 2.</param>
    /// <param name="p_t">P t.</param>
    public static Vector3 GetPoint(Vector3 p_0, Vector3 p_1, Vector3 p_2, float p_t)
    {
        p_t = Mathf.Clamp01(p_t);
        float oneMinusT = 1.0f - p_t;
        return oneMinusT * oneMinusT * p_0 + 2.0f * oneMinusT * p_t * p_1 + p_t * p_t * p_2;
    }

    ///// <summary>
    ///// Get a tangental vector based on the t value and
    ///// the bezier curve provided
    ///// </summary>
    ///// <returns>The first derivative.</returns>
    ///// <param name="p_0">P 0.</param>
    ///// <param name="p_1">P 1.</param>
    ///// <param name="p_2">P 2.</param>
    ///// <param name="p_t">P t.</param>
    public static Vector3 GetFirstDerivative(Vector3 p_0, Vector3 p_1, Vector3 p_2, float p_t)
    {
        return 2.0f * (1.0f - p_t) * (p_1 - p_0) + 2.0f * p_t * (p_2 - p_1);
    }
}

