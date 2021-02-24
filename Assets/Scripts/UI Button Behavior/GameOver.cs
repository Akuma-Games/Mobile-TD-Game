using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayAgain() {
        SceneManager.LoadScene("GameplayScene");
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
        Time.timeScale = 1;
    }
}
