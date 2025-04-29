using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private bool playerInRange = false;
    public Camera theCamera;
    public Camera[] cameras;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a tag "Player" (customize as needed)
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered the trigger zone.");
            SwitchCameras();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left the trigger zone.");
        }
    }

    void SwitchCameras()
    {
        DisableAllCameras();
        theCamera.enabled = true;
    }

    void DisableAllCameras()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
    }
}
