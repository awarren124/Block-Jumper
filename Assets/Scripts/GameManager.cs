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

    }
    [HideInInspector]
    public GameObject player;
    public static int currentLevel;
	
    public static void LoadLevel(int levelNum){
        currentLevel = levelNum;
        SceneManager.LoadScene(levelNum);
    }
}
