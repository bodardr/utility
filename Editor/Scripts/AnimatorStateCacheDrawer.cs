using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Bodardr.Utility.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorStateCache))]
    public class AnimatorStateCacheDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 2;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            RuntimeAnimatorController animatorRef = null;

            var hasValue = property.objectReferenceValue;

            if (hasValue)
            {
                animatorRef =
                    AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(
                        AssetDatabase.GUIDToAssetPath(property.objectReferenceValue.name));
            }

            animatorRef =
                (AnimatorController)EditorGUI.ObjectField(position, property.displayName, animatorRef,
                    typeof(AnimatorController), true);

            //The following process requires an assigned animator.
            if (!animatorRef)
                return;

            var stateCache = AnimatorStateCacheCompiler.GetOrCreateStateCache(animatorRef);

            property.objectReferenceValue = stateCache;
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}