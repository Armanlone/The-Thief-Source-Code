//Copyright (c) 2015 Sebastian

//Player +=  PlayerInput

using UnityEngine;

[RequireComponent(typeof(Platformer2D))]

[RequireComponent(typeof(FlashlightController))]

public class Player : MonoBehaviour
{

    [Header("Walk")]
    [Range(1, 10)] [SerializeField] private float speed = 6f;

    [Header("Jump")]
    [Range(1, 5)] [SerializeField] private float highJumpHeight = 4f;
    [Range(1, 2.5f)] [SerializeField] private float lowJumpHeight = 1f;
    [Range(0.1f, 1f)] [SerializeField] private float timeToJumpApex = 0.4f;

    [Header("Coyote Time")]
    [Range(0.1f, 1f)] [SerializeField] private float jumpTime = 0.2f;
    private float currentJumpTime = 0;
    [Range(0.1f, 1f)] [SerializeField] private float groundedTime = 0.25f;
    private float currentGroundedTime = 0;

    private float ATA = 0.2f;// ---------> Accelaration Time Airborne
    private float ATG = 0.1f;// ---------> Accelaration Time Grounded

    private float gravity;
    private float highJumpVelocity;
    private float lowJumpVelocity;

    private float HVD;// ---------> Horizontal Velocity Damping
    private Vector2 velocity;

    private Platformer2D platformer;

    private PlayerAnimations animations;

    private FlashlightController flashlight;

    private void Awake()
    {
        platformer = GetComponent<Platformer2D>();

        animations = GetComponent<PlayerAnimations>();

        flashlight = GetComponent<FlashlightController>();
    }

    private void Start()
    {
        CalculateJumpVelocity();
    }

    //Calculates the gravity, highJumpVelocity and the lowJumpVelocity of the Player.
    private void CalculateJumpVelocity()
    {
        gravity = -(2 * highJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        highJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        lowJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * lowJumpHeight);
    }

    private void Update()
    {

        //If the game is paused or restarted, don't do anything, just return.
        if (GameManager.IsPause() || GameManager.IsRestart())
            return;

        Vector2 input;

        if (!GameManager.IsPlayerDied())
        {
           input = new Vector2(ControllerManager.GetHorzAxis(), 0f);
        }

        else
        {
            input = Vector2.zero;
            animations.PlayerDieTrigger();
        }

        if (GameManager.IsPlayerWin())
        {
            //If player touch the door, disable it.
            gameObject.SetActive(false);
        }

        float acceleration = input.x * speed;
        velocity.x = CalcualateHorizontalVelocity(acceleration);

        //At every frame, the velocity.y of the player will increment by (gravity * Time.deltaTime).
        velocity.y += gravity * Time.deltaTime;

        //The Movement function of the platformer will be called every frame, even if the Player isn't moving.
        platformer.Movement(velocity * Time.deltaTime);

        //Functions to animate and flip the sprite of the Player.
        animations.xVelocityTrigger(velocity.x);
        animations.yVelocityTrigger(velocity.y);
        animations.FlipSprite(platformer.getCollisions().direction);

        //If the player is airborne or on the ground then the velocity.y is 0.
        if (platformer.getCollisions().above || platformer.getCollisions().below)
        {
            velocity.y = 0;
        }

        //Every frame, the currentGroundedTime will decrease.
        currentGroundedTime -= Time.deltaTime;

        //If Player is on the ground then currentGroundedTime will initialize to groundedTime.
        if (platformer.getCollisions().below)
        {
            currentGroundedTime = groundedTime;
            animations.onGroundTrigger(true);
        }

        //Every frame, the currentJumpTime will decrease.
        currentJumpTime -= Time.deltaTime;

        //If Player press "Jump" then the currentJumpTime will be initialize to jumpTime.
        if (ControllerManager.GetJumpDown() && !GameManager.IsPlayerDied() && !GameManager.IsPlayerWin() && !GameManager.IsRestart())
        {
            currentJumpTime = jumpTime;
        }

        //If the currentJumpTime is greater than 0 (it was press before) and currentGroundedTime is also greater than 0 (the Player was on the ground before) then Player will JUMP HIGH, and the currentJumpTime and currentGroundedTime will become 0.
        if (currentJumpTime > 0)
        {
            if (currentGroundedTime > 0)
            {
                velocity.y = highJumpVelocity;
                currentGroundedTime = 0;

                animations.onGroundTrigger(false);
            }

            currentJumpTime = 0;
        }

        //If Player press "Jump" once and the velocity.y is greater than the lowJumpVelocity then the Player will JUMP LOW.
        if (ControllerManager.GetJumpUp() && !GameManager.IsPlayerDied() && !GameManager.IsPlayerWin())
        {
            if (velocity.y > lowJumpVelocity)
            {
                velocity.y = lowJumpVelocity;

                animations.onGroundTrigger(false);
            }
        }

        //If not dead, call the FlashlightSwitch() function to enable switching gameObject with Sprite Mask.
        if (!GameManager.IsPlayerDied())
        {
            flashlight.FlashlightSwitch(platformer.getCollisions().direction);
        }

        //Calls the player's idle regarding the flashlight and what its direction is pointed to.
        animations.flashlightTrigger(flashlight.getFlashlightIndex());
    }

    //Calculates the Horizontal Velocity of the Player and returns the damped velocity;
    private float CalcualateHorizontalVelocity(float acceleration)
    {
        return Mathf.SmoothDamp(velocity.x, acceleration, ref HVD, (platformer.getCollisions().below) ? ATG : ATA);
    }
}