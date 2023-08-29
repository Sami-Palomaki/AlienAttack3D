using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int number;

    public bool healthPack = false;

    public bool pistol = false;

    public bool shotgun = false; 

    private void OnTriggerEnter(Collider other)
    {
        
        
        if(other.CompareTag("Player"))
        {
            if(healthPack == true)
            {
                if(InventoryItems.healthPack == 0)
                {
                    DisplayIcons();
                }
                InventoryItems.healthPack++;
                Destroy(gameObject);
            }
                 
           
            else if(pistol == true)
            {
                if(InventoryItems.pistol == 0)
                {
                    DisplayIcons();
                }
                InventoryItems.pistol++;
                Destroy(gameObject);
            }
            else if(shotgun == true)
            {
                if(InventoryItems.shotgun == 0)
                {
                    DisplayIcons();
                }
                InventoryItems.shotgun++;
                Destroy(gameObject);
            }
            else
            {
                DisplayIcons();
                Destroy(gameObject);

            }

        }
    }
    void DisplayIcons()
    {
        InventoryItems.newIcon = number;
        InventoryItems.iconUpdate = true;
    }
}
