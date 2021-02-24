using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildableTile : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop: " + name);
        FindObjectOfType<GameManager>().InstantiateTower(transform.position);
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnMouseDrag: " + name);
        //FindObjectOfType<GameManager>().InstantiateTower(transform.position);
    }

    private void OnMouseUp() {
        Debug.Log("Let go at " + gameObject.name);
    }

    private void OnMouseExit() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
