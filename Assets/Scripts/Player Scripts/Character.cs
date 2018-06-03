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
            GameManager.instance.Die();
        }


    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.M)) {
            GameManager.instance.LevelFinished();
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            GameManager.instance.ResetSavedLevels();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            print("shot");
            ScreenCapture.CaptureScreenshot("/Users/alexanderwarren/Desktop/shot.png", 7);
        }
    }

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.GetComponentInParent<Platform>() != null){
            if(col.contacts[0].normal == Vector3.up) {
                if(col.collider.gameObject.GetComponent<ShrinkingPlatform>() != null){
                    col.collider.gameObject.GetComponent<ShrinkingPlatform>().ResetScale();
                }
                transform.parent = col.gameObject.transform;
                col.gameObject.GetComponent<Platform>().StartCoroutine(col.gameObject.GetComponent<Platform>().Oscillate());
                StartCoroutine(SnapToPosition(transform, col.collider.gameObject.transform, platformSnapTime));
                if(col.collider.gameObject.tag == "Finish"){
                    GameManager.instance.LevelFinished();
                }
            }
        }

        if(col.collider.gameObject.tag == "Bank"){
            GameManager.instance.Die();
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
        if(col.gameObject.GetComponent<PlatformButton>() != null){
            col.gameObject.GetComponent<PlatformButton>().Push();
        }
    }

    void OnTriggerExit(Collider col){
        if(col.gameObject.GetComponent<PlatformButton>() != null){
            col.gameObject.GetComponent<PlatformButton>().Release();
        }
    }

    IEnumerator SnapToPosition(Transform a, Transform b, float totalTime){
        float elapsedTime = 0f;
        Vector3 start = a.position;
        Vector3 end = new Vector3(b.position.x, transform.position.y, b.position.z);


        while(elapsedTime < totalTime){
            end = new Vector3(b.position.x, transform.position.y, b.position.z);
            Vector3 targetPos = Vector3.Lerp(start, end, elapsedTime / totalTime);
            transform.position += new Vector3(targetPos.x - transform.position.x, 0, targetPos.z - transform.position.z);
            //transform.position = Vector3.Lerp(start, end, elapsedTime / totalTime);
            transform.rotation = Quaternion.Lerp(a.rotation, b.rotation, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        transform.rotation = b.transform.rotation;
        transform.position = new Vector3(b.position.x, transform.position.y, b.position.z);
    }

    public bool isGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, 1f);
    }
}
 