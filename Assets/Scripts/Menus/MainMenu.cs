using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    private static MainMenu _instance;
    public AudioClip mainMenuMusic;
    private bool firstTime;

    void Awake()
    {
        //cursor will not be available, the player´ll use WASD or arrows to browse the menu.
       

        if (_instance == null)
        {
            _instance = this;
            firstTime = false;
        }

        if (firstTime)
        {
            SoundManager.Instance.PlayMusic(mainMenuMusic);
        }

        else if (_instance != this)
            Destroy(gameObject);

        
    }

    private void Update()
    {
        Debug.Log(Gamepad.current.name);
        //Cursor.visible = !Joystick.current.enabled;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("History");
        
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void ExitSettings()
    {
        SceneManager.LoadScene("Menu");

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
