using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour {

    public Vector3 platformOffset = new Vector3(0f, 12f, 0f);
    public float platformSnapTime = 1f;
    float dieHeight = -10f;

    void Awake(){
        GameManager.instance.player = gameObject;
    }

    void FixedUpdate(){


        if(transform.position.y < dieHeight){
            SceneManager.LoadScene(GameManager.currentLevel);
        }
    }

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.GetComponentInParent<Platform>() != null){
            if(col.contacts[0].normal == Vector3.up) {
                StartCoroutine(SnapToPosition(transform, col.collider.gameObject.transform, platformSnapTime));
                col.gameObject.GetComponent<Platform>().StartCoroutine(col.gameObject.GetComponent<Platform>().Oscillate());
            }
        }
    }

    void OnCollisionStay(Collision col){
        if(col.collider.gameObject.GetComponent<Platform>() != null) {
            if(col.collider.gameObject.GetComponent<Platform>().shouldParent) {
                transform.parent = col.gameObject.transform;
            }
        }
    }

    void OnCollisionExit(Collision col){
        if(col.collider.gameObject.GetComponent<Platform>() != null){
            transform.parent = null;
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.GetComponent<Button>() != null){
            col.gameObject.GetComponent<Button>().Push();
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.GetComponent<Button>() != null){
            col.gameObject.GetComponent<Button>().Release();
        }
    }

    IEnumerator SnapToPosition(Transform a, Transform b, float totalTime){
        float elapsedTime = 0f;

        while(elapsedTime < totalTime){
            transform.position = Vector3.Lerp(a.position, b.position + platformOffset, elapsedTime / totalTime);
            transform.rotation = Quaternion.Lerp(a.rotation, b.rotation, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.rotation = b.transform.rotation;
        transform.position = b.transform.position;
    }

    public bool isGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, 1f);
    }
}
