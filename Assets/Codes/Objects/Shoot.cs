using UnityEngine;

public class Shoot : MonoBehaviour
{

    [Header("Shooting Components:")]
    [SerializeField]
    private GameObject projectile = null;

    [Space]

    //Timers
    [SerializeField]
    private float shootingTime = 5f;
    private float currentTime = 0f;

    [Space]

    [SerializeField]
    private Vector2 spoutPos = Vector2.zero;

    //Initialize the two vectors, the current position plus the spout position.
    private void Start()
    {
        spoutPos += (Vector2)transform.position;
    }

    private void Update()
    {

        if (GameManager.IsPause() || GameManager.IsRestart() || GameManager.IsPlayerDied() || GameManager.IsPlayerWin())
            return;

        //Plays an animation depending on the current timer: Idle, Ready, or Shoot.
        if (TryGetComponent(out Animator anim))
        {
            int hashID = Animator.StringToHash("timer");
            anim.SetFloat(hashID, currentTime);
        }

        //If the timer is greater than or equal to the time for launching then create the projectile and reset timer back to 0.
        if (currentTime >= shootingTime)
        {
            Instantiate(projectile, spoutPos, Quaternion.identity);
            currentTime = 0f;
            AudioManager.PlaySound(12);
        }

        //Else, it will increment the timer based from the deltaTime.
        else
        {
            currentTime += Time.deltaTime;
        }
    }

}
