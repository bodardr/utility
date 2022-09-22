using System.Linq;
using UnityEditor;
using UnityEditor.Build;

namespace Bodardr.Utility.Editor
{
    public static class PlayerSettingsExtensions
    {
        /// <summary>
        /// Adds a Scripting define to the specified build target
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <param name="define"></param>
        /// <returns>True if it was added to the scripting define symbols,
        /// False if it was already present within the scripting define symbols</returns>
        public static bool AddScriptingDefine(NamedBuildTarget buildTarget, string define)
        {
            PlayerSettings.GetScriptingDefineSymbols(buildTarget, out var defines);
            
            var hashSet = defines.ToHashSet();

            if (hashSet.Contains(define))
                return false;
            
            hashSet.Add(define);
            
            PlayerSettings.SetScriptingDefineSymbols(buildTarget, hashSet.ToArray());

            return true;
        }
        
        /// <summary>
        /// Removes a Scripting define to the specified build target
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <param name="define"></param>
        /// <returns>True if it was contained in the scripting define symbols, thus being removed,
        /// False if it wasn't present within the scripting define symbols</returns>
        public static bool RemoveScriptingDefine(NamedBuildTarget buildTarget, string define)
        {
            PlayerSettings.GetScriptingDefineSymbols(buildTarget, out var defines);
            
            var hashSet = defines.ToHashSet();

            if (!hashSet.Contains(define))
                return false;
            
            hashSet.Remove(define);
            
            PlayerSettings.SetScriptingDefineSymbols(buildTarget, hashSet.ToArray());

            return true;
        }
    }
}