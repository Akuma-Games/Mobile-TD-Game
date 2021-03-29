using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    [SerializeField] public GameObject toggleButton1;
    [SerializeField] public GameObject toggleButton2;
    public static bool toggled1;
    public static bool toggled2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleButtonR()
    {
        toggleButton1.SetActive(toggleButton1.activeSelf ? false : true);
        toggled1 = toggleButton1.activeSelf;
    }

    public void ToggleButtonL()
    {
        toggleButton2.SetActive(toggleButton2.activeSelf ? false : true);
        toggled2 = toggleButton2.activeSelf;
    }

}
