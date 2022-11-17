using UnityEditor;
using UnityEngine.InputSystem.Editor;

public class MouseToViewportProcessorEditor : InputParameterEditor<MouseToViewportProcessor>
{
    public override void OnGUI() => target.type = (ViewportProcessingType)EditorGUILayout.EnumPopup("Type", target.type);
}