using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{

    [SerializeField] public Joystick cameraControllerR;
    [SerializeField] public Joystick cameraControllerL;

    [SerializeField] private float panSpeed = 1f;
    [SerializeField] private float panSpeedJoystick = 0.05f;
    [SerializeField] private float panBorderThickness = 20f;

    private Vector3 newPosition;
    private Vector3 startingPosition;

    [SerializeField] float maxUpDistance;
    [SerializeField] float maxDownDistance;
    [SerializeField] float maxLeftDistance;
    [SerializeField] float maxRightDistance;
    [SerializeField] float maxZoom;

    void Awake()
    {
        newPosition = transform.position;
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;

        if (ControlsScript.toggled1 == false)
        {
            // Mouse hovering at the top
            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                if (!((newPosition + transform.up * panSpeed - startingPosition).magnitude > maxUpDistance))
                {
                    newPosition += transform.up * panSpeed;
                }
            }
            // Mouse hovering at the bottom
            else if (Input.mousePosition.y <= panBorderThickness)
            {
                if (!((newPosition - transform.up * panSpeed - startingPosition).magnitude > maxDownDistance))
                {
                    newPosition -= transform.up * panSpeed;
                }
            }

            // Mouse hovering on the right
            if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                if (!((newPosition + transform.right * panSpeed - startingPosition).magnitude > maxRightDistance))
                {
                    newPosition += transform.right * panSpeed;
                }
            }
            // Mouse hovering on the left
            else if (Input.mousePosition.x <= panBorderThickness)
            {
                if (!((newPosition - transform.right * panSpeed - startingPosition).magnitude > maxLeftDistance))
                {
                    newPosition -= transform.right * panSpeed;
                }
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                if (Camera.main.orthographicSize > maxZoom)
                {
                    Camera.main.orthographicSize--;
                    //maxDownDistance += 0.5f;
                    //maxLeftDistance += 0.5f;
                    //maxRightDistance += 0.5f;
                    //maxUpDistance += 0.5f;
                }
            }

            if (Input.mouseScrollDelta.y < 0)
            {
                if (Camera.main.orthographicSize < 10)
                {
                    Camera.main.orthographicSize++;
                    //maxDownDistance -= 0.5f;
                    //maxLeftDistance -= 0.5f;
                    //maxRightDistance -= 0.5f;
                    //maxUpDistance -= 0.5f;
                }
            }
        }


        if (ControlsScript.toggled1 == true)
        {
            //joystick controls
            if (cameraControllerR.Vertical > 0)
            {
                if (!((newPosition + transform.up * panSpeed - startingPosition).magnitude > maxUpDistance))
                {
                    newPosition += transform.up * panSpeedJoystick;
                }
            }

            else if (cameraControllerR.Vertical < 0)
            {
                if (!((newPosition - transform.up * panSpeed - startingPosition).magnitude > maxDownDistance))
                {
                    newPosition -= transform.up * panSpeedJoystick;
                }
            }

            if (cameraControllerR.Horizontal > 0)
            {
                if (!((newPosition + transform.right * panSpeed - startingPosition).magnitude > maxRightDistance))
                {
                    newPosition += transform.right * panSpeedJoystick;
                }
            }
            else if (cameraControllerR.Horizontal < 0)
            {
                if (!((newPosition - transform.right * panSpeed - startingPosition).magnitude > maxLeftDistance))
                {
                    newPosition -= transform.right * panSpeedJoystick;
                }
            }
        }

        if (ControlsScript.toggled2 == true)
        {
            //joystick controls
            if (cameraControllerL.Vertical > 0)
            {
                if (!((newPosition + transform.up * panSpeed - startingPosition).magnitude > maxUpDistance))
                {
                    newPosition += transform.up * panSpeedJoystick;
                }
            }

            else if (cameraControllerL.Vertical < 0)
            {
                if (!((newPosition - transform.up * panSpeed - startingPosition).magnitude > maxDownDistance))
                {
                    newPosition -= transform.up * panSpeedJoystick;
                }
            }

            if (cameraControllerL.Horizontal > 0)
            {
                if (!((newPosition + transform.right * panSpeed - startingPosition).magnitude > maxRightDistance))
                {
                    newPosition += transform.right * panSpeedJoystick;
                }
            }
            else if (cameraControllerL.Horizontal < 0)
            {
                if (!((newPosition - transform.right * panSpeed - startingPosition).magnitude > maxLeftDistance))
                {
                    newPosition -= transform.right * panSpeedJoystick;
                }
            }
        }






        //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
        transform.position = newPosition;




    }
}
