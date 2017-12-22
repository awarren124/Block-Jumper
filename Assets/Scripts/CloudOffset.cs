using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudOffset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animator anim = GetComponent<Animator>();
        float rand = Random.Range(0f, 10f);
        anim.Play("Idle", -1, rand);
	}

}
