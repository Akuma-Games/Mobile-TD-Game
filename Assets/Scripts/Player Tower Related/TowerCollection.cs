using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TowerCollection", menuName = "ScriptableObjects/TowerCollection")]
//ChangeLog 3-14-2021 changed towercost from an int to a vector3Int
public class TowerCollection : ScriptableObject
{
    [Serializable]
    public class TowerPrefab
    {
        public GameObject prefab;
        public TowerType towerType;
        public Vector3Int towerCost;

        public Vector3Int GetCost() {
            return towerCost;
        }
    }

    private Dictionary<TowerType, GameObject> prefabDict;
    private Dictionary<TowerType, Vector3Int> costDict;
    public TowerPrefab[] towerPrefabs;

    public GameObject this[TowerType type] {
        get {
            return prefabDict[type];
        }
    }
    
    public Vector3Int GetTowerCost(TowerType type) {
        return costDict[type];
    }

    public void Initialize() {
        Init();
    }

    private void Init() {
        if (prefabDict != null)
            return;
        prefabDict = new Dictionary<TowerType, GameObject>();
        costDict = new Dictionary<TowerType, Vector3Int>();
        foreach (var tower in towerPrefabs) {
            prefabDict[tower.towerType] = tower.prefab;
            costDict[tower.towerType] = tower.towerCost;
        }
    }
}
