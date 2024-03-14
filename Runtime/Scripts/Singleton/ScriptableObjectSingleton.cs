using System;
using System.IO;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Bodardr.Utility.Runtime
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance;
        public static T Instance => instance ??= GetOrCreate();

        private static T GetOrCreate()
        {
            string assetPath = ComputePath(out var assetName);
            var settings = Resources.Load<T>(assetPath);

#if UNITY_EDITOR
            if (settings == null)
            {
                if (!AssetDatabase.IsValidFolder(assetPath))
                {
                    Directory.CreateDirectory(
                        Application.dataPath[..Application.dataPath.LastIndexOf("Assets", StringComparison.Ordinal)] +
                        assetPath);
                    AssetDatabase.Refresh();
                }

                settings = CreateInstance<T>();
                AssetDatabase.CreateAsset(settings,
                    assetPath + (assetPath.EndsWith('/') ? "" : "/") + assetName + ".asset");
                AssetDatabase.SaveAssets();
            }
#endif

            return settings;
        }

        private static string ComputePath(out string assetName)
        {
            var attribute = (ScriptableObjectSingletonAttribute)typeof(T).GetCustomAttributes(true)
                .Single(x => x is ScriptableObjectSingletonAttribute);

            assetName = string.IsNullOrWhiteSpace(attribute.CustomName)
                ? typeof(T).FullName.GetMangledName()
                : attribute.CustomName;

            return attribute.AssetFolder;
        }
    }
}