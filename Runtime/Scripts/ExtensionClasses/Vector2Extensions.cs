using UnityEngine;
public static class Vector2Extensions
{
    public static float Lerp(this Vector2 range, float t) => Mathf.Lerp(range.x, range.y, t);
}