using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildableTile : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        
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
