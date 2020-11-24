//Copyright (c) 2015 Sebastian

//Raycast Controller + Controller2D = Platformer2D

using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Platformer2D : MonoBehaviour
{

    [SerializeField] private LayerMask platformMask = 0;

    private BoxCollider2D box;

    private const float border = 0.015f;
    private const float DBR = 0.25f;// ---------> Distance Between Raycasts

    private int horizontalRayCount;
    private int verticalRayCount;

    private float horizontalRaySpacing;
    private float verticalRaySpacing;

    private RaycastCollisions collisions;
    private RaycastPoints points;

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        CalculateRaycastSpacing();
        collisions.direction = 1;
    }

    //For moving the GameObject in both horizontal directions, jumping and gravity.
    public void Movement(Vector2 velocity)
    {
        UpdateRaycastPoints();
        collisions.Reset();

        if (velocity.x != 0)
        {
            collisions.direction = (int)Mathf.Sign(velocity.x);
        }

        HorizontalCollisions(ref velocity);

        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    //Checks if the player press "Right" then collision is right, otherwise if player press "Left" then collision is left.
    private void HorizontalCollisions(ref Vector2 velocity)
    {
        float dirX = collisions.direction;
        float rayLength = Mathf.Abs(velocity.x) + border;

        if (Mathf.Abs(velocity.x) < border)
        {
            rayLength = 2 * border;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (dirX == -1) ? points.bottomLeft : points.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * dirX, rayLength, platformMask);
            Debug.DrawRay(rayOrigin, Vector2.right * dirX, Color.red);

            if (hit)
            {
                if (hit.distance == 0)
                {
                    continue;
                }

                velocity.x = (hit.distance - border) * dirX;
                rayLength = hit.distance;

                collisions.left = dirX == -1;
                collisions.right = dirX == 1;
            }
        }
    }

    //Checks if the player is on platformMask, or if player is colliding on "Jump Through Platform".
    private void VerticalCollisions(ref Vector2 velocity)
    {
        float dirY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + border;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (dirY == -1) ? points.bottomLeft : points.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * dirY, rayLength, platformMask);
            Debug.DrawRay(rayOrigin, Vector2.up * dirY, Color.red);

            if (hit)
            {
                if (hit.collider.CompareTag("Jump Through Platform"))
                {
                    if (dirY == 1 || hit.distance == 0)
                    {
                        continue;
                    }
                }

                velocity.y = (hit.distance - border) * dirY;
                rayLength = hit.distance;

                collisions.below = dirY == -1;
                collisions.above = dirY == 1;
            }

        }
    }

    //Calculates the raycasts' spacing for the horizontal and vertical raycasts.
    private void CalculateRaycastSpacing()
    {
        Bounds bounds = box.bounds;
        bounds.Expand(border * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / DBR);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / DBR);

        horizontalRaySpacing = boundsHeight / (horizontalRayCount - 1);
        verticalRaySpacing = boundsWidth / (verticalRayCount - 1);
    }

    //Updates the RaycastPoints every frame.
    private void UpdateRaycastPoints()
    {
        Bounds bounds = box.bounds;
        bounds.Expand(border * -2);

        points.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        points.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        points.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        points.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    //The point of origins of the raycasts on Bounds.
    private struct RaycastPoints
    {
        public Vector2 topLeft, bottomLeft, topRight, bottomRight;
    }

    //A collection of the GameObjects collisions.
    public struct RaycastCollisions
    {
        public bool above, below, left, right;
        public int direction;

        //For resetting the collisions: above, below, left, right.
        public void Reset()
        {
            above = below = left = right = false;
        }
    }

    //Getter for the RaycastCollisions of the GameObject.
    public RaycastCollisions getCollisions()
    {
        return collisions;
    }
}