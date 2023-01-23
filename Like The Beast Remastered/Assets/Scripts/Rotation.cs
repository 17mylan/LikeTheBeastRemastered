using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    GameObject target;
    public void Start()
    {
        target = GameObject.Find("Main Camera");
    }
    public void Update()
    {
        //transform.LookAt(target.transform);
    }
}
