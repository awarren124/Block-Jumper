using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    void Awake(){
        if(instance == null){
            instance = this;

        }else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }
    [HideInInspector]
    public GameObject player;
    public LevelUIManager levelUI;
    public static int currentLevel;
	
    public static void LoadLevel(int levelNum){
        Time.timeScale = 1f;
        currentLevel = levelNum;
        SceneManager.LoadScene(levelNum);
    }

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        print("scene " + scene + " loaded");
        if(scene.buildIndex != 0){
            instance.levelUI.gameObject.SetActive(true);
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
    }

    public void Resume() {
        Time.timeScale = 1f;
    }
}
