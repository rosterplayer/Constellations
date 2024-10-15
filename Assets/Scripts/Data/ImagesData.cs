using System.Collections.Generic;
using UnityEngine;

namespace Constellations.Data
{
  [CreateAssetMenu(fileName = "Images", menuName = "Data/Images", order = 0)]
  public class ImagesData : ScriptableObject
  {
    public List<Sprite> Stars;
    public List<Sprite> Constellations;
  }
}