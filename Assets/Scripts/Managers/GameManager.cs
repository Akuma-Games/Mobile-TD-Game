using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum TowerType
{
    ARCHER
}

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    [SerializeField] TMP_Text goldAmount;
    [SerializeField] TMP_Text stoneAmount;
    [SerializeField] TMP_Text woodAmount;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] GameObject gameOverScreen;

    public TowerType CurrentTowerBuilding { get; set; }

    [SerializeField] TowerCollection towerCollection;

    [SerializeField] GameObject startWaveButton;

    private int enemiesInTheScene = 0;
    public int EnemiesInTheScene { get; set; }

    private int score = 0;
    private int wave = 1;
    private int gold = 90;
    private int stone = 0;
    private int wood = 0;

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
        /*AudioSource[] sounds = FindObjectsOfType<AudioSource>();
        foreach(AudioSource sound in sounds)
        {
            sound.volume = GameSettings.soundVolume;
        }
        GetComponent<AudioSource>().volume = GameSettings.musicVolume;
        */
        towerCollection.Initialize();
        goldAmount.text = gold.ToString();

        EnemiesInTheScene = 0;
    }

    public void collectGold(int amount)
    {
        gold += amount;
        goldAmount.text = gold.ToString();
    }

    public void collectStone(int amount)
    {
        stone += amount;
        stoneAmount.text = stone.ToString();
    }
    public void collectWood(int amount)
    {
        wood += amount;
        woodAmount.text = wood.ToString();
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

    public void BuildTower(Vector3 pos) {
        int towerCost = towerCollection.GetTowerCost(CurrentTowerBuilding);

        if (gold < towerCost) {
            Debug.Log("Not enough money");
            return;
        }

        gold -= towerCost;
        goldAmount.text = gold.ToString();
        Vector3 towerOffset = new Vector3(0, 1.1f, 0);
        pos += towerOffset;
        Instantiate(towerCollection[CurrentTowerBuilding], pos, Quaternion.identity);
    }

    // Temporary function
    public void UseGold()
    {
        if (gold > 0)
        {
            gold--;
            goldAmount.text = gold.ToString();
        }
    }

    public void EnemyDie() {
        EnemiesInTheScene--;

        if (EnemiesInTheScene <= 0) {
            // wave complete
            startWaveButton.SetActive(true);
        }
    }
}
