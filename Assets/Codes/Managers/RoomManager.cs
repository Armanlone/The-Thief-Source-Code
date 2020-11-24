using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    private static RoomManager INSTANCE = null;

    [SerializeField]
    private int maxRoom = 15;

    [SerializeField]
    private float duration = 4f;

    [SerializeField]
    private int menuIndex = 21;

    [SerializeField]
    private int creditsIndex = 22;

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;

        Debug.Log("Room Manager created.");

        DontDestroyOnLoad(gameObject);
    }

    //It loads the "Main Menu Scene" at start.
    private void Start()
    {
        SceneManager.LoadScene(GetMainMenuIndex());
        Debug.Log("Main Menu: " + GetMainMenuIndex());
    }

    //Responsible for calling the Coroutine.
    public static void LoadRoom(int roomID, float delay = 0)
    {
        if (INSTANCE == null)
            return;

        if (roomID < 0)
            return;

        INSTANCE.StartCoroutine(INSTANCE.LoadEnumRoom(roomID, delay));
    }

    //Wait the duration before loading the new room.
    //As well as handling transitions, resetting the Game Manager's variables and Audio Manager's fading in and out.
    private IEnumerator LoadEnumRoom(int roomID, float delay)
    {

        //Some delay before closing the transition.
        yield return new WaitForSeconds(delay);

        //Close Transition
        UIManager.CloseTransition();

        #region Audio Manager's Fade Out

        if (roomID == GetMainMenuIndex() || roomID == GetCreditsIndex())
        {

            if (AudioManager.IsPlaying(16))
                AudioManager.FadeOut(0.5f, 16);

            else
                AudioManager.FadeOut();

        }

        else
        {

            if (AudioManager.IsPlaying(15))
                AudioManager.FadeOut(0.5f, 15);

            else
                AudioManager.FadeOut(0.5f, 16);

        }

        #endregion

        yield return new WaitForSeconds(duration);

        SceneManager.LoadScene(roomID);

        GameManager.ResetBooleansAndUpdateScore();

        //Open Transition
        UIManager.OpenTransition((byte) roomID);

        #region Audio Manager's Fade In

        if (roomID == GetMainMenuIndex() || roomID == GetCreditsIndex())
            AudioManager.FadeIn(0.5f, 15);

        else
            AudioManager.FadeIn(0.5f, 16);

        #endregion

    }

    #region Setter(s) and getter(s)

    //Getter for INSTANCE.maxRoom
    public static int GetMaxRoom()
    {
        return INSTANCE.maxRoom;
    }

    //Getter for INSTANCE.currentRoom
    public static int GetCurrentRoom()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    //Getter for the index of Main Menu.
    public static int GetMainMenuIndex()
    {
        return INSTANCE.menuIndex;
    }
    
    //Getter for the index of Credits.
    public static int GetCreditsIndex()
    {
        return INSTANCE.creditsIndex;
    }

    #endregion
}
