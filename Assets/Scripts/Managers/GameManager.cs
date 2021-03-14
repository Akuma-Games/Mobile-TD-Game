using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum TowerType
{
    ARCHER,
    WARRIOR
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text[] costText;
    [SerializeField] GameObject gameOverScreen;

    public TowerType CurrentTowerBuilding { get; set; }

    [SerializeField] TowerCollection towerCollection;

    [SerializeField] GameObject startWaveButton;

    private int enemiesInTheScene = 0;
    public int EnemiesInTheScene { get; set; }

    public int score = 0;
    public int wave = 1;

    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null){
                if (FindObjectOfType<GameManager>() != null)
                {
                    m_Instance = FindObjectOfType<GameManager>();
                }
                else
                {
                    GameObject gm = new GameObject("GameManager");
                    m_Instance = gm.AddComponent<GameManager>();
                } 
            }
            return m_Instance;
        }
    }

    void Start()
    {

        towerCollection.Initialize();

        EnemiesInTheScene = 0;

        StartCoroutine(GenerateWood());
        StartCoroutine(GenerateStone());
        SetCostText(CurrentTowerBuilding);
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void GameOver()
    {
        scoreText.text = score.ToString();
        waveText.text = wave.ToString();
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public GameObject GetTowerPrefab(TowerType towerType) {
        return towerCollection[towerType];
    }

    public void SetCostText(TowerType tType)
    {
        int goldCost = towerCollection.GetTowerCost(CurrentTowerBuilding).x;
        int stoneCost = towerCollection.GetTowerCost(CurrentTowerBuilding).y;
        int woodCost = towerCollection.GetTowerCost(CurrentTowerBuilding).z;
        costText[0].SetText(goldCost + "\t" + stoneCost + "\t" + woodCost);
    }

    public void BuildTower(Vector3 pos, int tileIndex) {
        int goldCost= towerCollection.GetTowerCost(CurrentTowerBuilding).x;
        int stoneCost = towerCollection.GetTowerCost(CurrentTowerBuilding).y;
        int woodCost = towerCollection.GetTowerCost(CurrentTowerBuilding).z; 
        if (ResourceManager.Instance.gold < goldCost) {
            Debug.Log("Not enough money");
            return;
        }
        if ( ResourceManager.Instance.stone < stoneCost)
        {
            return;
        }
        if(ResourceManager.Instance.wood < woodCost)
        {
            return;
        }
        ResourceManager.Instance.gold -= goldCost;
        ResourceManager.Instance.stone -= stoneCost;
        ResourceManager.Instance.wood -= woodCost;
        ResourceManager.Instance.goldAmount.text = ResourceManager.Instance.gold.ToString();
        ResourceManager.Instance.stoneAmount.text = ResourceManager.Instance.stone.ToString();
        ResourceManager.Instance.woodAmount.text = ResourceManager.Instance.wood.ToString();
        Vector3 towerOffset = new Vector3(0, 1.1f, 0);
        pos += towerOffset;
        Instantiate(towerCollection[CurrentTowerBuilding], pos, Quaternion.identity);
    }

    public void EnemyDie() {
        EnemiesInTheScene--;

        if (EnemiesInTheScene <= 0) {
            // wave complete
            startWaveButton.SetActive(true);
        }
    }

    IEnumerator GenerateStone()
    {
        yield return new WaitForSeconds(6.0f);
        ResourceManager.Instance.GetResource(ResourceType.STONE, new Vector3(Random.Range(0.2f, 2.2f), 2.5f, Random.Range(-10.0f, -12.5f)));
        StartCoroutine(GenerateStone());
    }

    IEnumerator GenerateWood()
    {
        yield return new WaitForSeconds(5.0f);
        ResourceManager.Instance.CollectResource(ResourceType.WOOD, Random.Range(6, 12));
        StartCoroutine(GenerateWood());
    }
}
