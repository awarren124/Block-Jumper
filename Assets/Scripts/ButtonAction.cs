using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ButtonAction {

    public GameObject target;
    public ButtonActionType type;
    public bool isStrict;

}

public enum ButtonActionType {
    PlatformStopMove,
    PlatformMove,
    PlatformRotate,
    PlatformStopRotate,
    PlatformShow,
    PlatformHide,
    PlatformDissapear,
    PlatformAllowShrink,
    PlatformProhibitShrink

}
