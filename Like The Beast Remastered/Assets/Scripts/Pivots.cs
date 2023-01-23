using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivots : MonoBehaviour
{
    private Vector3 aim;
    private void Update()
    {
        if (GameObject.Find("PauseMenu") == false)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos += Camera.main.transform.forward * 1f;
            aim = Camera.main.ScreenToWorldPoint(mousePos) + (Camera.main.transform.forward * 1);
            transform.LookAt(aim);
        }
    }
}
