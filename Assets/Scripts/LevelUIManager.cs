using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour {

    public GameObject defaultMenu;
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;

    public GameObject powerRect;

    public Gradient powerGradient;
    public Text levelText;

    int powerRectDisplacement = 50;

    Animator anim;
    
    private void Awake() {
        if(GameManager.instance.levelUI != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    private void Start(){
        anim = GetComponent<Animator>();
    }

    public void PauseButtonPressed() {
        GameManager.instance.Pause();
        defaultMenu.SetActive(false);
        pauseMenu.SetActive(true);
        anim.SetBool("Paused", true);
    }

    public void ResumeButtonPressed() {
        GameManager.instance.Resume();
        defaultMenu.SetActive(true);
        pauseMenu.SetActive(false);
        anim.SetBool("Paused", false);
    }

    public void MainMenuButtonPressed() {
        defaultMenu.SetActive(true);
        pauseMenu.SetActive(false);
        gameObject.SetActive(false);
        GameManager.instance.isPaused = false;
        GameManager.LoadLevel(0, 0);
    }

    public void UpdatePower(float deltaTouch) {
        RectTransform rect = powerRect.GetComponent<RectTransform>();
        rect.offsetMax = new Vector2(rect.offsetMax.x, -deltaTouch * Screen.height);
        powerRect.GetComponent<Image>().color = powerGradient.Evaluate(deltaTouch);
    }

    public void ResetPower() {
        powerRect.GetComponent<RectTransform>().sizeDelta = new Vector2(powerRect.GetComponent<RectTransform>().sizeDelta.x, Screen.height);
    }

    public void ShowLevelCompleteMenu(){
        levelText.text = GameManager.instance.currentWorld + " - " + GameManager.instance.currentLevel;
        anim.SetBool("LevelComplete", true);
    }

    public void NextLevelButtonPressed(){
        anim.SetBool("LevelComplete", false);
        GameManager.instance.LoadNextLevel();
    }
}
