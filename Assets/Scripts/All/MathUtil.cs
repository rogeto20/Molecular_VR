using System;

namespace MoleculeViewer.Math {

	// ===============================
	// AUTHOR: Craig Milby
	// DATE: 25 October, 2017
	// PURPOSE: MathUtil has math funtions that are used throughout
	// 			the program.
	// ===============================
	static class MathUtil {

		/// <summary>
		/// Scale a value p_x betweeen min p_a and max p_b where the current max and min
		/// values are p_min and p_max
		/// </summary>
		/// <param name="p_x">P x.</param>
		/// <param name="p_a">P a.</param>
		/// <param name="p_b">P b.</param>
		/// <param name="p_min">P minimum.</param>
		/// <param name="p_max">P max.</param>
		public static float Scale ( float p_x, float p_a, float p_b, float p_min, float p_max ) {
			return ( ( ( p_b - p_a ) * ( p_x - p_max ) ) / ( p_max - p_min ) ) + p_a;
		}
	}
}

