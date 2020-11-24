using UnityEngine;

public class Trap : MonoBehaviour
{

    //If the trap collided with the player then...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        //If the player is alive then kill it.
        if (!GameManager.IsPlayerDied())
        {

            //Play the "Player Hurt" sound.
            AudioManager.PlaySound(3);

            GameManager.PlayerDied();
        }

    }
}
