using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryItems : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject inventoryOpen;
    public GameObject inventoryClosed;

    public Image[] emptySlots;
    public Sprite[] icons;

    public Sprite emptyIcon;

    public static int newIcon = 0;
    public static bool iconUpdate = false;

    private int max;
    
    public static int healthPack = 0;

    public static int pistol = 0;

    public static int shotgun = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventoryMenu.SetActive(false);
        inventoryOpen.SetActive(false);
        inventoryClosed.SetActive(true);  
        max = emptySlots.Length;

        //temp
        healthPack = 0;
        pistol = 0;
        shotgun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(iconUpdate == true)
        {
            for(int i = 0; i < max; i++)
            {
                if(emptySlots[i].sprite == emptyIcon)
                {
                    max = i;
                    emptySlots[i].sprite = icons[newIcon];
                    emptySlots[i].transform.gameObject.GetComponent<HintMessage>().objectType = newIcon;
                }
            }
            StartCoroutine(Reset());
        }
    }
    public void OpenMenu()
    {
        inventoryMenu.SetActive(true);
        inventoryOpen.SetActive(true);
        inventoryClosed.SetActive(false);
        Time.timeScale = 0f;

    }
    public void ClosedMenu()
    {
        inventoryMenu.SetActive(false);
        inventoryOpen.SetActive(false);
        inventoryClosed.SetActive(true);
        Time.timeScale = 1f;
        
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.01f); //tää jotenkin vaikuttaa tohon itemien poimintaan, kauan venaa ennenkö vetää updaten läpi tms
        iconUpdate = false;
        max = emptySlots.Length;
    }
}
