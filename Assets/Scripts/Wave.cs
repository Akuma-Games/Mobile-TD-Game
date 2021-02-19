using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    [SerializeField] WaveBlueprint[] enemyWave;
}

[System.Serializable]
public class WaveBlueprint
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int enemyCount;
    [SerializeField] float enemyRate;
    [SerializeField] float cooldownForNextWave;

    public int EnemyCount { get { return enemyCount; } }
    public float EnemyRate { get { return enemyRate; } }
}