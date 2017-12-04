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
    public bool shouldMove = true;

    void Start(){
        initialPos = transform.position;
    }

	void FixedUpdate () {
        if(shouldMove) {
            transform.position = new Vector3(initialPos.x + xPath.Evaluate(timer), transform.position.y, initialPos.z + zPath.Evaluate(timer));
            transform.rotation = Quaternion.Euler(Vector3.up * rotPath.Evaluate(timer));
            timer += Time.fixedDeltaTime;
        }

	}
}
