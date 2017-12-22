using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {

    public float duration = 2f;
    Color initialEmission;
    Color targetEmission = Color.black;
    public Color initialColor = Color.white;
    public Color endColor = Color.black;

    void Start() {
        initialEmission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.tag == "Player"){
            StartCoroutine(Dissapear());
        }
    }

    public IEnumerator Dissapear(){
        float timer = 0;
        while(timer < duration){
            GetComponent<Renderer>().material.color = Color.Lerp(initialColor, endColor, timer / duration);
            print(Color.Lerp(initialEmission, targetEmission, timer / duration));
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(initialEmission, targetEmission, timer / duration));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        GetComponent<Platform>().shouldParent = false;
        GameManager.instance.player.transform.parent = null;
        Destroy(gameObject);
    }

}
