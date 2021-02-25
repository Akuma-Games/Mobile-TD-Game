using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TowerCollection", menuName = "ScriptableObjects/TowerCollection")]

public class TowerCollection : ScriptableObject
{
    [Serializable]
    public class TowerPrefab
    {
        public GameObject prefab;
        public TowerType towerType;
        public int towerCost;

        public int GetCost() {
            return towerCost;
        }
    }

    private Dictionary<TowerType, GameObject> prefabDict;
    private Dictionary<TowerType, int> costDict;
    public TowerPrefab[] towerPrefabs;

    public GameObject this[TowerType type] {
        get {
            return prefabDict[type];
        }
    }
    
    public int GetTowerCost(TowerType type) {
        return costDict[type];
    }

    public void Initialize() {
        Init();
    }

    private void Init() {
        if (prefabDict != null)
            return;
        prefabDict = new Dictionary<TowerType, GameObject>();
        costDict = new Dictionary<TowerType, int>();
        foreach (var tower in towerPrefabs) {
            prefabDict[tower.towerType] = tower.prefab;
            costDict[tower.towerType] = tower.towerCost;
        }
    }
}
