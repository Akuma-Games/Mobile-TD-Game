using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool loadingGameFromMenu;
    public void NewGame() {
        SceneManager.LoadScene("GameplayScene");
    }

    public void LoadGame() {
        loadingGameFromMenu = true;
        SceneManager.LoadScene("GameplayScene");
        //Debug.Log("Load Game not implemented yet!");
    }

    public void Exit() {
        Application.Quit();
    }
}
