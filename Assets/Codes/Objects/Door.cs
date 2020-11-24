
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]

public class Door : MonoBehaviour
{

    private Animator anim;

    private int winID;

    //The current scene's buildIndex of where the door is.
    [SerializeField] private int roomID = -1;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        winID = Animator.StringToHash("win");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (roomID == -1)
	    {
                return;
	    }

        //If the player reach goal then it wiil call the GameManager's PlayerEnterRoom
        // and pass the current roomID to know the current buildIndex of the room.
        if (!GameManager.IsPlayerWin() && !GameManager.IsPlayerDied())
        {
            GameManager.PlayerEnterRoom(roomID);

            //Trigger Win Animation.
            anim.SetTrigger(winID);

        }

    }
}