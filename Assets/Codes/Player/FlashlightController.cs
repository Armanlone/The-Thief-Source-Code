using UnityEngine;

public class FlashlightController : MonoBehaviour
{
	[SerializeField] private GameObject flashlightU = null, flashlightD = null, flashlightL = null, flashlightR = null;

    private int index = 0;

    private void Start()
    {
        flashlightU.SetActive(false);
        flashlightD.SetActive(false);
        flashlightL.SetActive(false);
        flashlightR.SetActive(true);
    }

    private void Update()
    {

        //If the game is paused, don't do anything, just return.
        if (GameManager.IsPause())
            return;

        //If player pressed Z and the player isn't dead, then change flashlight facing direction.
        if (ControllerManager.GetFlashDown() && !GameManager.IsPlayerDied())
        {

            //PLay the "Player Flashlight" sound.
            AudioManager.PlaySound(5);

            if(index < 3)
            {
                ++index;
            }

            if (index == 3)
            {
                index = 0;
            }

        }
    }

    //Returns the current flashlight index.
    public int getFlashlightIndex()
    {
        return index;
    }

    //Switches the direction of the player depending on the counter.
    public void FlashlightSwitch(int direction)
	{
        switch (index)
        {
            case 0:// Either FlashlightR or FlashlightL enabled.

                //FlashlightR enabled.
                if (direction == 1)
                {
                    flashlightR.SetActive(true);

                    flashlightU.SetActive(false);
                    flashlightD.SetActive(false);
                    flashlightL.SetActive(false);
                }

                //FlashlightL enabled.
                else if (direction == -1)
                {
                    flashlightL.SetActive(true);

                    flashlightU.SetActive(false);
                    flashlightD.SetActive(false);
                    flashlightR.SetActive(false);
                }

                break;

            case 1: //FlashlightU enabled.

                flashlightU.SetActive(true);

                flashlightD.SetActive(false);
                flashlightL.SetActive(false);
                flashlightR.SetActive(false);

                break;

            case 2: //FlashlightD enabled.

                flashlightD.SetActive(true);

                flashlightU.SetActive(false);
                flashlightL.SetActive(false);
                flashlightR.SetActive(false);

                break;

        }
    }
}
