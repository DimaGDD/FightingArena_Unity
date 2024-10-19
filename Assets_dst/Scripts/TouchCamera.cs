using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed = false;
    public int fingerId;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == gameObject && eventData.position.x > Screen.width / 2)
        {
            pressed = true;
            fingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
