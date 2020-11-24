using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Coin : MonoBehaviour
{

    [Header("Sine Wave")]
    [Range(0.1f, 1)] [SerializeField] private float frequency = 1f;
    [Range(0.1f, 1)] [SerializeField] private float amplitude = 1f;

    [Header("Pick-up Reward")]
    [SerializeField]
    private int pickupReward = 0;

    [Space]

    [Header("Shine Effect")]
    public GameObject shine = null;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
         VerticalSineWave();
    }

    //Use the Rigidbody2D's velocity to float in sine wave motion in its vertical axis.
    private void VerticalSineWave()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Sin(Time.time * frequency) * amplitude);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //If the coin collides with the Player then...
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        print("Coin collided with Player.");

        //Calls the game manager's score system to be updated.
        GameManager.PlayerLootCoin(pickupReward);

        //It will instantiate shine at this position.
        Instantiate(shine, transform.position, Quaternion.identity);

        //And it will be destroyed.
        Destroy(gameObject);

    }
}