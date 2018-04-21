using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Control : MonoBehaviour {

    public float yStrength = 10;
    public float zStrength = 20;
    public float magnitude = 10;

    public float fallMultiplier = 2.5f;
    Rigidbody rb;
    Character character;

    public bool computerControls = false;

    float touchStart;
    float maxDist = 300f;

    public Vector3 oldCharacterVelocity;

    float distDragged;
    void Start(){
        rb = GetComponent<Rigidbody>();
        character = GetComponent<Character>();
    }

	void Update () {
        if(!GameManager.instance.isPaused) {
            if(computerControls) {
                GameManager.instance.levelUI.UpdatePower(1 - (Input.mousePosition.y / Screen.height));
                if(Input.GetMouseButtonDown(0) && !TouchOnUI(Input.mousePosition)) {
                    float pos = Screen.height - Input.mousePosition.y;
                    if(character.isGrounded()) {
                        magnitude = Mathf.Sqrt(pos / Screen.height);
                        magnitude = Mathf.Clamp(magnitude, 0f, 1f);
                        Vector3 force = new Vector3(0, yStrength, zStrength) * magnitude;
                        rb.AddRelativeForce(force);
                    }
                }
            } else {
                if(Input.touchCount > 0){
                    Touch touch = Input.GetTouch(0);

                     
                    if(!TouchOnUI(touch.position)) {
                        switch(touch.phase) {
                            case TouchPhase.Began:
                                touchStart = touch.position.y;
                                break;
                            case TouchPhase.Moved:
                                distDragged += touch.deltaPosition.y;
                                GameManager.instance.levelUI.UpdatePower(Mathf.Clamp((touchStart - touch.position.y) / maxDist,
                                                                                 0f,
                                                                                 1f));
                                break;
                            case TouchPhase.Stationary:
                                break;
                            case TouchPhase.Ended:
                                if(character.isGrounded()) {
                                    if(touchStart >= touch.position.y) {
                                        magnitude = Mathf.Sqrt((touchStart - touch.position.y) / maxDist);
                                        magnitude = Mathf.Clamp(magnitude, 0f, 1f);
                                        float pos = Screen.height - Input.mousePosition.y;
                                        //magnitude = pos / Screen.height;
                                        Vector3 force = new Vector3(0, yStrength, zStrength) * magnitude;
                                        rb.AddRelativeForce(force);
                                    }
                                }
                                distDragged = 0f;
                                break;
                        }
                    }
                }
            }
            if(rb.velocity.y < 0) {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }
	}

    public bool TouchOnUI(Vector2 pos){
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = pos;
        List<RaycastResult> results = new List<RaycastResult>(); //EventSystem.current.RaycastAll()
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        bool retVal = false;
        foreach(var result in results) {
            GameObject go = result.gameObject;
            if(go.tag == "NoTouchThrough") {
                retVal = true;
            }
        }
        return retVal;

    }

}
