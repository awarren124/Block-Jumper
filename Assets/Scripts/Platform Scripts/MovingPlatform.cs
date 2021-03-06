﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class MovingPlatform : MonoBehaviour {
	
    float moveTimer = 0f;
    float rotateTimer = 0f;
    public AnimationCurve xPath;
    public AnimationCurve zPath;
    public AnimationCurve rotPath;
    Vector3 initialPos;
    Quaternion initialRot;
    [HideInInspector]
    public bool shouldMove = true;
    [HideInInspector]
    public bool shouldRotate = true;

    void Start(){
        initialPos = transform.position;
        initialRot = transform.rotation;

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        float maxTime;
        float minTime;
        try {
            float xTime = xPath.keys[xPath.length - 1].time - xPath.keys[0].time;
            float zTime = zPath.keys[zPath.length - 1].time - zPath.keys[0].time;
            if(xTime > zTime) {
                minTime = xPath.keys[0].time;
                maxTime = xTime;
            } else {
                minTime = zPath.keys[0].time;
                maxTime = zTime;
            }
            maxTime *= 4;
            for(float i = minTime; i < maxTime; i += 0.1f) {
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
        if(!GameManager.instance.isPaused) {
            
            if(shouldMove) {
                transform.position = new Vector3(initialPos.x + xPath.Evaluate(moveTimer),
                                             transform.position.y,
                                             initialPos.z + zPath.Evaluate(moveTimer));
                moveTimer += Time.fixedDeltaTime;
            }
            if(shouldRotate) {
                transform.rotation = Quaternion.Euler(initialRot.eulerAngles.x,
                                                  initialRot.eulerAngles.y + rotPath.Evaluate(rotateTimer),
                                                  initialRot.eulerAngles.z);
                rotateTimer += Time.fixedDeltaTime;
            }
        }

	}
    public void InvertCurves(){
        xPath = xPath.Inverted();
        zPath = zPath.Inverted();
    }
}
