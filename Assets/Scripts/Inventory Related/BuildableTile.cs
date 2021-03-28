using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildableTile : MonoBehaviour, IDropHandler
{
    public int index = 0;
    public TowerType currentTower = TowerType.NONE;

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop: " + name);
        FindObjectOfType<GameManager>().BuildTower(this);
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
