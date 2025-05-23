using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool playerInRange = false;
    public GameObject player;
    public PlayerController pc;
    public UI ui;
    public Camera cutsceneCamera;
    public Camera[] cameras;
    //public PlayerInput input;

    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        ui = FindAnyObjectByType<UI>();
        //input = FindAnyObjectByType<PlayerInput>();
    }

    public void Victory()
    {
        DisableAllCameras();
        Debug.Log("Exit");
        ui.exit.SetActive(true);
        //display victory screen
        //input.DeactivateInput();
    }

    void DisableAllCameras()
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object has a tag "Player" (customize as needed)
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered the trigger zone.");
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
}
