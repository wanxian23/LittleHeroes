using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBook : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Animator animator;

    [HideInInspector]
    public bool isPlacedCorrectly = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        animator = GetComponent<Animator>();

        // Add CanvasGroup if not already there
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        isPlacedCorrectly = false; // Reset status when starting drag

        // Optional: Reset animation
        if (animator != null)
            animator.SetBool("isPlaced", false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // If not placed correctly in slot, return to original position
        if (!isPlacedCorrectly)
        {
            ResetToOriginalPosition();
        }
    }

    public void ResetToOriginalPosition()
    {
        rectTransform.anchoredPosition = originalPosition;

        if (animator != null)
            animator.SetBool("isPlaced", false);
    }
}
