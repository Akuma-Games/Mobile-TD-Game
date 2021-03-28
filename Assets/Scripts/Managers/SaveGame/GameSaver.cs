using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public TowerDataSO towerData;
    [SerializeField] private TowerCollection towerCollection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", GameManager.Instance.score);
        PlayerPrefs.SetInt("Wave", GameManager.Instance.wave);
        PlayerPrefs.SetInt("Gold", ResourceManager.Instance.gold);
        PlayerPrefs.SetInt("Stone", ResourceManager.Instance.stone);
        PlayerPrefs.SetInt("Wood", ResourceManager.Instance.wood);

        // Tower
        BuildableTile[] allTiles = FindObjectsOfType<BuildableTile>();
        foreach (BuildableTile tile in allTiles)
        {
            towerData.existingTowers[tile.index] = tile.currentTower;
        }

        for (int i = 0; i < towerData.existingTowers.Length; i++)
        {
            string slotName = "Tile" + i;
            PlayerPrefs.SetInt(slotName, (int)towerData.existingTowers[i]);
        }

        PlayerPrefs.Save();
    }

    public void Load()
    {
        GameManager.Instance.score = PlayerPrefs.GetInt("Score");
        GameManager.Instance.wave = PlayerPrefs.GetInt("Wave");
        ResourceManager.Instance.gold = PlayerPrefs.GetInt("Gold");
        ResourceManager.Instance.stone = PlayerPrefs.GetInt("Stone");
        ResourceManager.Instance.wood = PlayerPrefs.GetInt("Wood");

        ResourceManager.Instance.goldAmount.text = ResourceManager.Instance.gold.ToString();
        ResourceManager.Instance.stoneAmount.text = ResourceManager.Instance.stone.ToString();
        ResourceManager.Instance.woodAmount.text = ResourceManager.Instance.wood.ToString();

        // Tower
        BuildableTile[] allTiles = FindObjectsOfType<BuildableTile>();
        foreach (BuildableTile tile in allTiles)
        {
            string slotName = "Tile" + tile.index;
            tile.currentTower = (TowerType)PlayerPrefs.GetInt(slotName);

            if (tile.currentTower != TowerType.NONE)
            {
                Vector3 towerOffset = new Vector3(0, 1.1f, 0) + tile.transform.position;
                Instantiate(towerCollection[tile.currentTower], towerOffset, Quaternion.identity);
            }
        }
    }
}
