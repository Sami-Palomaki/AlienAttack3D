using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodScript : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyGameObject", 6f);

    }

    private void DestroyGameObject()
    {
        // Tuhotaan tämä GameObject
        Destroy(gameObject);
    }
}
