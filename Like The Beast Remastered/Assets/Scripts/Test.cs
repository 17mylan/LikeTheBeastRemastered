using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float speed = 2.0f;
    public Vector3 startPosition;
    public Vector3 endPosition;
    private bool movingUp = true;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + new Vector3(0, 5, 0);
    }

    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingUp = true;
            }
        }
    }
}
