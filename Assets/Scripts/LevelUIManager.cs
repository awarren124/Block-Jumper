using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour {

    public GameObject defaultMenu;
    public GameObject pauseMenu;
    public GameObject powerRect;
    
    private void Awake() {
        if(GameManager.instance.levelUI != this) {
            Destroy(gameObject);
        }
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
        defaultMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameObject.SetActive(false);
        GameManager.LoadLevel(0);
    }

    public void UpdatePower(float deltaTouch) {
        RectTransform rect = powerRect.GetComponent<RectTransform>();
        rect.offsetMax = new Vector2(rect.offsetMax.x, -deltaTouch * Screen.height);
    }

    public void ResetPower() {
        powerRect.GetComponent<RectTransform>().sizeDelta = new Vector2(powerRect.GetComponent<RectTransform>().sizeDelta.x, Screen.height);
    }
}
