using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject questLog; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleInventory()
    {
        inventory.SetActive(inventory.activeSelf ? false : true);        
    }

    public void ToggleQuestLog()
    {
        questLog.SetActive(!questLog.activeSelf);        
    }
}
