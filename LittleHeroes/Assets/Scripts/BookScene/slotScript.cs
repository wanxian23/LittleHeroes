using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BookSlot : MonoBehaviour, IDropHandler
{
    public List<string> acceptedBookNames = new List<string>();
    public Text errorText; // Assign in Inspector

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;

        if (dropped != null && acceptedBookNames.Contains(dropped.name))
        {
            RectTransform droppedRect = dropped.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            // Snap book to slot position
            droppedRect.anchoredPosition = slotRect.anchoredPosition;
            droppedRect.localScale = Vector3.one;

            // Optional: keep same parent or reset
            droppedRect.SetParent(slotRect.parent, false);

            // Force it to render on top (this line here)
            dropped.transform.SetAsLastSibling();

            // Play flip animation
            Animator animator = dropped.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetBool("isPlaced", true);
            }

            // If you use DraggableBook tracking
            DraggableBook dragScript = dropped.GetComponent<DraggableBook>();
            if (dragScript != null)
                dragScript.isPlacedCorrectly = true;

            Debug.Log("Book placed correctly: " + dropped.name);

            
            SlotManager.Instance.NotifySlotCorrect();
        } else
        {
            // Wrong placement: Show error message at this slot
            if (errorText != null)
            {
                errorText.text = "Salah Tempat!";
                errorText.transform.position = transform.position + new Vector3(0, 50, 0); // Offset slightly above
                errorText.gameObject.SetActive(true);

                // Start coroutine to hide after 2s
                StartCoroutine(HideErrorText());
            }
        }
    }

    private IEnumerator HideErrorText()
    {
        yield return new WaitForSeconds(2f);
        if (errorText != null)
        {
            errorText.text = "";
            errorText.gameObject.SetActive(false);
        }
    }

}
