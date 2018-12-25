using System.Collections;
using System.Collections.Generic;
using BayatGames.Serialization.Formatters.Binary;
using Sirenix.OdinInspector;
using UnityEngine;

namespace roundbeargames {
	public class SaveManager : Manager {
		public SaveData CurrentData = new SaveData ();

		void Awake () {
			Load ();
		}

		public void Save () {
			byte[] bytes = BinaryFormatter.SerializeObject (CurrentData);
			string stringData = System.Convert.ToBase64String (bytes);
			//Debug.Log (stringData);
			PlayerPrefs.SetString ("SaveData", stringData);
		}

		public void Load () {
			string stringData = PlayerPrefs.GetString ("SaveData");
			if (!stringData.Equals ("")) {
				byte[] bytes = System.Convert.FromBase64String (stringData);
				CurrentData = BinaryFormatter.DeserializeObject (bytes, typeof (SaveData)) as SaveData;
			} else {
				CurrentData = new SaveData ();
				Save ();
			}
		}

		[Button (ButtonSizes.Medium)]
		public void DeleteAllSavedData () {
			PlayerPrefs.DeleteAll ();
		}

		[Button (ButtonSizes.Medium)]
		public void SaveData () {
			Save ();
		}

		[Button (ButtonSizes.Medium)]
		public void LoadData () {
			Load ();
		}
	}
}