using Constellations.Views;
using UnityEngine;

namespace Constellations.Data
{
  [CreateAssetMenu(fileName = "Prefabs", menuName = "Data/Prefabs", order = 0)]
  public class PrefabRegistry : ScriptableObject
  {
    public LineView LinePrefab;
  }
}