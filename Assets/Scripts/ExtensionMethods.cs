using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static Vector3 SmoothStep(this Vector3 a, Vector3 b, float t){
        return new Vector3(Mathf.SmoothStep(a.x, b.x, t), Mathf.SmoothStep(a.y, b.y, t), Mathf.SmoothStep(a.z, b.z, t));
    }

    public static AnimationCurve Inverted(this AnimationCurve curve){
        if(curve.keys.Length == 0){
            return curve;
        }
        AnimationCurve newCurve = new AnimationCurve();
        for(int i = 0; i < curve.keys.Length; i++){
            Keyframe k = curve.keys[i];
            k.value = -k.value;
            newCurve.AddKey(k);
        }
        return newCurve;
    }
}
