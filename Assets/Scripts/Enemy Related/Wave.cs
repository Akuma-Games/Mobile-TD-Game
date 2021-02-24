using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    [SerializeField] WaveBlueprint[] enemyGroups;

    [SerializeField] int currentGroupIndex = 0;

    public WaveBlueprint GetGroupAtIndex(int i) {
        return enemyGroups[i];
    }

    public int GetNumberOfGroups() {
        return enemyGroups.Length;
    }

    public void FinishedSpawningGroup() {
        currentGroupIndex++;
    }
}

[System.Serializable]
public class WaveBlueprint
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount;
    [SerializeField] float enemyRate;
    [SerializeField] float cooldownForNextGroup;

    public GameObject EnemyPrefab {  get { return enemyPrefab; } }
    public int EnemyCount { get { return enemyCount; } }
    public float EnemyRate { get { return enemyRate; } }

    public float CooldownForNextGroup {  get { return cooldownForNextGroup; } }
}