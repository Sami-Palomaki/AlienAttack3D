using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CursorController : MonoBehaviour
{

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
