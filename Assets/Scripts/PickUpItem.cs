using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    public bool playerInRange = false;
    public bool hasKey = false;
    public GameObject item, player, cutscenePoint, keyPosition;
    public PlayerController pc;
    public UI ui;
    public Animator doorOpen;
    public Animator camera;
    public Camera cutsceneCamera;
    public Camera[] cameras;

    private void Awake()
    {
        pc = FindAnyObjectByType<PlayerController>();
        ui = FindAnyObjectByType<UI>();
    }

    public void ItemGrabbed()
    {
        gameObject.transform.position = keyPosition.transform.position;
        hasKey = true;
        ui.keyText.SetActive(true);
        StartCoroutine(DeactivateText());
    }

    IEnumerator DeactivateText()
    {
        yield return new WaitForSeconds(3f);
        ui.keyText.SetActive(false);
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
