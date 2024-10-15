using System.Collections.Generic;
using Constellations.Data;
using UnityEngine;

namespace Constellations.Services
{
  public class SpritesProvider
  {
    private static readonly string IMAGES_DATA_FOLDER = "ImagesData";
    private readonly Dictionary<string, Sprite> constellationSprites = new ();
    private readonly List<Sprite> starSprites = new ();

    public void LoadSpritesData()
    {
      constellationSprites.Clear();
      starSprites.Clear();
      
      var data = Resources.LoadAll<ImagesData>(IMAGES_DATA_FOLDER);
      foreach (var imagesData in data)
      {
        HandleData(imagesData);
      }
    }

    public Sprite GetConstellationSprite(string name) =>
      constellationSprites.TryGetValue(name, out Sprite sprite) ? sprite : null;

    public Sprite GetRandomStarSprite() =>
      starSprites[Random.Range(0, starSprites.Count)];

    private void HandleData(ImagesData imagesData)
    {
      starSprites.AddRange(imagesData.Stars);
      foreach (Sprite sprite in imagesData.Constellations)
      {
        constellationSprites.TryAdd(sprite.name, sprite);
      }
    }
  }
}