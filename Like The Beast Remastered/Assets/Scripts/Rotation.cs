using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject myObject;
    public GameObject mySecondObject;
    void Update()
    {
        myObject.transform.rotation = Quaternion.Euler(myObject.transform.rotation.x, Camera.main.transform.rotation.y, myObject.transform.rotation.z);
    }
}
