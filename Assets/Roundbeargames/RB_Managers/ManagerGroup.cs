using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public enum ManagerType
{
    INPUT_DEVICE_MANAGER,
    CHARACTER_MANAGER,
    FRAME_MANAGER,
    CAMERA_MANAGER,
    VFX_MANAGER,
    UI_MANAGER,
}

namespace roundbeargames
{
    public class ManagerGroup : SerializedMonoBehaviour
    {
        private static ManagerGroup _instance;

        public static ManagerGroup Instance
        {
            get
            {
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            LoadedManagers = new Dictionary<ManagerType, Manager>();
        }

        void Start()
        {
            GetManager(ManagerType.FRAME_MANAGER);
            GetManager(ManagerType.VFX_MANAGER);
        }

        [SerializeField]
        Dictionary<ManagerType, Manager> LoadedManagers;

        [SerializeField]
        List<Manager> ManagerPrefabs;

        public Manager GetManager(ManagerType managerType)
        {
            if (LoadedManagers.ContainsKey(managerType))
            {
                return LoadedManagers[managerType];
            }
            else
            {
                Manager[] old = GameObject.FindObjectsOfType<Manager>();
                foreach (Manager m in old)
                {
                    if (!LoadedManagers.ContainsValue(m))
                    {
                        Destroy(m.gameObject);
                    }
                }

                GameObject newObj = Instantiate(ManagerPrefabs[(int)managerType].gameObject) as GameObject;
                Manager newManager = newObj.GetComponent<Manager>();
                LoadedManagers.Add(managerType, newManager);
                return newManager;
            }
        }
    }
}