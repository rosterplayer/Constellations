using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Constellations.Data.Models;
using Constellations.Services;
using Constellations.Utilities;
using Constellations.Views;
using UnityEngine;

namespace Constellations.Animation
{
  public class LinesAnimationController : MonoBehaviour
  {
    private Constellation data;
    private List<LineView> lines;
    private SpriteRenderer constellationImage;

    private bool isVisible;
    private int starToStart;
    private Coroutine currentAnimation;
    private SettingsService settingsService;
    private List<List<LineView>> animationSortedLines = new ();

    public void Initialize(Constellation data, List<LineView> lines, SpriteRenderer constellationImage, SettingsService settingsService)
    {
      this.settingsService = settingsService;
      this.constellationImage = constellationImage;
      this.lines = lines;
      this.data = data;

      starToStart = FindClosestStar();
      FillLineAnimationOrder();
    }

    public void Show()
    {
      if (currentAnimation != null)
        StopCoroutine(currentAnimation);
      
      currentAnimation = StartCoroutine(FadeIn());
    }

    public void Hide()
    {
      if (currentAnimation != null)
        StopCoroutine(currentAnimation);
      
      currentAnimation = StartCoroutine(FadeOut());
    }

    private int FindClosestStar()
    {
      var starIds = new HashSet<int>();
      foreach (LineView lineView in lines)
      {
        starIds.Add(lineView.From.id);
        starIds.Add(lineView.To.id);
      }
      
      Vector3 center = PositionConverter.ConvertToScenePosition(data.ra, data.dec);
      float minDistance = float.MaxValue;
      int closestStar = 0;
      foreach (int starId in starIds)
      {
        Star star = data.stars.FirstOrDefault(x => x.id == starId);
        if (star == null)
          continue;
        
        Vector3 starPosition = PositionConverter.ConvertToScenePosition(star.ra, star.dec);
        float sqrMagnitude = (center - starPosition).sqrMagnitude;
        if (!(sqrMagnitude < minDistance))
          continue;
        
        minDistance = sqrMagnitude;
        closestStar = starId;
      }

      return closestStar;
    }

    private IEnumerator FadeIn()
    {
      SetLinesTransparency(lines, 0f);
      SetImageTransparency(0f);
      
      float stageDuration = settingsService.AnimationDuration / (animationSortedLines.Count + 1);
      
      for (float t = 0; t <= 1; t += Time.deltaTime / stageDuration)
      {
        SetImageTransparency(Mathf.Lerp(0, settingsService.ConstellationImageTransparency, t));
        yield return null;
      }
      
      yield return new WaitForSeconds(settingsService.LinesAnimationDelay);

      foreach (var linesToAnimate in animationSortedLines)
      {
        for (float t = 0; t <= 1; t += Time.deltaTime / stageDuration)
        {
          SetLinesTransparency(linesToAnimate, Mathf.Lerp(0, 1f, t));
          yield return null;
        }
      }

      SetLinesTransparency(lines, 1f);
      SetImageTransparency(settingsService.ConstellationImageTransparency);
    }

    private void FillLineAnimationOrder()
    {
      HashSet<LineView> handledLines = new HashSet<LineView>();
      var currentStars = new List<int>() { starToStart };
      while (currentStars.Any())
      {
        var toAnimate = GetLinesToAnimate(currentStars, handledLines, out var nextStars).ToList();
        if (toAnimate.Count > 0)
          animationSortedLines.Add(toAnimate);
        foreach (LineView lineView in toAnimate)
        {
          handledLines.Add(lineView);
        }
        currentStars = new List<int>(nextStars);
      }
    }

    private IEnumerable<LineView> GetLinesToAnimate(IEnumerable<int> starIds, ICollection<LineView> handled, out ICollection<int> nextStars)
    {
      var result = new List<LineView>();
      nextStars = new List<int>();
      foreach (int starId in starIds)
      {
        foreach (LineView lineView in lines.Where(x => !handled.Contains(x) && !result.Contains(x)))
        {
          if (lineView.From.id == starId)
          {
            result.Add(lineView);
            nextStars.Add(lineView.To.id);
          }
          else if (lineView.To.id == starId)
          {
            result.Add(lineView);
            nextStars.Add(lineView.From.id);
          }
        }
      }

      return result;
    }

    private IEnumerator FadeOut()
    {
      for (float t = 0; t <= 1; t += Time.deltaTime / (settingsService.AnimationDuration))
      {
        SetLinesTransparency(lines, Mathf.Lerp(1f, 0f, t));
        SetImageTransparency(Mathf.Lerp(settingsService.ConstellationImageTransparency, 0f, t));
        yield return null;
      }

      SetLinesTransparency(lines, 0f);
      SetImageTransparency(0f);
    }

    private void SetImageTransparency(float alpha)
    {
      Color imageCurrentColor = constellationImage.color;
      imageCurrentColor.a = alpha;
      constellationImage.color = imageCurrentColor;
    }

    private void SetLinesTransparency(IEnumerable<LineView> lines, float alpha)
    {
      foreach (LineView line in lines)
      {
        line.SetTransparency(alpha);
      }
    }
  }
}