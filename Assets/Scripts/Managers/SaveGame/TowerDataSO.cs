using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "ScriptableObjects/TowerData")]
public class TowerDataSO : ScriptableObject
{
    public TowerType[] existingTowers = new TowerType[55];
}
