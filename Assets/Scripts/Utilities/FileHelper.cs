using System;
using System.IO;
using UnityEngine;

namespace Constellations.Utilities
{
  public static class FileHelper
  {
    public static string[] GetAllFiles(string directoryPath, bool recursive = false, string searchPattern = "*.*")
    {
      try
      {
        if (!Directory.Exists(directoryPath))
        {
          throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");
        }

        string[] files = recursive
          ? Directory.GetFiles(directoryPath, searchPattern)
          : Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);

        return files;
      }
      catch (Exception ex)
      {
        Debug.LogError($"Error getting files: {ex.Message}");
        return Array.Empty<string>();
      }
    }
  }
}