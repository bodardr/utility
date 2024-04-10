#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Text;
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
    public static void CreateFolderIfNotExists(string folderPaths)
    {
        var folders = folderPaths.Split('/', '\\', StringSplitOptions.RemoveEmptyEntries);

        if (folders.Length < 2)
            throw new Exception("Folder paths must at least contain two elements. Something like \"Assets/MyScripts\"");
        
        for (int i = 1; i < folders.Length; i++)
        {
            StringBuilder str = new StringBuilder();

            for (int j = 0; j < i; j++)
            {
                str.Append($"{folders[j]}/");
            }

            //Remove last '/'
            str.Remove(str.Length - 1, 1);
            
            if (AssetDatabase.IsValidFolder(Path.Combine(str.ToString(),folders[i])))
                continue;

            AssetDatabase.CreateFolder(str.ToString(), folders[i]);
        }
    }
}
#endif