using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera mainCamera;
    Vector2 offset;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - mainCamera.ScreenToWorldPoint(eventData.position);
        Debug.Log("Fuck");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 newPos = mainCamera.ScreenToWorldPoint(eventData.position);
        //newPos.z = 0;
        transform.position = newPos + offset;
        Debug.Log(newPos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
