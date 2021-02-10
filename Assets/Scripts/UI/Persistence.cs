using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;

public class Persistence : MonoBehaviour
{

    public static Persistence instance;
    private readonly float autosaveTime = 30f;
    private readonly int matchSlotNumber = 5;
    private string SavePath => $"{Application.persistentDataPath}/GameData.bin";
    private FileStream fileStream;
    private BinaryFormatter formatter;
    private static Match loadedMatch;
    private List<Match> matchList;
    private int lastId;
    public bool newMatch;
    private GameObject playerObj;
    private Player player;
    private PlayerJump jump;

    private void Awake()
    {
        formatter = new BinaryFormatter();
        matchList = GetSavedMatches();
        lastId = matchList.Count - 1;
    }

    private void Update()
    {
        StartCoroutine(AutoSaveMatch(autosaveTime));
    }

    //Singleton pattern
    public static Persistence Instance
    {
        get { return instance != null ? instance : (instance = new GameObject("Persistence").AddComponent<Persistence>()); }
    }



    public void SaveGame()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();
        jump = playerObj.GetComponent<PlayerJump>();
        //if a match`s been loaded: overwrite it
        if (loadedMatch != null)
        {
            //guardar nuevos datos
            loadedMatch.Zone = GameManager.instance.GetZone();
            loadedMatch.PlayerCurrentHealth = player.GetCurrentHealth();
            loadedMatch.PlayerPotionAmount = player.GetPotionAmount();
            loadedMatch.PlayerAirJumpCount = jump.GetJumpCount();
            //respawn position
            loadedMatch.PlayerRespawnPos = PersistenceManager.instance.GetResetPoint();


            if (loadedMatch.Zone == 1)
                loadedMatch.Boss1IsDead = PersistenceManager.instance.boss1.getIsDead();
            else
                loadedMatch.Boss2IsDead = PersistenceManager.instance.boss2.isDead;


            //overwrite
            matchList.RemoveAt(loadedMatch.MatchId);
            matchList.Insert(loadedMatch.MatchId, loadedMatch);

        }
        //if it´s a new match:create a new one
        else
        {
            if (CheckIfRoom(matchSlotNumber))
            {
                Match newMatch = new Match
                {
                    MatchId = lastId + 1,
                    Zone = GameManager.instance.GetZone(),
                    PlayerCurrentHealth = player.GetCurrentHealth(),
                    PlayerPotionAmount = player.GetPotionAmount(),
                    PlayerAirJumpCount = jump.GetJumpCount(),
                    PlayerRespawnPos = GameManager.instance.GetStartPoint(),
                    Boss1IsDead = PersistenceManager.instance.boss1.getIsDead(),
                };
                //asignar variable
                loadedMatch = newMatch;
                //añadir a la lista de partidas
                matchList.Add(newMatch);
                //update lastId variable
                UpdateLastID();
            }

        }



        //write the changes into the file
        SaveListToFile(matchList);

    }

    /// <summary>
    /// Saves the list that contains all the matches to the binary file
    /// </summary>
    /// <param name="entryData"></param>
    public void SaveListToFile(List<Match> entryData)
    {
        fileStream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
        formatter.Serialize(fileStream, entryData);
        fileStream.Flush();
        fileStream.Close();
        fileStream.Dispose();
    }

    public List<Match> GetSavedMatches()
    {
        fileStream = new FileStream(SavePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
        try
        {
            object obj = formatter.Deserialize(fileStream);
            List<Match> matches = (List<Match>)obj;
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
            return matches;
        }
        catch (SerializationException)
        {
            //return an empty list if there are no matches saved
            return new List<Match>();
        }


    }

    private bool CheckIfRoom(int slotNumber)
    {
        if (matchList.Count > slotNumber)
            return false;

        return true;
    }

    private void UpdateLastID()
    {
        lastId = matchList.Count - 1;


    }

    private IEnumerator AutoSaveMatch(float autosaveTime)
    {
        yield return new WaitForSeconds(autosaveTime);
        SaveGame();
    }

    //GETTERS & SETTERS
    public void SetEntryData(int index) => loadedMatch = GetSavedMatches()[index];

    public Match GetEntryData()
    {
        return loadedMatch;
    }
}
