using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float panBorderThickness = 20f;

    private Vector3 newPosition;

    void Awake()
    {
        newPosition = transform.position;
    }

    void Update()
    {
        if (Input.mousePosition.y >= Screen.height - panBorderThickness) 
        {
            newPosition += transform.up * panSpeed;
        }
        else if (Input.mousePosition.y <= panBorderThickness) 
        {
            newPosition -= transform.up * panSpeed;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness) 
        {
            newPosition += transform.right * panSpeed;
        }
        else if (Input.mousePosition.x <= panBorderThickness) 
        {
            newPosition -= transform.right * panSpeed;
        }

        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        transform.position = newPosition;
    }
}
