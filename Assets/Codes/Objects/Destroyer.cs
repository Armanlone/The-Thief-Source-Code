using UnityEngine;

//This is responsible for destroying arrows on contact.
public class Destroyer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.IsTouchingLayers(LayerMask.NameToLayer("Trap")))
            Destroy(collision.gameObject);

    }

}
