using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame() {
        SceneManager.LoadScene("GameplayScene");
    }

    public void LoadGame() {
        Debug.Log("Load Game not implemented yet!");
    }

    public void Exit() {
        Application.Quit();
    }
}
