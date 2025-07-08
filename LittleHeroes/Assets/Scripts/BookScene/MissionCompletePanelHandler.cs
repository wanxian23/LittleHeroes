using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompletePanelHandler : MonoBehaviour
{
    // Call this from the Button's OnClick
    public void OnContinueButtonClicked()
    {
        // Example: Load next scene
        SceneManager.LoadScene("BedRoom"); // Replace with your real scene name
    }

    // You could also do something like:
    // public void OnClosePanel() => gameObject.SetActive(false);
}