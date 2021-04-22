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
        if (MainMenu.loadingGameFromMenu)
        {
            Load();
            MainMenu.loadingGameFromMenu = false;
        }
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

        PlayerPrefs.SetInt("waveQuest", FindObjectOfType<QuestManager>().questLevel_Wave);
        PlayerPrefs.SetInt("enemyQuest", FindObjectOfType<QuestManager>().questLevel_Enemy);
        PlayerPrefs.SetInt("wavesCompleted", FindObjectOfType<QuestManager>().wavesCompleted);
        PlayerPrefs.SetInt("enemiesKilled", FindObjectOfType<QuestManager>().enemiesKilled);

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

        FindObjectOfType<QuestManager>().questLevel_Wave = PlayerPrefs.GetInt("waveQuest");
        FindObjectOfType<QuestManager>().questLevel_Enemy = PlayerPrefs.GetInt("enemyQuest");
        FindObjectOfType<QuestManager>().wavesCompleted = PlayerPrefs.GetInt("wavesCompleted");
        FindObjectOfType<QuestManager>().enemiesKilled = PlayerPrefs.GetInt("enemiesKilled");
        FindObjectOfType<QuestManager>().UpdateEnemyAmountText();
        FindObjectOfType<QuestManager>().UpdateWaveAmountText();

        // Tower
        BuildableTile[] allTiles = FindObjectsOfType<BuildableTile>();

        foreach(Tower tower in FindObjectsOfType<Tower>())
        {
            Destroy(tower.gameObject);
        }

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
