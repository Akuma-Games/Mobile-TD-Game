using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    [SerializeField] public GameObject toggleButton1;
    public static bool toggled1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleButton()
    {
        toggleButton1.SetActive(toggleButton1.activeSelf ? false : true);
        toggled1 = toggleButton1.activeSelf;
    }

}
