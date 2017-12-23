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
    public GameObject levelUI;
    public static int currentLevel;
	
    public static void LoadLevel(int levelNum){
        Time.timeScale = 1f;
        currentLevel = levelNum;
        SceneManager.LoadScene(levelNum);
        instance.levelUI.SetActive(true);
    }

    public void Pause() {
        Time.timeScale = 0f;
    }

    public void Resume() {
        Time.timeScale = 1f;
    }
}
