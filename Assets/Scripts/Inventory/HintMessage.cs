using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hintBox;
    public Text message;

    private Vector3 screenPoint;

    private bool displaying = true;
    private bool overIcon = false;

    public int objectType = 0;

    public void OnPointerEnter(PointerEventData eventData)
    {
        overIcon = true;
        if(displaying == true)
        {
            hintBox.SetActive(true);
            screenPoint.x = Input.mousePosition.x + 400;
            screenPoint.y = Input.mousePosition.y;
            screenPoint.z = 1f;
            hintBox.transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
            MessageDisplay();
        }   
    }
    public void OnPointerExit(PointerEventData eventData)

    {
        overIcon = false;
        hintBox.SetActive(false);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        hintBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(overIcon == true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                displaying = false;
                hintBox.SetActive(false);

            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            displaying = true;
            hintBox.SetActive(false);

        }    
        
    }
    void MessageDisplay()
    {
        if(objectType == 0)
        {
            message.text = "empty";           
        }
        if(objectType == 1)
        {
            message.text = /*InventoryItems.pistol.ToString() +*/ "Basic pistol designed for Alien slaying";
        }

        if(objectType == 2)
        {
            message.text = InventoryItems.healthPack.ToString() + " Health kits that heals you once whenever you want 666 HPs";
        }
        if(objectType == 3)
        {
            message.text = /*InventoryItems.shotgun.ToString() + */ "This is Pump-action shotgun that makes shitloads of damage";
        }
    }
}
