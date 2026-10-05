using UnityEngine;
using Unity.Cinemachine; 
public class CameraController : Singleton<CameraController>
{

    private CinemachineCamera virtualCamera;

    private void Start()
    {
        SetPlayerCameraFollow();
    }
    public void SetPlayerCameraFollow()
    {
  
        virtualCamera = FindObjectOfType<CinemachineCamera>();

        if (virtualCamera != null)
        {
            if (PlayerController.Instance != null)
            {
   
                virtualCamera.Target.TrackingTarget = PlayerController.Instance.transform;
            }

        }
    }
}