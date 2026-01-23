using UnityEngine;
using UnityEngine.EventSystems;

public class RuneUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Rune rune;
    private Canvas canvas;
    private RectTransform rect;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) => canvasGroup.alpha = 0.6f;
    public void OnDrag(PointerEventData eventData)
    {
        rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) => canvasGroup.alpha = 1f;
}
