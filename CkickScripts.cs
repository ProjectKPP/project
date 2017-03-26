using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CkickScripts : MonoBehaviour {

    public bool clickedIs = true; //клік

    void OnMouseDown()
    {
        clickedIs = true;
    }

    void OnMouseUp()
    {
        clickedIs = false;
    }
}
