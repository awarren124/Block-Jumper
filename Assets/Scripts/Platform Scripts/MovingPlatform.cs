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

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        float maxTime;
        if(xPath.keys[xPath.length - 1].time > zPath.keys[zPath.length - 1].time){
            maxTime = xPath.keys[xPath.length - 1].time;
        }else{
            maxTime = zPath.keys[zPath.length - 1].time;
        }
        for(float i = 0; i < maxTime; i += 0.1f) {
            if(initialPos != Vector3.zero) {
                Gizmos.DrawLine(new Vector3(xPath.Evaluate(i), -0.5f + initialPos.y, zPath.Evaluate(i)) + initialPos, new Vector3(xPath.Evaluate(i + 0.1f), -0.5f + initialPos.y, zPath.Evaluate(i + 0.1f)) + initialPos);
            }else {
                Gizmos.DrawLine(new Vector3(xPath.Evaluate(i), -0.5f + transform.position.y, zPath.Evaluate(i)) + transform.position, new Vector3(xPath.Evaluate(i + 0.1f), -0.5f + transform.position.y, zPath.Evaluate(i + 0.1f)) + transform.position);
            }

        }
    }

    void FixedUpdate () {
        if(shouldMove) {
            transform.position = new Vector3(initialPos.x + xPath.Evaluate(timer), transform.position.y, initialPos.z + zPath.Evaluate(timer));
            transform.rotation = Quaternion.Euler(initialRot.eulerAngles.x, initialRot.eulerAngles.y + rotPath.Evaluate(timer), initialRot.eulerAngles.z);
            timer += Time.fixedDeltaTime;
        }

	}
}
