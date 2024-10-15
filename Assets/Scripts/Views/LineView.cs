using Constellations.Data.Models;
using Constellations.Services;
using Constellations.Utilities;
using UnityEngine;

namespace Constellations.Views
{
  public class LineView : MonoBehaviour
  {
    private static readonly float LINE_WIDTH = 0.05f;
    
    private LineRenderer lineRenderer;
    private SettingsService settingsService;

    public void Initialize(Star from, Star to, float marginFrom, float marginTo, SettingsService settingsService)
    {
      this.settingsService = settingsService;
      To = to;
      From = from;
      lineRenderer = GetComponent<LineRenderer>();
      
      SetTransparency(0);
      
      Vector3 fromScenePosition = PositionConverter.ConvertToScenePosition(from.ra, from.dec);
      Vector3 toScenePosition = PositionConverter.ConvertToScenePosition(to.ra, to.dec);
      Vector3 pos0 = fromScenePosition + (toScenePosition - fromScenePosition).normalized * marginFrom; 
      Vector3 pos1 = toScenePosition + (fromScenePosition - toScenePosition).normalized * marginTo; 
      Vector3[] positions = { pos0, pos1, };
      
      SetLineWidth();
      lineRenderer.SetPositions(positions);

      settingsService.SettingChanged += s =>
      {
        if (s == nameof(settingsService.LineWidth))
        {
          SetLineWidth();
        }
      };
    }

    public Star From { get; private set; }

    public Star To { get; private set; }

    public void SetTransparency(float alpha)
    {
      Color color = lineRenderer.material.color;
      color.a = alpha;
      lineRenderer.material.color = color;
    }

    private void SetLineWidth()
    {
      lineRenderer.startWidth = settingsService.LineWidth;
      lineRenderer.endWidth = settingsService.LineWidth;
    }
  }
}