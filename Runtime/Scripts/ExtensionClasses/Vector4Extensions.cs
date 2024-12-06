using UnityEngine;
public static class Vector4Extensions
{
    public static float Remap(this Vector4 remapValues, float value) => Mathf.Lerp(remapValues.z, remapValues.w,
        Mathf.InverseLerp(remapValues.x, remapValues.y, value));
}
