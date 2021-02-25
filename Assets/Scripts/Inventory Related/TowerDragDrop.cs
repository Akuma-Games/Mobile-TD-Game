using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TowerDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    [SerializeField] TowerType towerType;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        FindObjectOfType<GameManager>().CurrentTowerBuilding = towerType;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        rectTransform.position = originalPosition;
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }


}
