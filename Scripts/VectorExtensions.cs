using UnityEngine;

public static class VectorExtensions
{
      public static Vector2 ToVector2(this Vector3 vector3)
      {
            return vector3;
      }

      public static Vector3 WithAxis(this Vector3 v, Axis axis, float newValue)
      {
            return new Vector3(
                  axis == Axis.X ? newValue : v.x,
                  axis == Axis.Y ? newValue : v.y,
                  axis == Axis.Z ? newValue : v.z
            );
      }
}

public enum Axis
{
      X, Y, Z
}