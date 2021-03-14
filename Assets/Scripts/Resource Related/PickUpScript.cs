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

    [SerializeField] AudioSource pickup_sound;

    private void Start()
    {
        pickup_sound = GetComponent<AudioSource>();
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        pickup_sound.volume = GameSettings.soundVolume;

        if(Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
            {
                //pickup_sound.Play();

                switch (this.tag)
                {
                    case "Gold":
                        ResourceManager.Instance.CollectResource(ResourceType.GOLD, 10);
                        ResourceManager.Instance.ReturnResource(ResourceType.GOLD, this.gameObject);
                        break;
                    case "Stone":
                        ResourceManager.Instance.CollectResource(ResourceType.STONE, Random.Range(8, 15));
                        ResourceManager.Instance.ReturnResource(ResourceType.STONE, this.gameObject);
                        break;
                    case "Wood":
                        ResourceManager.Instance.CollectResource(ResourceType.WOOD, Random.Range(8, 15));
                        ResourceManager.Instance.ReturnResource(ResourceType.WOOD, this.gameObject);
                        break;
                }
            }
        }
    }
}
