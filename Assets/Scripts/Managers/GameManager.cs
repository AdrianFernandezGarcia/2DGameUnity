using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    private Transform lastFirePlace;
    private Vector3 startPoint;
    private Vector3 fallResetPoint;
    private Player player;
    public static GameManager instance;
    private Transform Zone1Spawn;
    private Transform Zone2Spawn;
    public bool loadMenu = true;
    //persistencia

    void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
            Destroy(gameObject);

        player = GameObject.Find("Player").GetComponent<Player>();
        startPoint = start.position;

    }

    public void InitGame()
    {
        loadMenu = false;
        player.gameObject.SetActive(true);
        player.InitUI();

    }




    void Update()
    {

        //menú de pausa
        if (PauseMenu.gamePaused)
        {
            player.enabled = false;
        }
        else
        {
            player.enabled = true;
        }


    }


    //se llama a este método cuando el jugador se cae pero no ha muerto. Reaparecerá en el útlimo punto de reaparición guardado.
    public void Respawn()
    {
        StartCoroutine(RestartGame());
    }

    //guarda el último punto de guardado
    public void CheckPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    public IEnumerator RestartGame()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(.5f);
        player.gameObject.SetActive(true);
        player.transform.position = fallResetPoint;
    }

    //al iniciar el juego de nuevo desde el menú de partidas
    public void ResetPlayer()
    {
        player.enabled = true;
        player.ResetCurrentHealth();
    }

    public void DisablePlayer()
    {
        player.enabled = false;

    }

    //cambiar la escena a la de la zona1
    public void GoZone1()
    {
        SceneManager.LoadScene("MainScene");
        player.transform.position = Zone1Spawn.position;


    }

    //cambiar la escena a la de la zona2
    public void GoZone2()
    {
        SceneManager.LoadScene("Zone2");


    }



    //GETTERS Y SETTERS

    public Vector3 GetStartPoint()
    {
        return startPoint;
    }

    public void SetLastCheckPoint(Vector3 startPoint)
    {
        this.startPoint = startPoint;
    }

    public void SetResetPoint(Vector3 fallResetPoint) => this.fallResetPoint = fallResetPoint;

    public void SetZone1Spawn(Transform transform)
    {
        this.Zone1Spawn = transform;

    }

    public void SetZone2Spawn(Transform transform)
    {
        this.Zone2Spawn = transform;

    }


    public int GetZone()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainScene"))
            return 1;


        else if (SceneManager.GetActiveScene().name.Equals("Zone2"))
            return 2;


        return 0;
    }
}