using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum ResourceType
{
    GOLD,
    STONE,
    WOOD
}

[System.Serializable]
public class ResourceManager : MonoBehaviour
{
    public ResourcesGenerator generator;
    public int goldObjectMax;
    public int stoneObjectMax;
    public int woodObjectMax;

    public int gold = 90;
    public int stone = 0;
    public int wood = 0;

    public TMP_Text goldAmount;
    public TMP_Text stoneAmount;
    public TMP_Text woodAmount;

    private Queue<GameObject> goldPool;
    private Queue<GameObject> stonePool;
    private Queue<GameObject> woodPool;

    private static ResourceManager m_Instance;

    public static ResourceManager Instance
    {
        get
        {
            if (m_Instance == null){
                if (FindObjectOfType<ResourceManager>() != null)
                {
                    m_Instance = FindObjectOfType<ResourceManager>();
                }
                else
                {
                    GameObject rm = new GameObject("ResourceManager");
                    m_Instance = rm.AddComponent<ResourceManager>();
                } 
            }
            return m_Instance;
        }
    }

    void Start()
    {
        BuildResourcePool();
        goldAmount.text = gold.ToString();
        stoneAmount.text = stone.ToString();
        woodAmount.text = wood.ToString();
    }

    private void BuildResourcePool()
    {
        goldPool = new Queue<GameObject>();
        for (int count = 0; count < goldObjectMax; count++)
        {
            var tempGold = generator.createResource(ResourceType.GOLD);
            goldPool.Enqueue(tempGold);
        }

        stonePool = new Queue<GameObject>();
        for (int count = 0; count < stoneObjectMax; count++)
        {
            var tempStone = generator.createResource(ResourceType.STONE);
            stonePool.Enqueue(tempStone);
        }

        woodPool = new Queue<GameObject>();
        for (int count = 0; count < woodObjectMax; count++)
        {
            var tempWood = generator.createResource(ResourceType.STONE);
            woodPool.Enqueue(tempWood);
        }
    }
    public void CollectResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.GOLD:
                gold += amount;
                goldAmount.text = gold.ToString();
                break;
            case ResourceType.STONE:
                stone += amount;
                stoneAmount.text = stone.ToString();
                break;
            case ResourceType.WOOD:
                wood += amount;
                woodAmount.text = wood.ToString();
                break;
        }
    }

    public GameObject GetResource(ResourceType type, Vector3 position)
    {
        switch (type)
        {
            case ResourceType.GOLD:
                var newGoldObject = goldPool.Dequeue();
                newGoldObject.SetActive(true);
                newGoldObject.transform.position = position;
                return newGoldObject;
            case ResourceType.STONE:
                var newStoneObject = stonePool.Dequeue();
                newStoneObject.SetActive(true);
                newStoneObject.transform.position = position;
                return newStoneObject;
            case ResourceType.WOOD:
                var newWoodObject = woodPool.Dequeue();
                newWoodObject.SetActive(true);
                newWoodObject.transform.position = position;
                return newWoodObject;
            default:
                return null;
        }
    }

    public bool HasResource(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.GOLD:
                return goldPool.Count > 0;
            case ResourceType.STONE:
                return stonePool.Count > 0;
            case ResourceType.WOOD:
                return woodPool.Count > 0;
            default:
                return false;
        }   
        
    }

    public void ReturnResource(ResourceType type, GameObject returnedObject)
    {
        StartCoroutine(StartReturning(type, returnedObject));
    }

    IEnumerator StartReturning(ResourceType type, GameObject returnedObject)
    {
        yield return new WaitForSeconds(0.3f);
        returnedObject.SetActive(false);

        switch (type)
        {
            case ResourceType.GOLD:
                goldPool.Enqueue(returnedObject);
                break;
            case ResourceType.STONE:
                stonePool.Enqueue(returnedObject);
                break;
            case ResourceType.WOOD:
                woodPool.Enqueue(returnedObject);
                break;
        }
    }
}
