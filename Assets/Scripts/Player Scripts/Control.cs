using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    public float yStrength = 10;
    public float zStrength = 20;
    public float magnitude = 10;

    public float fallMultiplier = 2.5f;
    public bool test = false;
    Rigidbody rb;
    Character character;
    void Start(){
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

	void Update () {
        if(Input.touchCount > 0){
            magnitude = (Input.GetTouch(Input.touchCount - 1).position.y / Screen.height);
            if(Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended && character.isGrounded()){
                Vector3 force = new Vector3(0, yStrength, zStrength) * magnitude;
                rb.AddRelativeForce(force);
            }
        }
        if(rb.velocity.y < 0 && test){
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
	}

}
