using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Camera cam;

    public float damage = 10f;
    public float range = 100f;
    
    [Header("AudioSource")]
    public string ammoPickUpSound;
    public string fireSound;
    public string unloadSound;
    public string loadSound;
    public string reloadSound;
    public string pickupSound;
    public string dropSound;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            AudioManager.instance.Play(fireSound, this.gameObject);
        }
    }

    void Shoot()
    {
        // Instantiate and play muzzle flash
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        Destroy(muzzleFlash, 0.1f); // Destroy after a short duration (adjust as needed)

        

        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            Target target = hitInfo.transform.GetComponent<Target>();

            if (target != null)
            {
                
                Debug.Log("ota 10 damagea");
                target.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Vihollinen on null");
            }

            if (hitInfo.transform.gameObject.activeSelf)
            {
                Debug.Log("Vihollinen on aktiivinen");
            }
            else
            {
                Debug.Log("Vihollinen ei ole aktiivinen");
            }
        }
    }
}
