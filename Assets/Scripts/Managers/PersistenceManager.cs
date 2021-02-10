using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager instance;
    private Vector3 respawnPoint;
    private GameObject playerObj;
    private Player player;
    private PlayerJump jump;
    public Boss1 boss1;
    public Boss2 boss2;
    private bool boss1Dead;
    private bool boss2Dead;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
            Destroy(gameObject);

        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        jump = playerObj.GetComponent<PlayerJump>();
        boss1 = FindObjectOfType<Boss1>();
        boss2 = FindObjectOfType<Boss2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (boss1Dead)
        {
            boss1.setIsDead(true);
        }

        if (boss2Dead)
        {
            boss2.SetIsDead(true);

        }
    }

    public void LoadGame()
    {
        Match loadedMatch = Persistence.Instance.GetEntryData();
        //load the match that player´s selected from the match menu.
        player.SetCurrentHealth(loadedMatch.PlayerCurrentHealth);
        player.SetPotionAmount(loadedMatch.PlayerPotionAmount);
        jump.SetJumpCount(loadedMatch.PlayerAirJumpCount);
        playerObj.transform.position = loadedMatch.PlayerRespawnPos;
        PersistenceManager.instance.SetBoss1Dead(loadedMatch.Boss1IsDead);
        PersistenceManager.instance.SetBoss2Dead(loadedMatch.Boss2IsDead);



    }
    public void Load()
    {
        if (Persistence.Instance.GetEntryData() != null)
        {
            LoadGame();
            GameManager.instance.ResetPlayer();
        }

        else
            respawnPoint = GameManager.instance.GetStartPoint();
    }


    #region GETTERS & SETTERS
   
    public Vector3 GetResetPoint()
    {
        return respawnPoint;
    }

    public void SetBoss1Dead(bool bossDead)
    {
        this.boss1Dead = bossDead;
    }

    public void SetBoss2Dead(bool bossDead)
    {
        this.boss2Dead = bossDead;
    }

    #endregion

}
