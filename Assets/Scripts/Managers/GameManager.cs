using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum TowerType
{
    ARCHER,
    TANK,
    HEALER,
    NONE
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public QuestManager qManager;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] GameObject gameOverScreen;

    public TowerType CurrentTowerBuilding { get; set; }

    [SerializeField] TowerCollection towerCollection;

    [SerializeField] TMP_Text costText;
    [SerializeField] GameObject startWaveButton;

    private int enemiesInTheScene = 0;
    public int EnemiesInTheScene { get; set; }

    public int score = 0;
    public int wave = 1;

    private bool isPaused = false;

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

    /*public void BuildTower(Vector3 pos, int tileIndex) {
        int towerCost = towerCollection.GetTowerCost(CurrentTowerBuilding);

        if (ResourceManager.Instance.gold < towerCost) {
            Debug.Log("Not enough money");
            return;
        }

        ResourceManager.Instance.gold -= towerCost;
        ResourceManager.Instance.goldAmount.text = ResourceManager.Instance.gold.ToString();
        Vector3 towerOffset = new Vector3(0, 1.1f, 0);
        pos += towerOffset;
        Instantiate(towerCollection[CurrentTowerBuilding], pos, Quaternion.identity);

        FindObjectOfType<GameSaver>().towerData.existingTowers[tileIndex] = TowerType.ARCHER;
    }*/
    public void SetCostText()
    {
        int goldCost = towerCollection.GetTowerCost(CurrentTowerBuilding).x;
        int stoneCost = towerCollection.GetTowerCost(CurrentTowerBuilding).y;
        int woodCost = towerCollection.GetTowerCost(CurrentTowerBuilding).z;
        costText.SetText(goldCost + "\t" + stoneCost + "\t" + woodCost);
    }
    public void BuildTower(BuildableTile tile) {
        if (tile.currentTower != TowerType.NONE)
            return;
    
        Vector3Int towerCost = towerCollection.GetTowerCost(CurrentTowerBuilding);

        if (ResourceManager.Instance.gold < towerCost.x) {
            Debug.Log("Not enough gold");
            return;
        }
        if(ResourceManager.Instance.wood < towerCost.y)
        {
            return;
        }

        if(ResourceManager.Instance.stone < towerCost.z)
        {
            return;
        }
        ResourceManager.Instance.gold -= towerCost.x;
        ResourceManager.Instance.wood -= towerCost.y;
        ResourceManager.Instance.stone -= towerCost.z;
        ResourceManager.Instance.goldAmount.text = ResourceManager.Instance.gold.ToString();
        ResourceManager.Instance.woodAmount.text = ResourceManager.Instance.wood.ToString();
        ResourceManager.Instance.stoneAmount.text = ResourceManager.Instance.stone.ToString();
        Vector3 towerOffset = new Vector3(0, 1.1f, 0) + tile.transform.position;
        //insert some kind of validation for tower types here maybe?
        Instantiate(towerCollection[CurrentTowerBuilding], towerOffset, Quaternion.identity);
        tile.currentTower = CurrentTowerBuilding;
    }

    public void EnemyDie() {
        EnemiesInTheScene--;
        qManager.KillEnemy();
        if (EnemiesInTheScene <= 0) {
            // wave complete
            startWaveButton.SetActive(true);
            qManager.CompleteWave();
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

    public void Pause_Resume()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }
}
