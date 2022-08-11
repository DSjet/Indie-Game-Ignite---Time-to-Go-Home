using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartOnDisplay : MonoBehaviour
{

    public UnityEvent ev;

    // Start is called before the first frame update
    void Start()
    {
        ev?.Invoke();
    }
}
