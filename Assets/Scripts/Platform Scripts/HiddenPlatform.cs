using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Platform))]
public class HiddenPlatform : MonoBehaviour {

    public float duration;
    public Vector3 targetScale;

    private void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().size * targetScale.x);
    }

    public IEnumerator Show(){
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = false;
        }
        float timer = 0;
        while(timer <= duration){
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = true;
        }
    }

    public IEnumerator Hide(){
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = false;
        }
        float timer = 0;
        Vector3 currentScale = transform.localScale;
        while(timer <= duration){
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(GetComponent<ShrinkingPlatform>()){
            GetComponent<ShrinkingPlatform>().shouldScale = true;
        }
    }
}
