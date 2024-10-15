using System.Collections.Generic;
using System.IO;
using Constellations.Data.Models;
using Constellations.Utilities;
using UnityEngine;

namespace Constellations.Services
{
  public class DataService
  {
    private static readonly string CONFIGS_DIRECTORY = "ConstellationConfigs"; 
    
    private Dictionary<string, Constellation> constellations = new ();

    public IEnumerable<string> ConstellationNames => constellations.Keys;

    public void LoadData()
    {
      constellations.Clear();
      
      string directoryPath = Path.Combine(Application.streamingAssetsPath, CONFIGS_DIRECTORY);
      var configsPaths = FileHelper.GetAllFiles(directoryPath, false, "*.json");

      foreach (var path in configsPaths)
      {
        string text = File.ReadAllText(path);
        var config = JsonUtility.FromJson<ConstellationConfig>(text);
        foreach (var constellation in config.items)
        {
          constellations.TryAdd(constellation.name, constellation);
        }
      }
    }

    public Constellation GetConstellationData(string name)
    {
      return constellations.TryGetValue(name, out var value) ? value : null;
    }
  }
}