using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class TowerDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    [SerializeField] TowerType towerType;
    [SerializeField] LayerMask layerMask;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.position;
    }

    void Start()
    {

    }
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        FindObjectOfType<GameManager>().CurrentTowerBuilding = towerType;
    }

    public void OnDrag(PointerEventData eventData) {
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        rectTransform.position = originalPosition;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        if (hit.collider != null) {
            Debug.Log(hit.collider.name);

            FindObjectOfType<GameManager>().BuildTower(hit.collider.GetComponent<BuildableTile>());
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        
    }


}
