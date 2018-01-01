using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditorOptions : MonoBehaviour {
    float scale;

    /*public override void OnInspectorGUI(){
        DrawDefaultInspector();
        scale = GUILayout.HorizontalSlider(scale, 0, 1);
        MovingPlatform plat = (MovingPlatform)target;
        if(GUILayout.Button("Invert Keys")){
            plat.xPath = plat.xPath.Inverted();
            plat.zPath = plat.zPath.Inverted();
            Debug.Log(plat.xPath.keys[3].value);
            Debug.Log(plat.actualval());
            //plat.xPath.Invert();
            //plat.zPath.Invert();

            for(int i = 0; i < plat.xPath.length; i++){
                plat.xPath.MoveKey(i, new Keyframe(plat.xPath.keys[i].time, -plat.xPath.keys[i].value));
            }
            for(int i = 0; i < plat.zPath.length; i++){
                plat.zPath.MoveKey(i, new Keyframe(plat.zPath.keys[i].time, -plat.zPath.keys[i].value));
            }

            //plat.xPat
            Debug.Log(plat.xPath.keys[3].value);

            plat.InvertCurves();
        }
    }*/

}
