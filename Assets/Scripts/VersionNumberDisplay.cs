using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionNumberDisplay : MonoBehaviour
{
    public TMP_Text versionText;

    private void Start()
    {
        // Haetaan versionumero PlayerSettingsistä ja asetetaan se tekstikenttään.
        string versionNumber = GetVersionNumber();
        versionText.text = "Version: " + versionNumber;
    }

    private string GetVersionNumber()
    {
        #if UNITY_EDITOR
        return PlayerSettings.bundleVersion;
        #else
        return "N/A"; // Tai voit palauttaa oletusarvon
        #endif
    }
}
