//Copyright (c) 2015 Sebastian

//Raycast Controller + Platform Controller = Buzzsaw

using UnityEngine;

public class Buzzsaw : MonoBehaviour
{

    [SerializeField]
    private Vector3[] localWaypoints = null;
    private Vector3[] globalWaypoints;

    [Range(0, 10)]
    [SerializeField]
    private float speed = 5;
    [Range(0, 1)]
    [SerializeField]
    private float easingAmount = 0.5f;

    private int FWI;// ---------> From Waypoint Index
    private float PBW;// ---------> Percentage Between Waypoints
    private float nextMoveTime = 0;

    private void Start()
    {
        globalWaypoints = new Vector3[localWaypoints.Length];

        for (int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
    }

    private void Update()
    {

        if (GameManager.IsPause() || GameManager.IsRestart() || GameManager.IsPlayerDied() || GameManager.IsPlayerWin())
            return;

        Vector3 velocity = CalculateBuzzsawMovement();

        transform.Translate(velocity);
    }

    //For more information regarding the Ease, visit Sebastian Lague's 2D Platformer.
    private float Ease(float x)
    {
        float a = easingAmount + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    private Vector3 CalculateBuzzsawMovement()
    {
        if (Time.time < nextMoveTime)
        {
            return Vector3.zero;
        }

        FWI %= globalWaypoints.Length;
        int TWI = (FWI + 1) % globalWaypoints.Length;// ---------> To Waypoint Index

        float DBW = Vector3.Distance(globalWaypoints[FWI], globalWaypoints[TWI]);// ---------> Distance Between Waypoints

        PBW += Time.deltaTime * speed / DBW;
        PBW = Mathf.Clamp01(PBW);

        float EasedPBW = Ease(PBW);// ---------> Eased Percentage Between Waypoints

        Vector3 newPos = Vector3.Lerp(globalWaypoints[FWI], globalWaypoints[TWI], EasedPBW);

        if (PBW >= 1)
        {
            PBW = 0;
            FWI++;

            if (FWI >= globalWaypoints.Length - 1)
            {

                //Play the "SFX Buzzsaw" sound.
                AudioManager.PlaySound(11);

                FWI = 0;
                System.Array.Reverse(globalWaypoints);
            }
        }

        nextMoveTime = Time.time;

        return newPos - transform.position;
    }

    private void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.3f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 GWP = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;

                Gizmos.DrawLine(GWP - Vector3.up * size, GWP + Vector3.up * size);
                Gizmos.DrawLine(GWP - Vector3.left * size, GWP + Vector3.left * size);
            }
        }
    }
}
