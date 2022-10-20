using System;
using System.Text;
using Bodardr.Utility.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bodardr.Utility.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]
    public class ReadOnlyInspectorDrawer : PropertyDrawer
    {
        private static GUIStyle richTextStyle;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 16;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (richTextStyle == null)
                InitializeStyle();

            var value = property.GetValue();

            if (property.isArray)
            {
                var array = (Array)value;

                var str = new StringBuilder($"{value.GetType()} : {{");

                foreach (var element in array)
                    str.Append($"{element}, ");

                //We remove the two last characters.
                str.Remove(str.Length - 2, 2);
                str.Append("}");

                EditorGUI.LabelField(position, $"<b>{property.displayName}</b> : {str}", richTextStyle);
            }
            else
            {
                EditorGUI.LabelField(position, $"<b>{property.displayName}</b> : {value}", richTextStyle);
            }
        }

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            if (richTextStyle == null)
                InitializeStyle();

            return new Label($"<b>{property.displayName}</b> : {property.GetValue()}") { enableRichText = true };
        }

        private void InitializeStyle()
        {
            richTextStyle = new GUIStyle
            {
                richText = true, normal = EditorStyles.label.normal, alignment = TextAnchor.MiddleCenter, fontSize = 16
            };
        }
    }
}