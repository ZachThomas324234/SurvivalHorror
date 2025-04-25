using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject place, player;
    public PlayerController pm;
    public Animator doorOpen;
    public Animator camera;

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
        doorOpen.Play("RotatePoint");
        camera.Play("CutsceneCamera");
        pm.rb.position = place.transform.position;
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
