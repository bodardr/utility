using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Bodardr.Utility.Editor
{
    [CustomEditor(typeof(AnimatorStateCache))]
    public class AnimatorStateCacheEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            root.Add(new PropertyField(serializedObject.FindProperty("m_Script"))
            {
                style =
                {
                    flexGrow = 1,
                }
            });

            var stateInfo = new VisualElement
            {
                name = "root",
                style =
                {
                    paddingLeft = 4,
                    paddingTop = 8,
                    display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex),
                    flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row),
                    flexWrap = new StyleEnum<Wrap>(Wrap.Wrap)
                }
            };

            var anim = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(
                AssetDatabase.GUIDToAssetPath(((AnimatorStateCache)target).AnimatorControllerGUID));

            stateInfo.Add(new Label($"This state cache is assigned to ")
                { style = { unityTextAlign = new StyleEnum<TextAnchor>(TextAnchor.MiddleLeft) } });

            var objectField = new ObjectField
            {
                focusable = false,
                delegatesFocus = false,
                value = anim,
                style =
                {
                    height = 18,
                }
            };

            var objectFieldSelector = objectField[0][1];
            objectFieldSelector.visible = false;

            stateInfo.Add(objectField);

            root.Add(stateInfo);

            return root;
        }
    }
}