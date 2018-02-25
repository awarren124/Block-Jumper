using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMessenger : MonoBehaviour {

    public void AnimationCallback(){
        GameManager.instance.mainUI.LevelSelectShowCallback();
    }

}