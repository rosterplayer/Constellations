using System;

namespace Constellations.Services
{
  public class SettingsService
  {
    private static readonly float EPSILON = 0.001f;
    
    private float animationDuration = 0.6f;
    private float linesAnimationDelay = 0.1f;
    private float constellationImageTransparency = 0.7f;
    private float starSize = 0.5f;
    private float lineWidth = 0.05f;

    public event Action<string> SettingChanged;

    public float AnimationDuration
    {
      get => animationDuration;
      set
      {
        if (!IsApproximately(value, animationDuration)) 
          return;
        
        animationDuration = value;
        SettingChanged?.Invoke(nameof(AnimationDuration));
      }
    } 
    
    public float LinesAnimationDelay
    {
      get => linesAnimationDelay;
      set
      {
        if (!IsApproximately(value, linesAnimationDelay)) 
          return;
        
        linesAnimationDelay = value;
        SettingChanged?.Invoke(nameof(LinesAnimationDelay));
      }
    }
    
    public float ConstellationImageTransparency
    {
      get => constellationImageTransparency;
      set
      {
        if (!IsApproximately(value, constellationImageTransparency)) 
          return;
        
        constellationImageTransparency = value;
        SettingChanged?.Invoke(nameof(ConstellationImageTransparency));
      }
    }
    
    public float StarSize
    {
      get => starSize;
      set
      {
        if (!IsApproximately(value, starSize)) 
          return;
        
        starSize = value;
        SettingChanged?.Invoke(nameof(StarSize));
      }
    }
    
    public float LineWidth
    {
      get => lineWidth;
      set
      {
        if (!IsApproximately(value, lineWidth)) 
          return;
        
        lineWidth = value;
        SettingChanged?.Invoke(nameof(LineWidth));
      }
    }

    private static bool IsApproximately(float a, float b) =>
      Math.Abs(a - b) > EPSILON;
  }
}