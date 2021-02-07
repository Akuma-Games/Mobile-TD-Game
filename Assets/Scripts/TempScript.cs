using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    public Transform tpPos;
    public Transform ogrePos;

    public void MoveCloserToTower() {
        ogrePos.position = tpPos.position;
    }
}
