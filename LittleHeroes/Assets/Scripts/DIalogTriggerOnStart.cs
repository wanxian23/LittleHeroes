using System.Collections;
using UnityEngine;

public class DialogTriggerOnStart : MonoBehaviour
{
    [SerializeField] private Dialog dialog;

    private IEnumerator Start()
    {
        Debug.Log("DialogTriggerOnStart: Scene loaded, starting dialog trigger...");

        // Wait one frame to ensure scene has loaded
        yield return null;

        // Wait until the DialogMessage singleton is ready
        int waitCounter = 0;
        while (DialogMessage.Instance == null || DialogMessage.Instance.gameObject == null)
        {
            waitCounter++;
            if (waitCounter > 300) // fail-safe to avoid infinite loop
            {
                Debug.LogError("DialogTriggerOnStart: DialogMessage.Instance is still null after waiting.");
                yield break;
            }
            yield return null;
        }

        Debug.Log("DialogTriggerOnStart: DialogMessage.Instance found, showing dialog...");
        DialogMessage.Instance.ShowDialog(dialog);
    }
}
