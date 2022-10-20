#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using Object = UnityEngine.Object;

public class AssetDatabaseUtility
{
    /// <summary>
    /// Inspired from https://forum.unity.com/threads/how-to-get-list-of-assets-at-asset-path.18898/
    /// Fetches all assets from a path.
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] LoadAssetsInFolder<T>(string path) where T : Object
    {
        var files = Directory.GetFiles(path);
        files = files.Except(files.Where(x => x.EndsWith(".meta"))).ToArray();

        var output = new T[files.Length];

        for (var i = 0; i < files.Length; i++)
        {
            var file = files[i];

            //This substring is the path starting with "Assets", and ending with its extension trimmed.
            var assetPath =
                file[file.IndexOf("Assets", StringComparison.InvariantCulture)..];

            output[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        return output;
    }
}
#endif