using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform spawnPosition;

    [SerializeField] Wave currentWave;

    int waveIndex = 0;

    private void Start() {
        currentWave = waves[waveIndex];
        StartCoroutine(SpawnWave(currentWave));
    }

    private void Update() {
        
    }

    IEnumerator SpawnWave(Wave wave) {
        int groupIndex = 0;

        while (wave.GetNumberOfGroups() - groupIndex > 0) {

            GameObject enemyToSpawn = wave.GetGroupAtIndex(groupIndex).EnemyPrefab;
            int enemySpawned = 0;


            while (wave.GetGroupAtIndex(groupIndex).EnemyCount - enemySpawned > 0) {
                Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
                enemySpawned++;
                yield return new WaitForSeconds(wave.GetGroupAtIndex(groupIndex).EnemyRate);
            }

            yield return new WaitForSeconds(wave.GetGroupAtIndex(groupIndex).CooldownForNextGroup);
            groupIndex++;
            
        }
    }

}
