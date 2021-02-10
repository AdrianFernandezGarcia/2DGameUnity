
[System.Serializable]
public class Match 
{

    public int MatchId { get; set; }

    public SerializableVector3 PlayerRespawnPos { get; set;}

    public int Zone { get; set; }
   
    public int PlayerPotionAmount { get; set; }

    public int PlayerAirJumpCount { get; set; }

    public int PlayerCurrentHealth { get; set; }

    public bool Boss1IsDead { get; set; }

    public bool Boss2IsDead { get; set; }

}
