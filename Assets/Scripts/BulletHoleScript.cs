using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleScript : MonoBehaviour
{
    [SerializeField] private GameObject _bulletHolePrefab;
    [SerializeField] private float _bulletHoleLifetime = 5f; // Luodinreiän elinaika

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Wall"))
                {
                    GameObject obj = Instantiate(_bulletHolePrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    obj.transform.position += obj.transform.forward / 1000;

                    Destroy(obj, _bulletHoleLifetime);
                }
            }
        }
    }
}
