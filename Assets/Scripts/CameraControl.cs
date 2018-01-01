using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    GameObject player;
    Vector3 offset;
    public float smoothness = 0.125f;
    public float xWeight = 0.5f;

    float conversion = .1f;
    float maxDragDist = 1000f;
    float distDragged;


    private void Start() {
        offset = transform.position;
        player = GameManager.instance.player;
    }

    void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x * xWeight, 0, player.transform.position.z) + offset, smoothness);//new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset, smoothness);

    }
}
