using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public float smoothness = 0.125f;

	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, 0, player.transform.position.z) + offset, smoothness);
	}
}
