using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class ShrinkingPlatform : MonoBehaviour {

    public AnimationCurve curve;
    float timer = 0f;
    Vector3 initialSize;
    [HideInInspector]
    public bool shouldScale = true;
    public bool consistentTime = false;

    void Start(){
        initialSize = GetComponent<Platform>().initialScale;
    }

    void FixedUpdate(){
        if(consistentTime){
            timer += Time.fixedDeltaTime;
        }
        if(shouldScale) {
            transform.localScale = initialSize * curve.Evaluate(timer);
            if(!consistentTime){
                timer += Time.fixedDeltaTime;
            }
        }


    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.tag == "Player") {
            shouldScale = false;
            transform.localScale = initialSize;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.collider.gameObject.tag == "Player") {
            shouldScale = true;
        }
    }
}
