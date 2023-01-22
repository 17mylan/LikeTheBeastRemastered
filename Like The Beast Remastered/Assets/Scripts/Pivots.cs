using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivots : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(Input.mousePosition);
    }
}
