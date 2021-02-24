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
    }

    private Dictionary<TowerType, GameObject> dict;
    public TowerPrefab[] towerPrefabs;

    public GameObject this[TowerType type] {
        get {
            Init();
            return dict[type];
        }
    }

    private void Init() {
        if (dict != null)
            return;
        dict = new Dictionary<TowerType, GameObject>();
        foreach (var tower in towerPrefabs) {
            dict[tower.towerType] = tower.prefab;
        }
    }
}
