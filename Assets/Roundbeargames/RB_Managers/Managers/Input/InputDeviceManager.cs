using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames {
	public class InputDeviceManager : Manager {
		[SerializeField]
		VirtualInput virtualInputPrefab;
		private VirtualInput virtualInput;
		public VirtualInput VIRTUAL_INPUT {
			get {
				if (virtualInput == null) {
					VirtualInput[] old = GameObject.FindObjectsOfType<VirtualInput> ();
					foreach (VirtualInput v in old) {
						Destroy (v.gameObject);
					}

					GameObject obj = Instantiate (virtualInputPrefab.gameObject) as GameObject;
					virtualInput = obj.GetComponent<roundbeargames.VirtualInput> ();
				}
				return virtualInput;
			}
		}
	}
}