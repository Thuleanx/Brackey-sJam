using UnityEngine;

namespace Thuleanx {
	public class Random {
		static Unity.Mathematics.Random random;	
		static bool initiated = false;

		public static float Range(float low, float hi) {
			if (!initiated) {
				random = new Unity.Mathematics.Random((uint) Time.time + 1);
				initiated = true;
			}
			return random.NextFloat(low, hi);
		}
	}
}