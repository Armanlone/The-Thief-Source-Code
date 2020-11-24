using UnityEngine;
using UIManagement;

public class UIManager : MonoBehaviour
{

    private static UIManager INSTANCE = null;

    private void Awake()
    {

        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;

        Debug.Log("UI Manager created.");

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        UITween.SetTweenCapacity();
    }

    private void Update()
    {

        //Press Space
        if (ControllerManager.GetSpaceDown() && UITween.IsMenuActive())
        {
            AudioManager.PlaySound(6);
            UITween.levelsActive(true);
            UITween.menuSlideOut();
            UITween.levelsSlideIn();
            UITween.menuActive(false);
        }

    }

    //Mutes/Unmutes sounds.
    public void ClickSounds()
    {

        AudioManager.MuteSound();
        UITween.imgSoundsChangeSprite(AudioManager.IsMute());

        Debug.Log("Is Muted: " + AudioManager.IsMute());
    }

    #region Transition

    //Slides out the rctfTransition + unfade the cnvgPanel.
    public static void OpenTransition(byte roomID)
    {

        if (INSTANCE == null)
            return;

        AudioManager.PlaySound(10);
        UITween.rctfTransitionSlide(500f, 0.1f);
        UITween.cnvgPanelFade(0, 1f);
        UITween.transitionActive(false);

        //If the roomID is for menu or credits, don't enable the game UI.
        if (roomID == RoomManager.GetMainMenuIndex() || roomID == RoomManager.GetCreditsIndex())
            return;

        else
            UITween.gameActive(true);

    }

    //Slides out the rctfTransition + unfade the cnvgPanel.
    public static void CloseTransition()
    {

        if (INSTANCE == null)
            return;

        UITween.transitionActive(true);
        UITween.cnvgPanelFade(1, 0.5f);
        AudioManager.PlaySound(9);
        UITween.rctfTransitionSlide(0, 0);
        UITween.gameActive(false);

    }

    #endregion

    #region Menu

    public void ClickMenuMiniMap()
    {

        AudioManager.PlaySound(7);
        UITween.optionsActive(true);
        UITween.optionsMenuActive(true);
        UITween.btnSoundsActive(true);

        UITween.helpSectionActive(false);
        UITween.optionsLevelsActive(false);
        UITween.optionsGameActive(false);
        UITween.warrantSectionActive(false);

        UITween.menuSlideOut();
        UITween.rctfMapSlide(0, 1);
        UITween.menuActive(false);

    }

    public static void CallClickMenuExit()
    {

        if (INSTANCE == null)
            return;

        else
            INSTANCE.ClickMenuExit(0);

    }

    public void ClickMenuExit(int soundID)
    {

        AudioManager.PlaySound(soundID);
        UITween.menuActive(true);
        UITween.rctfMapSlide(-500f, 0f);
        UITween.menuSlideIn();
        UITween.optionsActive(false);

    }

    public void ClickQuit()
    {

        AudioManager.PlaySound(8);
        Application.Quit();

        Debug.Log("Player quit.");

    }

    public void ClickHelp()
    {

        AudioManager.PlaySound(7);
        UITween.optionsMenuActive(false);
        UITween.btnSoundsActive(false);
        UITween.helpSectionActive(true);

    }

    public void ClickHelpExit()
    {

        AudioManager.PlaySound(8);
        UITween.helpSectionActive(false);
        UITween.optionsMenuActive(true);
        UITween.btnSoundsActive(true);

    }

    #endregion

    #region Levels

    public void ClickLevelsMiniMap()
    {

        AudioManager.PlaySound(7);
        UITween.optionsActive(true);
        UITween.optionsLevelsActive(true);
        UITween.btnSoundsActive(true);

        UITween.optionsMenuActive(false);
        UITween.helpSectionActive(false);
        UITween.optionsGameActive(false);
        UITween.warrantSectionActive(false);

        UITween.levelsSlideOut();
        UITween.rctfMapSlide(0, 1);
        UITween.levelsActive(false);

    }

    public void ClickLevelsExit()
    {

        AudioManager.PlaySound(8);
        UITween.levelsActive(true);
        UITween.rctfMapSlide(-500f, 0f);
        UITween.levelsSlideIn();
        UITween.optionsActive(false);

    }

    public void ClickMenu()
    {

        AudioManager.PlaySound(7);
        UITween.menuActive(true);
        UITween.rctfMapSlide(-500f, 0f);
        UITween.menuSlideIn();
        UITween.optionsActive(false);

    }

    //Loads the room based on roomID.
    public void ClickButtonRoom(int roomID)
    {

        AudioManager.PlaySound(7);
        UITween.gameActive(true);
        UITween.levelsSlideOut();
        UITween.gameSlideIn();
        UITween.levelsActive(false);
        RoomManager.LoadRoom(roomID);

    }

    #endregion

    #region Game

    public void ClickGameMiniMap()
    {

        AudioManager.PlaySound(7);
        UITween.optionsActive(true);
        UITween.optionsGameActive(true);
        UITween.btnSoundsActive(true);

        UITween.optionsMenuActive(false);
        UITween.helpSectionActive(false);
        UITween.optionsLevelsActive(false);
        UITween.warrantSectionActive(false);

        UITween.gameSlideOut();
        UITween.rctfMapSlide(0, 1);
        UITween.gameActive(false);
        GameManager.SetPlayerPause(true);

    }

    public void ClickGameExit(int soundID)
    {

        AudioManager.PlaySound(soundID);
        UITween.gameActive(true);
        UITween.rctfMapSlide(-500f, 0f);
        UITween.gameSlideIn();
        UITween.optionsActive(false);
        GameManager.SetPlayerPause(false);

    }

    public void ClickLeave()
    {

        AudioManager.PlaySound(7);
        SaveData.SaveGameData();
        RoomManager.LoadRoom(RoomManager.GetMainMenuIndex());
        UITween.levelsActive(true);
        UITween.rctfMapSlide(-500f, 0f);
        UITween.levelsSlideIn();
        UITween.optionsActive(false);

    }

    public void ClickRestart()
    {

        ClickGameExit(7);
        GameManager.SetPlayerRestart(true);
        RoomManager.LoadRoom(RoomManager.GetCurrentRoom(), 0.5f);

    }

    //Set the UI's txtScore.
    public static void UpdateTextScore(int score)
    {

        if (score < 0)
            return;

        else
            UITween.UpdateTextScore(score);
    }


    #endregion

    #region Warrant/Credits

    public static void TriggerWarrant()
    {

        UITween.optionsActive(true);
        UITween.warrantSectionActive(true);

        UITween.btnSoundsActive(false);
        UITween.optionsMenuActive(false);
        UITween.helpSectionActive(false);
        UITween.optionsLevelsActive(false);
        UITween.optionsGameActive(false);

        UITween.gameSlideOut();
        UITween.rctfMapSlide(0, 1);
        UITween.gameActive(false);
        GameManager.SetPlayerPause(true);

    }

    public void ClickWarrantExit()
    {

        //Slides the map down.
        AudioManager.PlaySound(8);
        GameManager.ResetScore();
        UITween.rctfMapSlide(-500f, 0);

        //Go to the credits scene.
        RoomManager.LoadRoom(RoomManager.GetCreditsIndex());
        UITween.optionsActive(false);
        UITween.LoadingCredits(2f);
        GameManager.SetPlayerPause(false);

    }

    #endregion

}

