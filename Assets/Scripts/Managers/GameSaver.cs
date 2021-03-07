using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", GameManager.Instance.score);
        PlayerPrefs.SetInt("Wave", GameManager.Instance.wave);
        PlayerPrefs.SetInt("Gold", ResourceManager.Instance.gold);
        PlayerPrefs.SetInt("Stone", ResourceManager.Instance.stone);
        PlayerPrefs.SetInt("Wood", ResourceManager.Instance.wood);

        // Tower
    }

    public void Load()
    {
        GameManager.Instance.score = PlayerPrefs.GetInt("Score");
        GameManager.Instance.wave = PlayerPrefs.GetInt("Wave");
        ResourceManager.Instance.gold = PlayerPrefs.GetInt("Gold");
        ResourceManager.Instance.stone = PlayerPrefs.GetInt("Stone");
        ResourceManager.Instance.wood = PlayerPrefs.GetInt("Wood");

        ResourceManager.Instance.goldAmount.text = ResourceManager.Instance.gold.ToString();
        ResourceManager.Instance.stoneAmount.text = ResourceManager.Instance.stone.ToString();
        ResourceManager.Instance.woodAmount.text = ResourceManager.Instance.wood.ToString();

        // Tower
    }
}
