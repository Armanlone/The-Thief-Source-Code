using UnityEngine;

[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimations : MonoBehaviour
{
    
    private int xVelocityID;
    private int yVelocityID;
    private int onGroundID;
    private int flashlightID;
    private int playerDieID;

    private Animator anim;

    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        xVelocityID = Animator.StringToHash("xVelocity");
        yVelocityID = Animator.StringToHash("yVelocity");
        onGroundID = Animator.StringToHash("onGround");
        flashlightID = Animator.StringToHash("flashlight");
        playerDieID = Animator.StringToHash("playerDie");
    }

    //This triggers the Animation Parameter: xVelocity of the Animation of the Player.
    public void xVelocityTrigger(float xVelocity)
    {
        anim.SetFloat(xVelocityID, Mathf.Abs(xVelocity));
    }

    //This triggers the Animation Parameter: yVelocity of the Animation of the Player.
    public void yVelocityTrigger(float yVelocity)
    {
        anim.SetFloat(yVelocityID, yVelocity);
    }

    //This triggers the Animation Parameter: onGround of the Animation of the Player.
    public void onGroundTrigger(bool onGround)
    {
        anim.SetBool(onGroundID, onGround);
    }

    //This triggers the Animation Parameter: flashlight of the Animation of the Player.
    public void flashlightTrigger(int flashlight)
    {
        anim.SetInteger(flashlightID, flashlight);
    }

    //This triggers the Animation Parameter: playerDie of the Animation of the Player.
    public void PlayerDieTrigger()
    {
        anim.SetTrigger(playerDieID);
    }

    //Use to flip the sprite of the Player is facing right(1) or Player is facing left(-1).
    public void FlipSprite(int direction)
    {
        sr.flipX = direction == -1 ? true : false;
    }
   
}