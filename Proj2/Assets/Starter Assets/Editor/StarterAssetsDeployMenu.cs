using System.Linq;
using UnityEditor;
using UnityEngine;
#if STARTER_ASSETS_PACKAGES_CHECKED
using Unity.Cinemachine;
#endif

namespace StarterAssets
{
    public partial class StarterAssetsDeployMenu : ScriptableObject
    {
        public const string MenuRoot = "Tools/Starter Assets";

        // prefab names
        private const string MainCameraPrefabName = "MainCamera";
        private const string PlayerCapsulePrefabName = "PlayerCapsule";

        // names in hierarchy
        private const string CinemachineVirtualCameraName = "PlayerFollowCamera";

        // tags
        private const string PlayerTag = "Player";
        private const string MainCameraTag = "MainCamera";
        private const string CinemachineTargetTag = "CinemachineTarget";

        // Get the path to the template prefabs
        private static string StarterAssetsPath => PathToThisFile;

        private static GameObject _cinemachineVirtualCamera;

        public static string StarterAssetsInstallPath
        {
            get
            {
                string path = PathToThisFile;
                return path.Substring(0, path.LastIndexOf("StarterAssets"));
            }
        }

        private static string PathToThisFile
        {
            get
            {
                var dummy = CreateInstance<StarterAssetsDeployMenu>();
                string path = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(dummy));
                DestroyImmediate(dummy);
                return path.Substring(0, path.LastIndexOf("/Editor/StarterAssetsDeployMenu.cs"));
            }
        }

        [MenuItem(MenuRoot + "/Reinstall Dependencies", false)]
        static void ResetPackageChecker()
        {
            Debug.LogWarning("Package checker utilities not found in this project.");
        }

#if STARTER_ASSETS_PACKAGES_CHECKED
        private static void CheckCameras(string prefabPath, Transform targetParent)
        {
            CheckMainCamera(prefabPath);

            GameObject vcam = GameObject.Find(CinemachineVirtualCameraName);

            if (!vcam)
            {
                HandleInstantiatingPrefab(
                    StarterAssetsPath + prefabPath,
                    CinemachineVirtualCameraName,
                    out GameObject vcamPrefab
                );
                _cinemachineVirtualCamera = vcamPrefab;
            }
            else
            {
                _cinemachineVirtualCamera = vcam;
            }

            GameObject target = targetParent.Find("PlayerCameraRoot")?.gameObject;

            if (target == null)
            {
                target = new GameObject("PlayerCameraRoot");
                target.transform.SetParent(targetParent);
                target.transform.localPosition = new Vector3(0f, 1.375f, 0f);
                Undo.RegisterCreatedObjectUndo(target, "Created new cinemachine target");
            }

            CheckVirtualCameraFollowReference(target, _cinemachineVirtualCamera);
        }

        private static void CheckMainCamera(string prefabPath)
        {
            GameObject[] mainCameras = GameObject.FindGameObjectsWithTag(MainCameraTag);

            if (mainCameras.Length < 1)
            {
                HandleInstantiatingPrefab(
                    StarterAssetsPath + prefabPath,
                    MainCameraPrefabName,
                    out _
                );
            }
            else
            {
                if (!mainCameras[0].TryGetComponent(out CinemachineBrain cinemachineBrain))
                {
                    mainCameras[0].AddComponent<CinemachineBrain>();
                }
            }
        }

        private static void CheckVirtualCameraFollowReference(GameObject target, GameObject cinemachineVirtualCamera)
        {
            var virtualCamera = cinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>();
            if (virtualCamera == null)
            {
                Debug.LogError("CinemachineVirtualCamera component not found.");
                return;
            }

            var serializedObject = new SerializedObject(virtualCamera);
            var serializedProperty = serializedObject.FindProperty("m_Follow");

            if (serializedProperty != null)
            {
                serializedProperty.objectReferenceValue = target.transform;
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                Debug.LogError("Could not find m_Follow property on CinemachineVirtualCamera.");
            }
        }

        private static void HandleInstantiatingPrefab(string path, string prefabName, out GameObject prefab)
        {
            Object loadedPrefab = AssetDatabase.LoadAssetAtPath<Object>($"{path}{prefabName}.prefab");

            if (loadedPrefab == null)
            {
                Debug.LogError($"Could not load prefab at path: {path}{prefabName}.prefab");
                prefab = null;
                return;
            }

            prefab = (GameObject)PrefabUtility.InstantiatePrefab(loadedPrefab);

            if (prefab != null)
            {
                Undo.RegisterCreatedObjectUndo(prefab, "Instantiate Starter Asset Prefab");
                prefab.transform.localPosition = Vector3.zero;
                prefab.transform.localEulerAngles = Vector3.zero;
                prefab.transform.localScale = Vector3.one;
            }
        }
#endif
    }
}