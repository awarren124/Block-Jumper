using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour {

    public ButtonAction[] actions;
    public ButtonType type;
    [HideInInspector]
    public ButtonState state = ButtonState.Default;
    public Color releasedColor = new Color(0.44706f, 0.94118f, 0.38039f);
    public Color pressedColor = new Color(0.23137f, 0.48235f, 0.19607f);

    public void Start(){
        foreach(var action in actions) {
            if(action.isStrict) {
                switch(action.type) {
                    case ButtonActionType.PlatformMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = false;
                        break;
                    case ButtonActionType.PlatformRotate:
                        action.target.GetComponent<MovingPlatform>().shouldRotate = false;
                        break;
                    case ButtonActionType.PlatformAllowShrink:
                        action.target.GetComponent<ShrinkingPlatform>().shouldScale = false;
                        break;
                }

            }
        }
    }

    public void Push(){
        if(state != ButtonState.Pressed){
            state = ButtonState.Pressed;
            GetComponent<Renderer>().material.color = pressedColor;
            transform.localScale -= Vector3.up * 0.2f;
            transform.position -= Vector3.up * 0.09f;
            foreach(var action in actions) {
                print(action.target.name);
                switch(action.type) {
                    case ButtonActionType.PlatformMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = true;
                        break;
                    case ButtonActionType.PlatformStopMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = false;
                        break;
                    case ButtonActionType.PlatformRotate:
                        action.target.GetComponent<MovingPlatform>().shouldRotate = true;
                        break;
                    case ButtonActionType.PlatformStopRotate:
                        action.target.GetComponent<MovingPlatform>().shouldRotate = false;
                        break;
                    case ButtonActionType.PlatformShow:
                        if(action.target.GetComponent<HiddenPlatform>().isHidden) {
                            StartCoroutine(action.target.GetComponent<HiddenPlatform>().Show());
                        }
                        break;
                    case ButtonActionType.PlatformHide:
                        if(!action.target.GetComponent<HiddenPlatform>().isHidden) {
                            StartCoroutine(action.target.GetComponent<HiddenPlatform>().Hide());
                        }
                        break;
                    case ButtonActionType.PlatformDissapear:
                        StartCoroutine(action.target.GetComponent<DissapearingPlatform>().Dissapear());
                        break;
                    case ButtonActionType.PlatformAllowShrink:
                        action.target.GetComponent<ShrinkingPlatform>().shouldScale = true;
                        break;
                    case ButtonActionType.PlatformProhibitShrink:
                        action.target.GetComponent<ShrinkingPlatform>().shouldScale = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void Release(){
        if(type == ButtonType.Regular){
            transform.localScale += Vector3.up * 0.2f;
            transform.position += Vector3.up * 0.09f;
            state = ButtonState.Default;
            GetComponent<Renderer>().material.color = releasedColor;
            foreach(var action in actions) {
                switch(action.type) {
                    case ButtonActionType.PlatformMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = false;
                        break;
                    case ButtonActionType.PlatformStopMove:
                        action.target.GetComponent<MovingPlatform>().shouldMove = true;
                        break;
                    case ButtonActionType.PlatformRotate:
                        action.target.GetComponent<MovingPlatform>().shouldRotate = false;
                        break;
                    case ButtonActionType.PlatformStopRotate:
                        action.target.GetComponent<MovingPlatform>().shouldRotate = true;
                        break;
                    case ButtonActionType.PlatformShow:
                        if(!action.target.GetComponent<HiddenPlatform>().isHidden) {
                            StartCoroutine(action.target.GetComponent<HiddenPlatform>().Hide());
                        }
                        break;
                    case ButtonActionType.PlatformHide:
                        if(action.target.GetComponent<HiddenPlatform>().isHidden) {
                            StartCoroutine(action.target.GetComponent<HiddenPlatform>().Show());
                        }
                        break;
                    case ButtonActionType.PlatformAllowShrink:
                        action.target.GetComponent<ShrinkingPlatform>().shouldScale = false;
                        break;
                    case ButtonActionType.PlatformProhibitShrink:
                        action.target.GetComponent<ShrinkingPlatform>().shouldScale = true;
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