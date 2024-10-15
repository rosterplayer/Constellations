using UnityEngine;

namespace Constellations.Camera
{
  public class CameraController
  {
    private readonly Transform cameraTransform;

    public CameraController(Transform cameraTransform)
    {
      this.cameraTransform = cameraTransform;
    }

    public void LookAt(Vector3 at)
    {
      cameraTransform.LookAt(at, Vector3.up);
    }
  }
}