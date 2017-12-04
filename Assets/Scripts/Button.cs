using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public ButtonAction[] actions;
    public ButtonType type;
    public ButtonState state = ButtonState.Default;

    

    public void Push(){
        state = ButtonState.Pressed;
        transform.localScale -= Vector3.up * 0.2f;
        transform.position -= Vector3.up * 0.09f;
        foreach(var action in actions) {
            switch(action.type) {
                case ButtonActionType.PlatformMove:
                    action.target.GetComponent<MovingPlatform>().shouldMove = true;
                    break;
                case ButtonActionType.PlatformStop:
                    action.target.GetComponent<MovingPlatform>().shouldMove = false;
                    break;
                case ButtonActionType.PlatformShow:
                    StartCoroutine(action.target.GetComponent<HiddenPlatform>().Show());
                    break;
                case ButtonActionType.PlatformHide:
                    StartCoroutine(action.target.GetComponent<HiddenPlatform>().Hide());
                    break;
                case ButtonActionType.PlatformDissapear:
                    StartCoroutine(action.target.GetComponent<DissapearingPlatform>().Dissapear());
                    break;
                default:
                    break;
            }
        }
    }

    public void Release(){
        if(type == ButtonType.Regular){
            transform.localScale += Vector3.up * 0.2f;
            transform.position += Vector3.up * 0.09f;
            state = ButtonState.Default;
            foreach(var action in actions) {
                switch(action.type) {
                    case ButtonActionType.PlatformMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = false;
                        break;
                    case ButtonActionType.PlatformStop:
                        action.target.GetComponent<MovingPlatform>().shouldMove = true;
                        break;
                    case ButtonActionType.PlatformShow:
                        StartCoroutine(action.target.GetComponent<HiddenPlatform>().Hide());
                        break;
                    case ButtonActionType.PlatformHide:
                        StartCoroutine(action.target.GetComponent<HiddenPlatform>().Show());
                        break;
                    default:
                        break;
                }
            }
        }
    }

}

public enum ButtonType {
    Regular,
    Sticky
}

public enum ButtonState {
    Pressed,
    Default
}