using System;

namespace Bodardr.Utility.Runtime
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScriptableObjectSingletonAttribute : Attribute
    {
        /// <summary>
        /// Mandatory attribute for the <see cref="ScriptableObjectSingleton{T}"/> class
        /// </summary>
        /// <param name="assetFolder">The folder in which the singleton's asset will be saves</param>
        /// <param name="customName">The custom name of the attribute can be specified. Otherwise, it will be set to the Display Name of the embedded class.</param>
        public ScriptableObjectSingletonAttribute(string assetFolder = "Assets/Resources/Singletons",
            string customName = "")
        {
            AssetFolder = assetFolder;
            CustomName = customName;
        }

        public string AssetFolder { get; }
        public string CustomName { get; }
    }
}