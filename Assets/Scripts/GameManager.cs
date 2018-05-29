using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    [HideInInspector]
    public GameObject player;
    public LevelUIManager levelUI;
    public MainUIManager mainUI;
    public int currentWorld;
    public int currentLevel;
    [HideInInspector]
    public bool isPaused = false;
    public static int numOfWorlds = 3;
    public static int levelsPerWorld = 10;
    public static bool[,] unlockedLevels;

    void Awake(){
        if(instance == null){
            instance = this;

        }else if(instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        unlockedLevels = new bool[numOfWorlds, levelsPerWorld];

    }

    public void ResetSavedLevels() {
        bool[] tmp = new bool[10];
        tmp[0] = true;
        PlayerPrefsX.SetBoolArray("World " + 1 + " Levels Unlocked", tmp);
        PlayerPrefsX.SetBoolArray("World " + 2 + " Levels Unlocked", tmp);
        PlayerPrefsX.SetBoolArray("World " + 3  + " Levels Unlocked", tmp);

    }

    public static void LoadLevel(int worldlNum, int levelNum){
        Time.timeScale = 1f;
        if(levelNum != 0 && worldlNum != 0) {
            instance.currentLevel = levelNum;
            instance.currentWorld = worldlNum;
        }
        //print("Loading scene with index " + ((worldlNum - 1) * 10 + levelNum + 1));
        SceneManager.LoadScene((Mathf.Max(((worldlNum - 1) * 10 + levelNum), 0)));
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
        player.GetComponent<Control>().oldCharacterVelocity = player.GetComponent<Rigidbody>().velocity;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        isPaused = true;
    }

    public void Resume() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.GetComponent<Rigidbody>().velocity = player.GetComponent<Control>().oldCharacterVelocity;
        isPaused = false;

    }

    public void Die(){
        LoadLevel(currentWorld, currentLevel);
    }

    public void LevelFinished(){
        StartCoroutine(instance.levelUI.ShowLevelCompleteMenu());
        if(currentLevel < levelsPerWorld) {
            unlockedLevels[currentWorld - 1, currentLevel] = true;
            bool[] unlockedForThisWorld = PlayerPrefsX.GetBoolArray("World " + instance.currentWorld + " Levels Unlocked");
            print(unlockedForThisWorld.Length);
            print(currentLevel);
            unlockedForThisWorld[currentLevel] = true;
            PlayerPrefsX.SetBoolArray("World " + instance.currentWorld + " Levels Unlocked", unlockedForThisWorld);
        }
    }

    public void LoadNextLevel(){
        print("currentWorld: " + currentWorld);
        print("currentLevel: " + currentLevel);
        if(currentLevel < levelsPerWorld){
            LoadLevel(currentWorld, currentLevel + 1);
        }else if(currentWorld < numOfWorlds){
            LoadLevel(currentWorld + 1, 1);
        }else{
            LoadLevel(0,0);
        }
    }
}
