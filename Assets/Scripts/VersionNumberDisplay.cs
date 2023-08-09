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
        string versionNumber = PlayerSettings.bundleVersion;
        versionText.text = "Version: " + versionNumber;
    }
}
