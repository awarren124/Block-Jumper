using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public float yStrength = 10;
    public float zStrength = 20;
    public float magnitude = 10;

    public float fallMultiplier = 2.5f;
    Rigidbody rb;
    Character character;

    float touchStart;
    float maxDist = 300f;

    float distDragged;
    void Start(){
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

	void Update () {
        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);
            switch(touch.phase) {
                case TouchPhase.Began:
                    touchStart = touch.position.y;
                    break;
                case TouchPhase.Moved:
                    distDragged += touch.deltaPosition.y;
                    GameManager.instance.levelUI.UpdatePower(Mathf.Clamp((touchStart - touch.position.y)/maxDist, 0f, 1f));
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    if(character.isGrounded()) {
                        if(touchStart >= touch.position.y) {
                            magnitude = Mathf.Sqrt((touchStart - touch.position.y) / maxDist);
                            magnitude = Mathf.Clamp(magnitude, 0f, 1f);
                            Vector3 force = new Vector3(0, yStrength, zStrength) * magnitude;
                            print(force == null);
                            rb.AddRelativeForce(force);
                        }
                    }
                    distDragged = 0f;
                    break;/*
                case TouchPhase.Canceled:
                    break;
                default:
                    break;*/
            }
        }
        if(rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
	}

}
