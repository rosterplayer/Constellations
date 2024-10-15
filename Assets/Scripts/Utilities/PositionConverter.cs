using UnityEngine;

namespace Constellations.Utilities
{
  public class PositionConverter
  {
    private static readonly float RADIUS = 10;
    private static readonly double HOURS_2_DEG = 15.0;

    public static Vector3 ConvertToScenePosition(double ra, double dec)
    {
      float x = RADIUS * Mathf.Cos((float)(dec * Mathf.Deg2Rad)) * Mathf.Sin((float)(ra * HOURS_2_DEG * Mathf.Deg2Rad));
      float y = RADIUS * Mathf.Cos((float)(dec * Mathf.Deg2Rad)) * Mathf.Cos((float)(ra * HOURS_2_DEG * Mathf.Deg2Rad));
      float z = RADIUS * Mathf.Sin((float)(dec * Mathf.Deg2Rad));
      
      return new Vector3(x, y, z);
    }
  }
}