using UnityEngine;

public class SaveData : MonoBehaviour
{

    private SaveData INSTANCE = null;

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;
        Debug.Log("Save data created.");

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GameManager.SetLoot(PlayerPrefs.GetInt("score", 0));
        Debug.Log("Score: " + GameManager.GetLoot());
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public static void SaveGameData()
    {
        PlayerPrefs.SetInt("score", GameManager.GetLoot());
        Debug.Log("Score: " + GameManager.GetLoot());
        Debug.Log("Game Saved");
    }

}
