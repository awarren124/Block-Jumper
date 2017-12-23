using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour {

    public GameObject defaultMenu;
    public GameObject pauseMenu;
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    public void PauseButtonPressed() {
        GameManager.instance.Pause();
        defaultMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ResumeButtonPressed() {
        GameManager.instance.Resume();
        defaultMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void MainMenuButtonPressed() {
        GameManager.LoadLevel(0);
        gameObject.SetActive(false);
    }
}
