using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    

    public float damage = 10f;
    public float range = 100f;

    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private float _bulletHoleLifetime = 5f; // Luodinreiän elinaika
    
    [Header("AudioSource")]
    public string ammoPickUpSound;
    public string fireSound;
    public string unloadSound;
    public string loadSound;
    public string reloadSound;
    public string pickupSound;
    public string dropSound;
    public static bool canMove = true;



    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
            //if(canMove == true)
            //{
                if(Input.GetButtonDown("Fire1"))
                {
                    if(canMove == true)
                        {
                            Shoot();
                    
                            AudioManager.instance.Play(fireSound, this.gameObject);
                        }
                }
            //}
        //}
    }

    void Shoot()
    {
        // Instantiate and play muzzle flash
        GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
        Destroy(muzzleFlash, 0.15f); // Destroy after a short duration (adjust as needed)
        
        RaycastHit hitInfo;

       // Muokataan ampumissuuntaa niin, että se säilyttää saman korkeuden kuin pelaaja
        Vector3 playerPosition = transform.position; // Pelaajan sijainti
        Vector3 targetDirection = firePoint.transform.forward; // Suunta eteenpäin
        targetDirection.y = 0; // Asetetaan Y-koordinaatti nollaksi, jotta ammus liikkuu samalle korkeudelle


        // Normalisoidaan suunta, jotta ammus ei liiku liian pitkälle
        targetDirection.Normalize();



        if (Physics.Raycast(firePoint.transform.position, targetDirection, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);

            Target target = hitInfo.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Invalid target");
            }

            if (hitInfo.collider.CompareTag("Wall"))
            {
                GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                obj.transform.position += obj.transform.forward / 1000;

                Destroy(obj, _bulletHoleLifetime);
            }
        }
    }
}
