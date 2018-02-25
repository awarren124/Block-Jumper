using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public float oscillationDampen = 3f;
    public float oscillationAmplitude = 0.54f;
    public float maxTime = 2f;
    public int oscillations = 3;
    [HideInInspector]
    public bool shouldParent = true;
    [HideInInspector]
    public Vector3 initialScale;
    public bool isOscillating = false;
    void Awake(){
        initialScale = transform.localScale;
    }

    public IEnumerator Oscillate(){
        
        isOscillating = true;
        Vector3 initialPos = transform.position;
        //float currentOffset = oscillationAmplitude;
        float timer = 0f;
        while (timer < maxTime){
            transform.position = new Vector3(transform.position.x, initialPos.y + -oscillationAmplitude * Mathf.Exp(-oscillationDampen * timer) * Mathf.Sin(oscillations * Mathf.PI * timer), transform.position.z);
            if(!GameManager.instance.isPaused) {
                timer += Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
        transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);
        isOscillating = false;
    }


}

public enum PlatformModifiers{
    Moving,
    Dissapearing,
    Hidden,
    Shrinking
}