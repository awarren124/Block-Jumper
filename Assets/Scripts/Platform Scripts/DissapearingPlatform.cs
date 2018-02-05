using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {

    public float duration = 2f;
    public Color initialColor = Color.white;
    public Color endColor = Color.black;
    public bool affectEmission = false;
    public Color emissionEndColor = new Color(0.49411f, 0.70196f, 0.87450f);

    void OnCollisionEnter(Collision col){
        if(col.collider.gameObject.tag == "Player"){
            StartCoroutine(Dissapear());
        }
    }

    public IEnumerator Dissapear(){
        float timer = 0;
        Color initialEmission = Color.white;
        if(affectEmission){
            initialEmission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        }
        while(timer < duration){
            GetComponent<Renderer>().material.color = Color.Lerp(initialColor, endColor, timer / duration);

            if(affectEmission){
                GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(initialEmission, emissionEndColor, timer / duration));
            }

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame(); 
        }
        GetComponent<Platform>().shouldParent = false;
        GameManager.instance.player.transform.parent = null;
        Destroy(gameObject);
    }

}
