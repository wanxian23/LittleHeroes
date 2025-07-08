using UnityEngine;

public class ShowExclamationOnApproach : MonoBehaviour
{
    public GameObject exclamationMark;
    [SerializeField] private Dialog dialog;

    [Header("Unique Key for This Mission")]
    [SerializeField] private string missionKey = "mission_book_complete";

    private bool hasShownDialog = false;
    private bool missionCompleted;

    private void Start()
    {

        //PlayerPrefs.DeleteKey("mission_book_complete");
        //PlayerPrefs.Save();
        // Check if this specific mission was completed
        missionCompleted = PlayerPrefs.GetInt(missionKey, 0) == 1;

        // Always start with the mark hidden
        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false);
        }

        // Optionally hide this object entirely if mission is done
        if (missionCompleted)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (missionCompleted) return;

        if (other.CompareTag("Player"))
        {
            if (exclamationMark != null)
                exclamationMark.SetActive(true);

            Debug.Log("Player entered trigger zone");

            if (!hasShownDialog)
            {
                hasShownDialog = true;
                Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (missionCompleted) return;

        if (other.CompareTag("Player"))
        {
            if (exclamationMark != null)
                exclamationMark.SetActive(false);

            hasShownDialog = false;

            if (DialogMessage.Instance != null)
            {
                DialogMessage.Instance.HideDialog();
            }
        }
    }

    public void Interact()
    {
        if (DialogMessage.Instance != null && dialog != null)
        {
            DialogMessage.Instance.ShowDialog(dialog);
        }
        else
        {
            Debug.LogWarning("DialogMessage.Instance or dialog is null!");
        }
    }

    // Call this when the mission is completed
    public void CompleteMission()
    {
        missionCompleted = true;

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false);
            Debug.Log("Exclamation mark hidden.");
        }
        else
        {
            Debug.LogWarning("Exclamation mark GameObject is null!");
        }

        PlayerPrefs.SetInt(missionKey, 1);
        PlayerPrefs.Save();

        // Optionally hide the GameObject to remove the marker completely
        gameObject.SetActive(false);
    }
}
