using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject place, player, cutscenePoint;
    public PlayerController pm;
    public Animator doorOpen;
    public Animator camera;
    public Camera cutsceneCamera;
    public Camera[] cameras;

    private void Awake()
    {
        pm = FindAnyObjectByType<PlayerController>();   
    }

    private void Update()
    {
        // Check if player is in range and the interact key is pressed
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DoorEntered();
        }
    }

    private void DoorEntered()
    {
        DisableAllCameras();
        //pm.rb.position = cutscenePoint.transform.position;
        doorOpen.Play("RotatePoint", -1, 0);
        camera.Play("CutsceneCamera", -1, 0);
        //disable player input
        //play sound effects
        cutsceneCamera.enabled = true;
        StartCoroutine(CutscenePlay());
    }

    IEnumerator CutscenePlay()
    {
        yield return new WaitForSeconds(4.5f);
        pm.rb.position = place.transform.position;
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
