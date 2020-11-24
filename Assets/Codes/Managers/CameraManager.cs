using UnityEngine;

public class CameraManager: MonoBehaviour
{

    private static CameraManager INSTANCE;

    [SerializeField]
    private float power = 0.7f;

    [SerializeField]
    private float duration = 0.25f;

    [SerializeField]
    private float slowDownAmount = 0.25f;

	[SerializeField]
    private bool shouldShake = false;
    private float initialDuration = 0;

    private Transform cameraPos = null;
    private Vector3 startPos;

    private float origPower = -1f, origDuration = -1f, origSlowDownAmount = -1f;

    private void Awake()
	{
        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Main Camera created.");
    }

    private void Start()
    {
        cameraPos = this.transform;
        startPos = cameraPos.localPosition;
        initialDuration = duration;

        //Original variables to be used for resetting the Camera Shake variables.
        origPower = power;
        origDuration = duration;
        origSlowDownAmount = slowDownAmount;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cameraPos.localPosition = startPos + (Vector3) Random.insideUnitCircle * power;
                duration -= Time.deltaTime * slowDownAmount;
            }

            else
            {
                shouldShake = false;
                duration = initialDuration;
                cameraPos.localPosition = startPos;
            }
        }
    }

    //This will be set to true if the GameManager calls the method of PlayerDied.
    public static void CameraShake()
    {
		if (INSTANCE == null)
		{
			return;
		}

        INSTANCE.shouldShake = true;
    }

    //Setter for Camera Shake Variables.
    public static void SetShakeVariables(float power = 0.25f, float initDuration = 0.25f, float slowDownAmount = 0.25f)
    {
        if (INSTANCE == null)
        {
            return;
        }

        //Set the Camera Shake Variables
        INSTANCE.power = power;
        INSTANCE.initialDuration = initDuration;
        INSTANCE.slowDownAmount = slowDownAmount;

    }

    //Resets the Camera Shake Variables.
    public static void ResetShakeVariables()
    {

        if (INSTANCE == null)
        {
            return;
        }

        //Reset the Camera Shake Variables
        INSTANCE.power = INSTANCE.origPower;
        INSTANCE.initialDuration = INSTANCE.origDuration;
        INSTANCE.slowDownAmount = INSTANCE.origSlowDownAmount;

    }
}
