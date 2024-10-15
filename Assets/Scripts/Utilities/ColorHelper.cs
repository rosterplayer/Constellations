using UnityEngine;

namespace Constellations.Utilities
{
  public class ColorHelper
  {
    public static Color StringToColor(string hexColor)
    {
      int r = int.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
      int g = int.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
      int b = int.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

      return new Color(r / 255f, g / 255f, b / 255f, 1f);
    }
  }
}