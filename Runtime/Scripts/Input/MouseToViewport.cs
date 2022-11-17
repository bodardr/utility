using System;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
[Serializable]
#endif
public class MouseToViewportProcessor : InputProcessor<Vector2>
{
    private static readonly Vector2 half = new(0.5f, 0.5f);
    private static Vector2 Resolution => new(Display.main.systemWidth, Display.main.systemHeight);

    public ViewportProcessingType type;

#if UNITY_EDITOR
    static MouseToViewportProcessor()
    {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        InputSystem.RegisterProcessor<MouseToViewportProcessor>();
    }

    public override Vector2 Process(Vector2 value, InputControl control)
    {
        return type == ViewportProcessingType.Default ? value / Resolution : value / Resolution - half;
    }
}

public enum ViewportProcessingType
{
    Default,
    Centered
}