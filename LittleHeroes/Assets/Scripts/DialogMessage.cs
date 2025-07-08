using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogMessage : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond = 20;
    [SerializeField] GameObject bookCanvas;

    public static DialogMessage Instance { get; private set; }

    private List<string> lines;
    private int currentLineIndex;
    private bool isTyping = false;
    private bool canContinue = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // This is an extra, leftover copy
        }
    }

    public void ShowDialog(Dialog dialog)
    {
        if (dialog == null || dialogBox == null || dialogText == null)
        {
            Debug.LogError("DialogMessage: Missing references in ShowDialog!");
            Debug.LogWarning($"Dialog is null? {dialog == null}, dialogBox: {dialogBox}, dialogText: {dialogText}");
            return;
        }

        dialogBox.SetActive(true);
        lines = dialog.Lines;
        currentLineIndex = 0;
        StartCoroutine(TypeDialog(lines[currentLineIndex]));
    }


    private void Update()
    {
        if (dialogBox == null || dialogText == null || !dialogBox.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogText.text = lines[currentLineIndex];
                isTyping = false;
                canContinue = true;
            }
            else if (canContinue)
            {
                currentLineIndex++;
                if (currentLineIndex < lines.Count)
                {
                    StartCoroutine(TypeDialog(lines[currentLineIndex]));
                }
                else
                {
                    dialogBox.SetActive(false); // Hide dialog box

                    if (bookCanvas != null)
                        bookCanvas.SetActive(true); // Show the book puzzle
                    else
                        LoadNextScene();
                }
            }
        }
    }


    private void LoadNextScene()
    {
        // You can load by name or by index
        // Example: load scene by name
        SceneManager.LoadScene("BookShelf");

        // Or by index (e.g., next scene in build settings)
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator TypeDialog(string line)
    {
        dialogText.text = "";
        isTyping = true;
        canContinue = false;

        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }

        isTyping = false;
        canContinue = true;
    }

    public void HideDialog()
    {
        dialogBox.SetActive(false);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
