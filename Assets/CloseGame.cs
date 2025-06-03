using UnityEngine;

public class CloseGame : MonoBehaviour
{
    public void CloseApplication()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}
