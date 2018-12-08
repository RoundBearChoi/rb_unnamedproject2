using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class VFXManager : Manager {
		public Dictionary<SimpleEffectType, EffectPool> EffectPoolDictionary;

		public void ShowSimpleEffect (SimpleEffectType simpleEffectType, Vector3 pos) {
			if (EffectPoolDictionary.ContainsKey (simpleEffectType)) {
				EffectPoolDictionary[simpleEffectType].ShowEffect (pos);
			}
		}
	}
}