using UnityEngine;
using UnityEngine.EventSystems;

public class PaperObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform paperObjectRectTransform;
    [SerializeField]
    private CanvasGroup paperObjectCanvasGroup;
    [SerializeField]
    private Canvas backgroundCanvas;

    private Vector2 originalPosition;
    
    public void SpawnOnDesk()
    {
        paperObjectRectTransform.anchoredPosition = new Vector2(
            Random.Range(-200f, 200f),
            Random.Range(-200f, 200f)
        );

        paperObjectRectTransform.localRotation = Quaternion.Euler(
            0, 0, Random.Range(-100, 100)
        );
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = this.paperObjectRectTransform.anchoredPosition;
        
        // UX
        this.paperObjectCanvasGroup.alpha = 0.8f;
        
        this.paperObjectCanvasGroup.blocksRaycasts = false;

        // Bring to front
        this.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move with cursor, adjusted for canvas scaling
        this.paperObjectRectTransform.anchoredPosition += eventData.delta / this.backgroundCanvas.scaleFactor;
    } 

    public void OnEndDrag(PointerEventData eventData)
    {
        // UX
        this.paperObjectCanvasGroup.alpha = 1f;
        
        this.paperObjectCanvasGroup.blocksRaycasts = true;
    }
}

    

