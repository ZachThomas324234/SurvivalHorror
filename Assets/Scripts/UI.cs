using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject exit, keyText, starterText, escapeText, menu, exitButton;

    void Awake()
    {
        menu.SetActive(false);
        exitButton.SetActive(false);
        escapeText.SetActive(true);
        starterText.SetActive(true);
        keyText.SetActive(false);
        exit.SetActive(false);
        StartCoroutine(DeactivateText());
    }

    public IEnumerator DeactivateText()
    {
        yield return new WaitForSeconds(3f);
        starterText.SetActive(false);
        escapeText.SetActive(false);
    }
}
