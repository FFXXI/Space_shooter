using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private int pointerID;
    private bool touched;
    private bool canFire;


    private void Awake()
    {
        touched = false;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        canFire = true;
        touched = true;
        pointerID = eventData.pointerId;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            canFire = false;
            touched = false;

        }

    }

    public bool CantFire()
    {
        return canFire;
    }







}
    
