using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MoveTo : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;
    private Collider2D col;

    private bool bMovingToLocation = false;
    private Vector3 goalLocation = Vector3.zero;
    private float moveAcceptanceRadius = 0.0f;
    private Action callbackAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    public void MoveToLocation(Vector3 location, bool bIgnoreCollision, float acceptanceRadius, Action callback)
    {
        MoveToLocation(location, bIgnoreCollision, false, acceptanceRadius, callback);
    }

    public void MoveToLocation(Vector3 location, bool bIgnoreCollision, bool bIgnoreGravity, float acceptanceRadius, Action callback)
    {
        col.enabled = !bIgnoreCollision;
        if (bIgnoreGravity)
            rb.gravityScale = 0.0f;
        goalLocation = location;
        moveAcceptanceRadius = acceptanceRadius;
        callbackAction = callback;
        bMovingToLocation = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bMovingToLocation)
        {
            if (Vector3.Distance(transform.position, goalLocation) <= moveAcceptanceRadius)
            {
                transform.position = goalLocation;
                bMovingToLocation = false;
                rb.gravityScale = 1.0f;
                col.enabled = true;
                callbackAction?.Invoke();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, goalLocation, speed * Time.deltaTime);
            }
        }
    }
}
