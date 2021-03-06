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
                GetComponent<AudioSource>().Play();
                switch (this.tag)
                {
                    case "Gold":
                        GameManager.Instance.collectGold(10);
                        break;
                    case "Stone":
                        GameManager.Instance.collectStone(Random.Range(8, 15));
                        break;
                    case "Wood":
                        GameManager.Instance.collectWood(Random.Range(8, 15));
                        break;
                }
                Destroy(this.gameObject, 0.3f);
            }
        }
    }
}
