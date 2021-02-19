using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform spawnPosition;

    int waveIndex = 0;

    private void Start() {
        
    }

}
