using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeShake : MonoBehaviour
{
    public CameraShake cameraShake;
    void Start()
    {
        cameraShake.Invoke("shake", 0.3f);
    }
}
