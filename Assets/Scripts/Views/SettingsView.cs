using Constellations.Services;
using UnityEngine;

namespace Constellations.Views
{
  public class SettingsView : MonoBehaviour
  {
    [SerializeField]
    [Range(0.3f, 2f)]
    private float AnimationDuration = 0.8f;
    
    [SerializeField]
    [Range(0f, 0.3f)]
    private float LinesAnimationDelay = 0.1f;
    
    [SerializeField]
    [Range(0f, 1f)]
    private float ConstellationImageTransparency = 0.7f;
    
    [SerializeField]
    [Range(0.2f, 1.5f)]
    private float StarSize = 0.5f;
    
    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float LineWidth = 0.05f;

    private SettingsService settingsService;
    private bool isInitialized;

    public void Initialize(SettingsService settingsService)
    {
      this.settingsService = settingsService;
      isInitialized = true;
    }

    private void OnValidate()
    {
      if (!isInitialized)
        return;
      
      settingsService.AnimationDuration = AnimationDuration;
      settingsService.LinesAnimationDelay = LinesAnimationDelay;
      settingsService.ConstellationImageTransparency = ConstellationImageTransparency;
      settingsService.StarSize = StarSize;
      settingsService.LineWidth = LineWidth;
    }
  }
}