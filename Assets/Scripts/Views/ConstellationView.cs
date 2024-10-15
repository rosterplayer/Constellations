using System.Collections.Generic;
using System.Linq;
using Constellations.Animation;
using Constellations.Data;
using Constellations.Data.Models;
using Constellations.Services;
using Constellations.Utilities;
using Constellations.Views;
using UnityEngine;

namespace Constellations.Views
{
  public class ConstellationView : MonoBehaviour
  {
    private static readonly float STAR_MARGIN = 0.07f;

    private readonly List<LineView> lines = new ();
    private Constellation data;
    private SpritesProvider spritesProvider;
    private SpriteRenderer constellationImage;
    private LinesAnimationController animationController;
    private PrefabRegistry prefabs;
    private Dictionary<int, SpriteRenderer> starRenderers = new ();
    private SettingsService settingsService;
    private float starScaleFactor;

    public Vector3 ScenePosition { get; private set; }
    
    public bool IsShowed { get; private set; }

    public void Initialize(Constellation data, SpritesProvider spritesProvider, PrefabRegistry prefabs, SettingsService settingsService)
    {
      this.settingsService = settingsService;
      this.prefabs = prefabs;
      this.spritesProvider = spritesProvider;
      this.data = data;

      starScaleFactor = settingsService.StarSize;
      ScenePosition = PositionConverter.ConvertToScenePosition(data.ra, data.dec);
      
      InstantiateImage();
      InstantiateStars();
      InstantiateLines();

      animationController = GetComponent<LinesAnimationController>();
      animationController.Initialize(data, lines, constellationImage, settingsService);

      settingsService.SettingChanged += s =>
      {
        if (nameof(settingsService.StarSize) == s)
        {
          starScaleFactor = settingsService.StarSize;
          OnStarSizeChanged();
        }
      };
    }

    private void OnStarSizeChanged()
    {
      foreach (var kvp in starRenderers)
      {
        double starMagnitude = data.stars.First(x => x.id == kvp.Key).magnitude;
        kvp.Value.gameObject.transform.localScale = (Vector3.one * starScaleFactor) / (float)starMagnitude;
      }
    }

    public void ShowFadeInAnimation()
    {
      animationController.Show();
      IsShowed = true;
    }

    public void ShowFadeOutAnimation()
    {
      animationController.Hide();
      IsShowed = false;
    }

    private void InstantiateImage()
    {
      Vector3 position = PositionConverter.ConvertToScenePosition(data.image.ra, data.image.dec);
      Sprite sprite = spritesProvider.GetConstellationSprite(data.name);
      
      var go = new GameObject("image");
      
      constellationImage = go.AddComponent<SpriteRenderer>();
      constellationImage.sprite = sprite;
      SetTransparent(constellationImage);

      Transform imageTransform = constellationImage.transform;
      imageTransform.position = position;
      imageTransform.localScale *=  (float)data.image.scale / 2f;
      Vector3 lookDirection = (Vector3.zero - position).normalized;
      Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
      imageTransform.rotation = lookRotation * Quaternion.Euler(Vector3.forward * (float)data.image.angle);
      imageTransform.parent = gameObject.transform;
    }

    private void InstantiateStars()
    {
      foreach (Star star in data.stars)
      {
        Vector3 position = PositionConverter.ConvertToScenePosition(star.ra, star.dec);
        Sprite sprite = spritesProvider.GetRandomStarSprite();

        var go = new GameObject($"star_{star.id}");
        
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = ColorHelper.StringToColor(star.color);

        Transform starTransform = go.transform;
        starTransform.position = position;
        starTransform.localScale = (Vector3.one * starScaleFactor) / (float)star.magnitude;
        starTransform.LookAt(Vector3.zero, Vector3.up);
        starTransform.parent = gameObject.transform;
        
        starRenderers.Add(star.id, spriteRenderer);
      }
    }

    private void InstantiateLines()
    {
      lines.Clear();
      
      foreach (Pair pair in data.pairs)
      {
        Star from = data.stars.FirstOrDefault(x => x.id == pair.from);
        Star to = data.stars.FirstOrDefault(x => x.id == pair.to);
        if (from == null || to == null)
          continue;

        float marginFrom = STAR_MARGIN * starRenderers[from.id].size.x; 
        float marginTo = STAR_MARGIN * starRenderers[to.id].size.x; 
        var line = Instantiate(prefabs.LinePrefab, gameObject.transform);
        line.Initialize(from, to, marginFrom, marginTo, settingsService);
        lines.Add(line);
      }
    }

    private static void SetTransparent(SpriteRenderer spriteRenderer)
    {
      Color color = spriteRenderer.color;
      color.a = 0;
      spriteRenderer.color = color;
    }
  }
}