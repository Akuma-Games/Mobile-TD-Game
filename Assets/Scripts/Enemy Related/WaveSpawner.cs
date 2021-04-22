using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform spawnPosition;

    [SerializeField] Wave currentWave;
    [SerializeField] AudioSource spawnSound;

    int waveIndex = 0;

    private void Start() {
        currentWave = waves[waveIndex];
    }

    public void StartWave() {
        FindObjectOfType<GameManager>().CurrentlyInAWave(true);
        StartCoroutine(SpawnWave(currentWave));
    }

    IEnumerator SpawnWave(Wave wave) {
        int groupIndex = 0;

        while (wave.GetNumberOfGroups() - groupIndex > 0) {

            GameObject enemyToSpawn = wave.GetGroupAtIndex(groupIndex).EnemyPrefab;
            int enemySpawned = 0;


            while (wave.GetGroupAtIndex(groupIndex).EnemyCount - enemySpawned > 0) {
                spawnSound.Play();
                GameObject enemy = Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
                enemy.name = groupIndex + " " + enemySpawned;
                enemySpawned++;
                yield return new WaitForSeconds(wave.GetGroupAtIndex(groupIndex).EnemyRate);
            }

            yield return new WaitForSeconds(wave.GetGroupAtIndex(groupIndex).CooldownForNextGroup);
            groupIndex++;
            
        }

        Debug.Log("WAVE DONE SPAWNING");
        FindObjectOfType<GameManager>().WaveDoneSpawning = true;

        waveIndex++;

        try {
            currentWave = waves[waveIndex];
        }
        catch (Exception e) {
            // spawned all waves
            
        }
    }

}
