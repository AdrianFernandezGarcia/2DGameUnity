using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private Text [] text= new Text[5];
    [SerializeField]
    private Button [] matchButtons= new Button[5];
    [SerializeField]
    private Button[] deleteMatchButtons= new Button[5];
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;
    [SerializeField]
    private MenuController controller;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite zone1Sprite;
    [SerializeField]
    private Sprite zone2Sprite;
    [SerializeField]
    private Canvas deleteConfirmationCanvas;
    private List<Match> savedMatches;
    private bool playerReactioned = false;
    private bool deleteConfirmed;
    private bool continued=false;

    void Awake()
    {

        foreach (Button deleteButton in deleteMatchButtons)
        {
            deleteButton.gameObject.SetActive(false);
        }
        savedMatches = Persistence.Instance.GetSavedMatches();
        yesButton.onClick.AddListener(DeleteConfirmed);
        noButton.onClick.AddListener(DeleteRejected);
        UpdateMatchBoard();

    }


    private void Update()
    {
        savedMatches = Persistence.Instance.GetSavedMatches();

        if (Input.GetButtonDown("Cancel") )
        {
            SceneManager.LoadScene("Menu");
        }
    }


    private void UpdateMatchBoard()
    {

        for (int i = savedMatches.Count; i < matchButtons.Length; i++)
        {
            matchButtons[i].onClick.AddListener(NewGame);
        }



        foreach (Match match in savedMatches)
        {

            if (match != null)
            {
                text[match.MatchId].text = "Vida restante: " + match.PlayerCurrentHealth + "\n \n Pociones: " + match.PlayerPotionAmount;
                text[match.MatchId].alignment = TextAnchor.MiddleLeft;
                matchButtons[match.MatchId].onClick.AddListener(LoadGame);
                deleteMatchButtons[match.MatchId].gameObject.SetActive(true);
                deleteMatchButtons[match.MatchId].onClick.AddListener(DeleteMatch);

                //attach zone 1 style
                if (match.Zone == 1)
                {
                    matchButtons[match.MatchId].image.sprite = zone1Sprite;
                    ColorBlock cb = matchButtons[match.MatchId].colors;
                    cb.normalColor = Color.white;
                    cb.selectedColor = Color.white;
                    matchButtons[match.MatchId].colors = cb;
                }

                //attach zone 2 style
                else if (match.Zone == 2)
                {
                    matchButtons[match.MatchId].image.sprite = zone2Sprite;
                    ColorBlock cb = matchButtons[match.MatchId].colors;
                    cb.normalColor = Color.white;
                    cb.selectedColor = Color.white;
                    matchButtons[match.MatchId].colors = cb;
                }


            }

        }


    }

    /// <summary>
    /// Delete Match Buttons´ listener
    /// </summary>
    async void DeleteMatch()
    {
        for (int i = 0; i < deleteMatchButtons.Length; i++)
        {
            if (deleteMatchButtons[i].gameObject.name.Equals(EventSystem.current.currentSelectedGameObject.name))
            {
                deleteConfirmationCanvas.gameObject.SetActive(true);
                controller.enabled = false;
                await new WaitUntil(() => playerReactioned == true);
                if (deleteConfirmed)
                {
                    savedMatches.RemoveAt(i);
                    Persistence.Instance.SaveListToFile(savedMatches);
                    //refresh scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //reiniciar el controlador del menú(mando)
                    
                }

                deleteConfirmationCanvas.gameObject.SetActive(false);
                controller.enabled = true;
                playerReactioned = false;
                deleteConfirmed = false;

            }
            

        }
    }



    public void DeleteConfirmed() {
        deleteConfirmed = true;
        playerReactioned = true;
    } 


    public void DeleteRejected() => playerReactioned = true;
    
    /// <summary>
    /// Load Match Buttons´ listener
    /// </summary>
    public async void LoadGame()
    {

        for (int i = 0;i< matchButtons.Length;i++){
            if (matchButtons[i].gameObject.name.Equals(EventSystem.current.currentSelectedGameObject.name))
            {
                Persistence.Instance.SetEntryData(i);
                if(savedMatches[i].PlayerRespawnPos.x < 194.4f && savedMatches[i].PlayerRespawnPos.y >= -24.484f)
                {
                    SceneManager.LoadScene("MainScene");
                    
                }
                else
                    SceneManager.LoadScene("Zone2");

                await new WaitUntil(() => PersistenceManager.instance != null);
                PersistenceManager.instance.Load();
                GameManager.instance.InitGame();
                return;
            }
        }




    }

    /// <summary>New Game Buttons´ listener </summary>
    public async void NewGame()
    {
        SceneManager.LoadScene("MainScene");
        await new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.InitGame();
        GameManager.instance.ResetPlayer();

    }
}
