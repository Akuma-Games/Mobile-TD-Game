using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int enemiesKilled = 0;
    public int wavesCompleted = 0;
    
    int enemyTarget;
    int waveTarget;

    public int questLevel_Enemy = 1;
    public int questLevel_Wave = 1;

    [SerializeField] Text enemyAmountText;
    [SerializeField] Text waveAmountText;

    private AudioSource questCompleteSound;

    // Start is called before the first frame update
    void Start()
    {
        questCompleteSound = GetComponent<AudioSource>();

        enemyTarget = questLevel_Enemy * 10;
        waveTarget = questLevel_Wave;

        UpdateEnemyAmountText();
        UpdateWaveAmountText();
    }

    public void KillEnemy()
    {
        enemiesKilled++;
        if (enemiesKilled >= enemyTarget)
        {
            ResourceManager.Instance.CollectResource((ResourceType)Random.Range(0, 3), questLevel_Enemy * 100);
            questLevel_Enemy++;
            
            enemiesKilled = 0;
            questCompleteSound.Play();
            UpdateEnemyAmountText();
        }
        else
        {
            UpdateEnemyAmountText();
        }
    }

    public void CompleteWave()
    {
        wavesCompleted++;
        if (wavesCompleted >= waveTarget)
        {
            ResourceManager.Instance.CollectResource((ResourceType)Random.Range(0, 3), questLevel_Wave * 50);
            questLevel_Wave++;
            
            wavesCompleted = 0;
            questCompleteSound.Play();
            UpdateWaveAmountText();
        }
        else
        {
            UpdateWaveAmountText();
        }
    }

    public void UpdateEnemyAmountText()
    {
        enemyTarget = questLevel_Enemy * 10;
        enemyAmountText.text = enemiesKilled + " / " + enemyTarget;
    }

    public void UpdateWaveAmountText()
    {
        waveTarget = questLevel_Wave;
        waveAmountText.text = wavesCompleted + " / " + waveTarget;
    }
}
