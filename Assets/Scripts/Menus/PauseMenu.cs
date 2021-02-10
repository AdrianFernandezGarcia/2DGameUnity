using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused;
    public GameObject pauseMenuUI;
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button exitToMenuButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private GameObject pauseSettingsMenu;

    private void Awake()
    {
        
        resumeButton.onClick.AddListener(Resume);
        exitToMenuButton.onClick.AddListener(ExitToMenu);
        settingsButton.onClick.AddListener(OpenSettings);

        
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        

    }

    //aplicar un poco de tiempo de espera, ya que si no el personaje saltaría al pulsar el botón de aceptar en mando.
    public IEnumerator WaitForResume()
    {
        yield return new WaitForSeconds(.2f);
        DisableMenu();
        gamePaused = false;
        
    }

    private void Pause()
    {
        pauseMenuUI.GetComponent<MenuController>().enabled = true;
        pauseMenuUI.SetActive(true);
        gamePaused = true;
        
            
    }

    private void Resume()
    {
       
        StartCoroutine(WaitForResume());

    }



    private void ExitToMenu()
    {
        DisableMenu();
        gamePaused = false;
        SceneManager.LoadScene("Menu");
    }

    private void OpenSettings()
    {
        DisableMenu();
        pauseSettingsMenu.SetActive(true);
        pauseSettingsMenu.GetComponent<MenuController>().enabled = true ;
    }

    private void DisableMenu()
    {
        pauseMenuUI.GetComponent<MenuController>().enabled = false;
        pauseMenuUI.SetActive(false);
    }

    
}
