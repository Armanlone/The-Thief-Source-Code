using UnityEngine;

public class VFXDisabler : MonoBehaviour
{
    //Function to be called in animation event to disable the gameObject of this component attached to.
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
