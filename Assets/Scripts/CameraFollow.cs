using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Pelaajan Transform-komponentti

    public Vector3 offset;    // Halutun etäisyyden säätö kameran ja pelaajan välille
    public float smoothSpeed = 0.125f;  // Kuinka pehmeästi kamera seuraa pelaajaa

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);  // Käännetään kamera kohti pelaajaa
    }
}
