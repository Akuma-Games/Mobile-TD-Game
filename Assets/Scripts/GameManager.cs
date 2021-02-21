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
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] GameObject gameOverScreen;

    private TowerType currentTowerBuilding;
    public TowerType CurrentTowerBuilding { get; set; }

    [SerializeField] TowerCollection towerCollection;

    private int score = 1000;
    private int wave = 3;
    private int gold = 0;
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

    public void collectGold(int amount)
    {
        gold += 10;
        goldAmount.text = gold.ToString();
    }

    public void Replay()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void GameOver()
    {
        scoreText.text = score.ToString();
        waveText.text = wave.ToString();
        gameOverScreen.SetActive(true);
    }
}
