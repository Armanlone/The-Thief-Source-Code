using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private bool goRight = true;

    [SerializeField]
    [Range(100f, 500f)]
    private float speed = 100f;

    private Vector2 direction;

    private Rigidbody2D rb = null;
    private Animator anim = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (goRight)
        {
            direction = Vector2.right;
        }

        else
        {
            direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime;
    }

    //If the arrow collided with a player or a wall then it will go down and it rotates.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Platform") || collision.CompareTag("Player"))
        {

            Debug.Log(collision.gameObject.name + " was hit.");

            direction = Vector2.down;

            anim.SetTrigger("rotate");

        }

    }

}
