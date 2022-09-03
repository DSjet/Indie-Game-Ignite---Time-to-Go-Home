using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool onHover = false;
    public UnityEvent ev;
    public void OnPointerEnter(PointerEventData eventData){
        onHover = true;
        ev?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData){
        onHover = false;
        ev?.Invoke();
    }
}
