using System.Collections;
using UnityEngine;
using TMPro;

public class TMPNotificationManager : MonoBehaviour
{
    public TMP_Text notificationText;
    public float displayDuration = 3f; // Ilmoituksen näyttöaika (sekunneissa)

    private void Start()
    {
        // Piilota ilmoitusteksti alussa
        notificationText.gameObject.SetActive(false);

        // Aloita ilmoituksen näyttäminen pelin alkaessa
        StartCoroutine(ShowNotification());
    }

    private IEnumerator ShowNotification()
    {
        notificationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        notificationText.gameObject.SetActive(false);
    }
}
