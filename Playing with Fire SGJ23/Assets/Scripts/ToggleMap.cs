using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class ToggleMap : MonoBehaviour
{
    // put this script on any gameobject, i personally use an empty 'root' gameobject on which i put all my scripts alike
    public Canvas doorCanvas;    // now you have to drag and drop your canvas in the editor in the script component
    private bool doorActive = false; // do we have to display the canvas (true) or not (false)

    // Start is called before the first frame update
    void Start()
    {
        doorCanvas.gameObject.SetActive(doorActive);
        
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) { // if you press the E key
            doorActive = !doorActive; // change the state of your bool
            //doorCanvas.
            doorCanvas.gameObject.SetActive(doorActive); // display or not the canvas (following the state of the bool)
        }
    }
    
}
