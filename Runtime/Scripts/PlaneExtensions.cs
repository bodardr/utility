using UnityEngine;

namespace Bodardr.Utility.Runtime
{
    public static class PlaneExtensions
    {
        public static bool GetIntersectingPoint(this Plane plane, Ray ray, out Vector3 point)
        {
            if (!plane.Raycast(ray, out var dist))
            {
                point = Vector3.zero;
                return false;
            }

            point = ray.GetPoint(dist);
            return true;
        }
    }
}