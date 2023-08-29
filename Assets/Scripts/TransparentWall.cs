using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TransparentWall : MonoBehaviour
{
    public Transform player; // Pelaajan GameObject tai sijainti
    public float transparencyDuration = 0.5f; // Läpinäkyvyyden muutoksen kesto sekunteina
    public float maxDistance = 5.0f; // Kuinka lähelle pelaajan pitää tulla, jotta muutos alkaa

    private Material wallMaterial;
    private float originalAlpha;
    private bool isTransparent = false;

    private void Start()
    {
        wallMaterial = GetComponent<Renderer>().material;
        originalAlpha = wallMaterial.color.a;
    }

    private void Update()
    {
        // Tarkista etäisyys pelaajan ja seinän välillä
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxDistance && !isTransparent)
        {
            // Aloita läpinäkyvyyden animaatio
            wallMaterial.DOFade(0.3f, transparencyDuration);
            isTransparent = true;
        }
        else if (distance > maxDistance && isTransparent)
        {
            // Aloita palauttavan animaation
            wallMaterial.DOFade(originalAlpha, transparencyDuration);
            isTransparent = false;
        }
    }
}
