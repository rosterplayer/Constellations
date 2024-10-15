using Constellations.Views;
using UnityEngine;

namespace Constellations.UI
{
  public class UIController : MonoBehaviour
  {
    private bool isInitialized;
    private ConstellationViewer viewer;

    public void Initialize(ConstellationViewer viewer)
    {
      this.viewer = viewer;
      isInitialized = true;
    }

    public void SwitchConstellation()
    {
      if (!isInitialized)
        return;
      
      viewer.SwitchConstellation();
    }

    public void SwitchAnimation()
    {
      if (!isInitialized)
        return;
      
      viewer.SwitchAnimation();
    }
  }
}