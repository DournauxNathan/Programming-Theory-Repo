using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject crossHair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        crossHair.transform.position = GetMousePosition();
    }


    public static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos -= Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        return mousePos;
    }
}
