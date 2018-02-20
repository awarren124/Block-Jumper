﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

    public Animator anim;
    int selectedWorld;

    public void WorldSelected(int worldNum){
        anim.SetTrigger("World" + worldNum + "Pressed");
        selectedWorld = worldNum;
        anim.SetFloat("SpeedMult", 1);
        anim.SetBool("Reversed", false);
    }

    public void LevelSelected(int levelNum){
        GameManager.LoadLevel((selectedWorld - 1) * 10 + levelNum);
    }

    public void BackButtonPressed(){
        anim.SetFloat("SpeedMult", -1);
        anim.SetBool("Reversed", true);
        anim.SetTrigger("Back");
    }
}
