using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    private static ControllerManager INSTANCE = null;

    private void Awake()
    {
        
        if (INSTANCE != null && INSTANCE != this)
        {

            Destroy(gameObject);
            return;

        }

        INSTANCE = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Controller Manager created.");

    }

    #region Getter(s)

    //Returns true if player pressed space, else false.
    public static bool GetSpaceDown()
    {
        if (INSTANCE == null)
            return false;

        return Input.GetKeyDown(KeyCode.Space);
    }

    //Returns true if player pressed space || w || up arrow, else false.
    public static bool GetJumpDown()
    {

        if (INSTANCE == null)
            return false;

        return Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W);

    }

    //Returns true if player tapped space || w || up arrow, else false.
    public static bool GetJumpUp()
    {

        if (INSTANCE == null)
            return false;

        return Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W);

    }

    //Returns true if player pressed Z || W || Down, else false.
    public static bool GetFlashDown()
    {

        if (INSTANCE == null)
            return false;

        return Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

    }

    //Returns horizontal axis of player, else 0.
    public static float GetHorzAxis()
    {
        if (INSTANCE == null)
            return 0f;

        return Input.GetAxisRaw("Horizontal");
    }

    //Returns true if player pressed escape key, else false.
    public static bool GetEsc()
    {
        if (INSTANCE == null)
            return false;

        return Input.GetKeyDown(KeyCode.Escape);
    }

    #endregion


}
