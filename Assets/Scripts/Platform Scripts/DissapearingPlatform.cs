using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {

    public float duration = 2f;
    public Color initialColor = Color.white;
    public Color endColor = Color.black;

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.tag == "Player"){
            StartCoroutine(Dissapear());
        }
    }

    public IEnumerator Dissapear(){
        float timer = 0;
        while(timer < duration){
            GetComponent<Renderer>().material.color = Color.Lerp(initialColor, endColor, timer / duration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        GetComponent<Platform>().shouldParent = false;
        GameManager.instance.player.transform.parent = null;
        Destroy(gameObject);
    }

}
