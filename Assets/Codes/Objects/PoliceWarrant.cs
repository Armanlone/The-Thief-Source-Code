using UnityEngine;

public class PoliceWarrant : Coin
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //Reset the overall score to 0.
        GameManager.ResetScore();

        //It will instantiate shine at this position.
        Instantiate(shine, transform.position, Quaternion.identity);

        //Trigger in the warrant.
        UIManager.TriggerWarrant();

        //And it will be destroyed.
        Destroy(gameObject);

    }

}