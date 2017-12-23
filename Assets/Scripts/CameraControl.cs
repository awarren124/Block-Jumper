using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    GameObject player;
    Vector3 offset;
    public float smoothness = 0.125f;


    float conversion = .1f;
    float maxDragDist = 1000f;
    float distDragged;


    private void Start() {
        offset = transform.position;
        player = GameManager.instance.player;
    }

    /*void Update() {
        if(Input.touchCount > 0 && GameManager.instance.player.GetComponent<Character>().isGrounded()) {
            Touch touch = Input.GetTouch(0);
            switch(touch.phase) {
                case TouchPhase.Moved:
                    if(touch.deltaPosition.y < 0) {
                        distDragged += touch.deltaPosition.y;
                        print("dragged"+distDragged);
                        if(distDragged > -maxDragDist) {
                            print("max" + -maxDragDist);
                            transform.position = Vector3.up * touch.deltaPosition.y * conversion;
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    distDragged = 0f;
                    break;

            }
        }
    }*/

    void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset, smoothness);

    }
}
