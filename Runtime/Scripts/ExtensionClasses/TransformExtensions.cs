using System.Text;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindWithTag(this Transform tr, string tag)
    {
        foreach (Transform transform in tr)
        {
            if (transform.CompareTag(tag))
                return transform;

            if (transform.childCount <= 0)
                continue;

            var val = transform.FindWithTag(tag);

            if (val)
                return val;
        }

        return null;
    }

    public static Transform[] GetChildren(this Transform tr)
    {
        var children = new Transform[tr.childCount];

        for (int i = 0; i < children.Length; i++)
            children[i] = tr.GetChild(i);

        return children;
    }

    /// <returns>The full scene path of this GameObject</returns>
    public static string GetFullPath(this Transform tr)
    {
        var parents = tr.GetComponentsInParent<Transform>();

        var str = new StringBuilder(parents[^1].name);
        for (var i = parents.Length - 2; i >= 0; i--)
            str.Append($"/{parents[i].name}");

        return str.ToString();
    }
}