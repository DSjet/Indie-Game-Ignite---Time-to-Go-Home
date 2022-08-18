using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickAnywhere : MonoBehaviour
{
    public UnityEvent ev;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            ev?.Invoke();
        }
    }
}
