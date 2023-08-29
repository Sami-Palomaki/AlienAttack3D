using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CursorOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //tämän lisäks tee playermovementtiin tai hiirellä klikkaus inputtii public static bool canMove = true;
    // ja  void Update()
    //     {
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        if(canMove == true)
    // jne .... ettei liiku menussa ja pysäyttää ajan
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if(Time.timeScale == 1)
        {
            Shooting.canMove = false;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(Time.timeScale == 1)
        {
            Shooting.canMove = true;
        }
    }

}
