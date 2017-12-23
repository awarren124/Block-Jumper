using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour {

	
    public void SelectLevel(Dropdown dropdown){
        GameManager.LoadLevel(dropdown.value);
    }

}
