using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class MovingPlatform : MonoBehaviour {
	
    float timer = 0f;
    public AnimationCurve xPath;
    public AnimationCurve zPath;
    public AnimationCurve rotPath;
    Vector3 initialPos;
    Quaternion initialRot;
    public bool shouldMove = true;

    void Start(){
        initialPos = transform.position;
        initialRot = transform.rotation;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float maxTime;
        try {
            if(xPath.keys[xPath.length - 1].time > zPath.keys[zPath.length - 1].time) {
                maxTime = xPath.keys[xPath.length - 1].time;
            } else {
                maxTime = zPath.keys[zPath.length - 1].time;
            }
            for(float i = 0; i < maxTime; i += 0.1f) {
                if(initialPos != Vector3.zero) {
                    Gizmos.DrawLine(new Vector3(xPath.Evaluate(i) + initialPos.x, initialPos.y, zPath.Evaluate(i) + initialPos.z), new Vector3(xPath.Evaluate(i + 0.1f) + initialPos.x, initialPos.y, zPath.Evaluate(i + 0.1f) + initialPos.z));
                } else {
                    Gizmos.DrawLine(new Vector3(xPath.Evaluate(i) + transform.position.x, transform.position.y, zPath.Evaluate(i) + transform.position.z), new Vector3(xPath.Evaluate(i + 0.1f) + transform.position.x, transform.position.y, zPath.Evaluate(i + 0.1f) + transform.position.z));
                }

            }

        } catch(System.Exception) {

        }
    }

    void FixedUpdate () {
        if(shouldMove) {
            transform.position = new Vector3(initialPos.x + xPath.Evaluate(timer), transform.position.y, initialPos.z + zPath.Evaluate(timer));
            transform.localEulerAngles = new Vector3(initialRot.eulerAngles.x, initialRot.eulerAngles.y + rotPath.Evaluate(timer), initialRot.eulerAngles.z);
            timer += Time.fixedDeltaTime;
        }

	}
}
