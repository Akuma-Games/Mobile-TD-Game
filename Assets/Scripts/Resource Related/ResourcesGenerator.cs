using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourcesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject goldPrefab;
    [SerializeField] private GameObject stonePrefab;
    [SerializeField] private GameObject woodPrefab;

    public GameObject createResource(ResourceType type)
    {
        GameObject tempObject = null;
        switch (type)
        {
            case ResourceType.GOLD:
                tempObject = Instantiate(goldPrefab);
                break;
            case ResourceType.STONE:
                tempObject = Instantiate(stonePrefab);
                break;
            case ResourceType.WOOD:
                tempObject = Instantiate(woodPrefab);
                break;
        }

        tempObject.transform.parent = transform;
        tempObject.SetActive(false);
        
        return tempObject;
    }
}
