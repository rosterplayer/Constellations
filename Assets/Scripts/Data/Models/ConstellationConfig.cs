using System;
using System.Collections.Generic;

namespace Constellations.Data.Models
{
  [Serializable]
  public class ConstellationConfig
  {
    public List<Constellation> items;
  }

  [Serializable]
  public class Constellation
  {
    public string name;
    public double ra;
    public double dec;
    public Image image;
    public List<Pair> pairs;
    public List<Star> stars;
  }
  
  [Serializable]
  public class Image
  {
    public double ra;
    public double dec;
    public double scale;
    public double angle;
  }
  
  [Serializable]
  public class Pair
  {
    public int from;
    public int to;
  }
  
  [Serializable]
  public class Star
  {
    public int id;
    public double ra;
    public double dec;
    public double magnitude;
    public string color;
  }
}