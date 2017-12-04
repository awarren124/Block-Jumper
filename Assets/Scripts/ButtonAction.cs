using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ButtonAction {

    public GameObject target;
    public ButtonActionType type;

}

public enum ButtonActionType {
    PlatformStop,
    PlatformMove,
    PlatformShow,
    PlatformHide,
    PlatformDissapear
}
