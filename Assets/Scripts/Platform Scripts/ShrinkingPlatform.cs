using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class ShrinkingPlatform : MonoBehaviour {

    public AnimationCurve curve;
    float timer = 0f;
    Vector3 initialSize;
    public bool shouldScale = true;

    void Start(){
        initialSize = GetComponent<Platform>().initialScale;
    }

    void FixedUpdate(){
        if(shouldScale) {
            transform.localScale = initialSize * curve.Evaluate(timer);
            timer += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.gameObject.tag == "Player") {
            shouldScale = false;
            transform.localScale = initialSize;
            timer = 0f;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if(collision.collider.gameObject.tag == "Player") {
            shouldScale = true;
        }
    }
}
