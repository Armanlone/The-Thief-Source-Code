using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager INSTANCE = null;

    private int loot = 0;
    private static int collectedReward = 0;

    private bool playerDie = false, playerWin = false, playerPause = false, playerRestart = false;

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;

        Debug.Log("Game Manager created.");

        DontDestroyOnLoad(gameObject);
    }

    #region Game Logics

    //It will add the reward of the looted coin to the score of the thief.
    public static void PlayerLootCoin(int coinReward)
    {

        if (INSTANCE == null)
            return;

        collectedReward += coinReward;

        //Update text score UI.
        UIManager.UpdateTextScore(INSTANCE.loot + collectedReward);

        //PLay the "Player Pickup" sound.
        AudioManager.PlaySound(4);

        Debug.Log("Current Score: " + (INSTANCE.loot + collectedReward));

    }

    //The player will go to the next room/level.
    public static void PlayerEnterRoom(int roomID)
    {

        if (INSTANCE == null)
            return;

        if (roomID + 1 > RoomManager.GetMaxRoom())
            return;

        if (roomID < 0)
            return;

        INSTANCE.playerWin = true;

        //If the loot score is greater than or equal to 99999, then just set it 99999.
        if (INSTANCE.loot >= 99999)
        {
            INSTANCE.loot = 99999;
        }

        //Else, it will add the new collected rewards.
        else
        {
            INSTANCE.loot += collectedReward;
        }

        //PLay the "SFX Door" sound.
        AudioManager.PlaySound(13);
        
        Debug.Log("Current Score: " + INSTANCE.loot);

        RoomManager.LoadRoom(roomID + 1);

        Debug.Log("Current Level: " + RoomManager.GetCurrentRoom());

    }

    //Kills the player and restarts the level.
    public static void PlayerDied()
    {
        if (INSTANCE == null)
            return;

        //Camera Shake
        CameraManager.SetShakeVariables(0.15f, 0.25f, 0.25f);
        CameraManager.CameraShake();

        INSTANCE.playerDie = true;

        RoomManager.LoadRoom(RoomManager.GetCurrentRoom(), 0.5f);
    }

    //Reset booleans to false and updates the text score UI.
    public static void ResetBooleansAndUpdateScore()
    {

        if (INSTANCE == null)
            return;

        //Reset both booleans to false.
        INSTANCE.playerWin = INSTANCE.playerDie = INSTANCE.playerRestart = INSTANCE.playerPause = false;

        //Reset the collected reward in the current room to 0.
        collectedReward = 0;

        //Update the text score UI.
        UIManager.UpdateTextScore(INSTANCE.loot);
    }

    //Reset overall score of the player.
    public static void ResetScore()
    {

        if (INSTANCE == null)
            return;

        //Both temporary and saved score are equalized to 0.
        INSTANCE.loot = collectedReward = 0;

        //Then save the score data.
        SaveData.SaveGameData();

        //Update the UI Manager's txtScore to 0.
        UIManager.UpdateTextScore(0);

        //PLay the "Player Pickup" sound.
        AudioManager.PlaySound(4);

        Debug.Log("Current Score: " + (INSTANCE.loot + collectedReward));

    }

    #endregion

    #region Setter and getters.

    //Setter INSTANCE.loot.
    public static void SetLoot(int savedLoot)
    {
        if (savedLoot < 0)
            return;

        INSTANCE.loot = savedLoot;
    }

    //Getter INSTANCE.loot.
    public static int GetLoot()
    {
        return INSTANCE.loot;
    }

    //Getter for playerDied.
    public static bool IsPlayerDied()
    {
        return INSTANCE.playerDie;
    }

    //Getter for playerWin.
    public static bool IsPlayerWin()
    {
        return INSTANCE.playerWin;
    }

    //Setter INSTANCE.playerPause.
    public static void SetPlayerPause(bool playerPause)
    {
        if (INSTANCE == null)
            return;

        INSTANCE.playerPause = playerPause;
    }

    //Getter INSTANCE.playerPause.
    public static bool IsPause()
    {
        return INSTANCE.playerPause;
    }
    
    //Setter INSTANCE.playerRestart.
    public static void SetPlayerRestart(bool playerRestart)
    {
        if (INSTANCE == null)
            return;

        INSTANCE.playerRestart = playerRestart;
    }

    //Getter INSTANCE.playerRestart.
    public static bool IsRestart()
    {
        return INSTANCE.playerRestart;
    }

    #endregion
}