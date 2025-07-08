using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance { get; private set; }

    public GameObject missionCompletePanel; // Drag your panel here
    private int correctCount = 0;
    private int totalSlots = 8;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(false); // Hide initially
    }

    public void NotifySlotCorrect()
    {
        correctCount++;

        if (correctCount >= totalSlots)
        {
            ShowMissionComplete();
        }
    }

    private void ShowMissionComplete()
    {
        if (missionCompletePanel != null)
            missionCompletePanel.SetActive(true);

        // Save mission completion
        PlayerPrefs.SetInt("mission_book_complete", 1);
        PlayerPrefs.Save();

        // Don't try to find the exclamation mark in this scene — it's in the previous one!
    }

}
