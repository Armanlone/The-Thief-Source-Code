using UnityEngine;

//Class that handles the sounds produced by the Player.
public class PlayerSounds : MonoBehaviour
{

    //This triggers the "Player Walk" sound in AudioManager.
    public void PlayerWalk()
    {
        AudioManager.PlaySound(1);
    }

    //This triggers the "PlayerJump" sound in AudioManager.
    public void PlayerJump()
    {
        AudioManager.PlaySound(2);
    }

    //This triggers the "PlayerHurt" sound in AudioManager.
    public void PlayerHurt()
    {
        AudioManager.PlaySound(3);
    }
}
