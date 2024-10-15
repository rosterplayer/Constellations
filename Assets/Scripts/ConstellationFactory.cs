using System;
using Constellations.Animation;
using Constellations.Data;
using Constellations.Data.Models;
using Constellations.Services;
using Constellations.Views;
using UnityEngine;

namespace Constellations
{
  public class ConstellationFactory
  {
    private readonly DataService dataService;
    private readonly SpritesProvider spritesProvider;
    private readonly PrefabRegistry prefabs;
    private readonly SettingsService settingsService;

    public ConstellationFactory(DataService dataService, SpritesProvider spritesProvider, PrefabRegistry prefabs, SettingsService settingsService)
    {
      this.dataService = dataService;
      this.spritesProvider = spritesProvider;
      this.prefabs = prefabs;
      this.settingsService = settingsService;
    }

    public ConstellationView CreateConstellation(string name)
    {
      Constellation data = dataService.GetConstellationData(name);

      var view = new GameObject(data.name);
      var viewComponent = view.AddComponent<ConstellationView>();
      view.AddComponent<LinesAnimationController>();
      viewComponent.Initialize(data, spritesProvider, prefabs, settingsService);
      
      return viewComponent;
    }
  }
}