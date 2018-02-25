using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class HiddenPlatform : MonoBehaviour {

    public float duration;
    public Vector3 targetScale;
    public bool startsHidden = true;
    [HideInInspector]
    public bool isHidden = false;
    private void OnDrawGizmos() {
        if(startsHidden) {
            Gizmos.color = Color.cyan;
            Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size * targetScale.x);
        }
    }

    void Start(){
        if(startsHidden){
            isHidden = true;
        }else{
            isHidden = false;
        }
    }

    public IEnumerator Show(){
        if(GetComponent<ShrinkingPlatform>()) {
            GetComponent<ShrinkingPlatform>().shouldScale = false;
        }
        float timer = 0;
        while(timer <= duration) {
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / duration);
            if(!GameManager.instance.isPaused) {
                timer += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        if(GetComponent<ShrinkingPlatform>()) {
            GetComponent<ShrinkingPlatform>().shouldScale = true;
        }
        isHidden = false;
    }

    public IEnumerator Hide(){
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = false;
        }
        float timer = 0;
        Vector3 currentScale = transform.localScale;
        while(timer <= duration){
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, timer / duration);
            if(!GameManager.instance.isPaused) {
                timer += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = true;
        }
        isHidden = true;
    }
}
