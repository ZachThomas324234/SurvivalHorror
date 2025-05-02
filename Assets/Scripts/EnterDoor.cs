using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterDoor : MonoBehaviour
{
    public bool playerInRange = false;
    public GameObject place, player, cutscenePoint;
    public PlayerController pc;
    public Animator doorOpen;
    public Animator camera;
    public Camera cutsceneCamera;
    public Camera[] cameras;
    public PlayerInput input;

    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        input = FindAnyObjectByType<PlayerInput>();
    }

    private void Update()
    {
        // Check if player is in range and the interact key is pressed
    }

    public void DoorEntered()
    {
        DisableAllCameras();
        doorOpen.Play("RotatePoint", -1, 0);
        camera.Play("CutsceneCamera", -1, 0);
        //disable player input
        //play sound effects
        cutsceneCamera.enabled = true;
        input.DeactivateInput();
        StartCoroutine(CutscenePlay());
    }

    IEnumerator CutscenePlay()
    {
        yield return new WaitForSeconds(3.6f);
        pc.rb.position = place.transform.position;
        input.ActivateInput();
        yield return new WaitForSeconds(0.4f);
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
