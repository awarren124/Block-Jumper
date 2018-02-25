﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainUIManager : MonoBehaviour {

    public Animator anim;
    int selectedWorld;

    public void Start(){
        GameManager.instance.mainUI = this;
        for(int numOfWorld = 0; numOfWorld < GameManager.numOfWorlds; numOfWorld++){
            if(PlayerPrefsX.GetBoolArray("World " + numOfWorld + "Levels Unlocked").Length == 0){
                bool[] isUnlocked = new bool[GameManager.levelsPerWorld];
                isUnlocked[0] = true;
                PlayerPrefsX.SetBoolArray("World " + numOfWorld + "Levels Unlocked", isUnlocked);
                for(int i = 0; i < GameManager.levelsPerWorld; i++) {
                    GameManager.unlockedLevels[numOfWorld, i] = isUnlocked[i];

                }
            }else{
                bool[] isUnlocked = PlayerPrefsX.GetBoolArray("World " + numOfWorld + "Levels Unlocked");
                for(int i = 0; i < GameManager.levelsPerWorld; i++) {
                    GameManager.unlockedLevels[numOfWorld, i] = isUnlocked[i];
                }
                    
            }
        }
        print(PlayerPrefsX.GetBoolArray("World " + 3 + "Levels Unlocked").Length);
    }

    public void LevelSelectShowCallback(){
        for(int worldNum = 0; worldNum < GameManager.numOfWorlds; worldNum++) {
            for(int levelNum = 0; levelNum < GameManager.levelsPerWorld; levelNum++){
                GameObject buttGO = GameObject.Find("Level " + (levelNum + 1) + " Button");
                Button levelButton = buttGO.GetComponent<Button>();
                levelButton.interactable = GameManager.unlockedLevels[worldNum, levelNum];
            }
        }
    }

    public void WorldSelected(int worldNum){
        anim.SetTrigger("World" + worldNum + "Pressed");
        selectedWorld = worldNum;
        anim.SetFloat("SpeedMult", 1);
        anim.SetBool("Reversed", false);
    }

    public void LevelSelected(int levelNum){
        GameManager.LoadLevel(selectedWorld, levelNum);
    }

    public void BackButtonPressed(){
        anim.SetFloat("SpeedMult", -1);
        anim.SetBool("Reversed", true);
        anim.SetTrigger("Back");
    }
        
}
