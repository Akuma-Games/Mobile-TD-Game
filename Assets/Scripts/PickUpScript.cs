using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    /*void OnMouseDown()
    {
        GameManager.Instance.collectGold(10);
        Destroy(gameObject);
    }*/

    Ray ray;
    RaycastHit hit;
     
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameManager.Instance.collectGold(10);
                Destroy(gameObject);
            }
        }
    }
}
