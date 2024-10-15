using System.Collections.Generic;
using System.Linq;
using Constellations.Camera;
using Constellations.Services;

namespace Constellations.Views
{
  public class ConstellationViewer
  {
    private readonly ConstellationFactory _constellationFactory;
    private readonly CameraController _cameraController;
    private readonly DataService dataService;
    private List<ConstellationView> constellations = new ();
    private int currentConstellation;

    public ConstellationViewer(DataService dataService, ConstellationFactory constellationFactory, CameraController cameraController)
    {
      _constellationFactory = constellationFactory;
      _cameraController = cameraController;
      this.dataService = dataService;
      
      InstantiateConstellations();
      if (constellations.Count > 0)
      {
        CurrentConstellation = constellations[currentConstellation];
        _cameraController.LookAt(CurrentConstellation.ScenePosition);
        SetActive(false, CurrentConstellation);
      }
    }
    
    public ConstellationView CurrentConstellation { get; private set; }

    public void SwitchConstellation()
    {
      if (constellations.Count <= 0)
        return;
      
      currentConstellation++;
      if (currentConstellation >= constellations.Count)
        currentConstellation = 0;

      CurrentConstellation = constellations[currentConstellation];
      CurrentConstellation.gameObject.SetActive(true);
      
      _cameraController.LookAt(CurrentConstellation.ScenePosition);
      
      SetActive(false, CurrentConstellation);
    }

    public void SwitchAnimation()
    {
      if (constellations.Count <= 0)
        return;

      if (CurrentConstellation.IsShowed)
        CurrentConstellation.ShowFadeOutAnimation();
      else
        CurrentConstellation.ShowFadeInAnimation();
    }

    private void InstantiateConstellations()
    {
      foreach (string constellationName in dataService.ConstellationNames)
      {
        constellations.Add(_constellationFactory.CreateConstellation(constellationName));
      }
    }

    private void SetActive(bool isActive, ConstellationView exception)
    {
      foreach (ConstellationView view in constellations.Where(x => x != exception))
      {
        view.gameObject.SetActive(isActive);
      }
    }
  }
}