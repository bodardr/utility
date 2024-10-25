using UnityEngine;

namespace Bodardr.Utility.Editor
{
    public static class GUIStyleExtensions
    {
        public static void Assign(this GUIStyle a, GUIStyle b)
        {
            a.active = b.active;
            a.focused = b.focused;
            a.hover = b.hover;
            a.normal = b.normal;
            
            a.onActive = b.onActive;
            a.onFocused = b.onFocused;
            a.onHover = b.onHover;
            a.onNormal = b.onNormal;
        }
    }
}