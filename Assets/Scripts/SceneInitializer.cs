using Constellations.Camera;
using Constellations.Data;
using Constellations.Services;
using Constellations.UI;
using Constellations.Views;
using UnityEngine;

namespace Constellations
{
  public class SceneInitializer : MonoBehaviour
  {
    private DataService dataService;
    private SpritesProvider spritesProvider;
    private ConstellationFactory constellationFactory;
    private CameraController cameraController;
    private PrefabRegistry prefabRegistry;
    private ConstellationViewer constellationViewer;
    private SettingsService settingsService;

    private void Awake()
    {
      dataService = new DataService();
      dataService.LoadData();

      settingsService = new SettingsService();
      
      prefabRegistry = Resources.Load<PrefabRegistry>("Prefabs");

      spritesProvider = new SpritesProvider();
      spritesProvider.LoadSpritesData();

      cameraController = new CameraController(UnityEngine.Camera.main.transform);
      
      constellationFactory = new ConstellationFactory(dataService, spritesProvider, prefabRegistry, settingsService);

      constellationViewer = new ConstellationViewer(dataService, constellationFactory, cameraController);

      var settingsView = FindObjectOfType<SettingsView>();
      settingsView.Initialize(settingsService);

      var uiController = FindObjectOfType<UIController>();
      uiController.Initialize(constellationViewer);
    }
  }
}