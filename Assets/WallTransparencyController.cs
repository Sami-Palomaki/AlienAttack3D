using UnityEngine;
using DG.Tweening;

public class WallTransparencyController : MonoBehaviour
{
    public Transform player; // Aseta pelaajan GameObject Unity-editorissa
    public Transform wall;   // Aseta seinän GameObject Unity-editorissa

    private Renderer wallRenderer;
    private Material originalMaterial;
    private bool isPlayerBehindWall = false;

    private void Start()
    {
        wallRenderer = wall.GetComponent<Renderer>();
        originalMaterial = wallRenderer.material;
    }

    private void Update()
    {
        // Tarkista, onko pelaaja seinän takana
        Vector3 playerPosition = player.position;
        Vector3 wallPosition = wall.position;

        Vector3 directionToWall = wallPosition - playerPosition;
        float distanceToWall = directionToWall.magnitude;

        RaycastHit hitInfo;
        if (Physics.Raycast(playerPosition, directionToWall, out hitInfo, distanceToWall))
        {
            if (hitInfo.collider.gameObject == wall.gameObject)
            {
                if (!isPlayerBehindWall)
                {
                    isPlayerBehindWall = true;
                    SetWallTransparency(0.5f, 1.0f);
                }
            }
            else
            {
                if (isPlayerBehindWall)
                {
                    isPlayerBehindWall = false;
                    SetWallTransparency(1.0f, 1.0f);
                }
            }
        }
    }

    private void SetWallTransparency(float targetAlpha, float duration)
    {
        Color targetColor = new Color(originalMaterial.color.r, originalMaterial.color.g, originalMaterial.color.b, targetAlpha);
        originalMaterial.DOColor(targetColor, duration);
    }
}
