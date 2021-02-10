using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private Text volumeText;
    private Toggle toggleFullscreen;
    private Dropdown qualityDropdown;
    private float volume;
    private Settings settings;
    private bool insideDropdown = false;
    private void Awake()
    {

        
        if (SettingsPersistence.Instance.GetSettings() == null)
        {
            settings = new Settings
            {
                Volume = 50f,

                QualityIndex = 1,

                FullScreen = true
            };
        }
        else
            settings = SettingsPersistence.instance.GetSettings();


    }

    private void Start()
    {

        volumeText = GameObject.Find("VolumeText").GetComponent<Text>();
        toggleFullscreen = GameObject.Find("ToggleFullscreen").GetComponent<Toggle>();
        qualityDropdown = GameObject.Find("QualityDropdown").GetComponent<Dropdown>();

        //set volume value
        volume = settings.Volume;

        //set quality value
        QualitySettings.SetQualityLevel(settings.QualityIndex);
        qualityDropdown.value = settings.QualityIndex;

        //set fullscreen value
        Screen.fullScreen = settings.FullScreen;
        toggleFullscreen.isOn = settings.FullScreen;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!insideDropdown)
            {
                SettingsPersistence.Instance.SaveSettingsToFile(settings);

                if (SceneManager.GetActiveScene().name.Equals("SettingsMenu"))
                    SceneManager.LoadScene("Menu");

            }
            else
                insideDropdown = false;
           
        }

        SoundManager.Instance.MusicSource.volume = volume / 100;

        volumeText.text = volume.ToString();
    }

    // subir / bajar volumen
    public void VolumeUpOnClick()
    {
        this.volume++;
        settings.Volume = this.volume;
    }

    public void VolumeDownOnClick()
    {
        this.volume--;
        settings.Volume = this.volume;

    }

    

    //calidad de gráficos
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        settings.QualityIndex = qualityIndex;

    }

    //pantalla completa/modo ventana
    public void ToggleFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        settings.FullScreen = fullScreen;

    }

    public void OnClickDropdownListener()
    {
        insideDropdown = true;
    }

}
