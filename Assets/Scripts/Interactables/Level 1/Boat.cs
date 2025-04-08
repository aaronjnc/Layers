using UnityEngine;
using UnityEngine.InputSystem;

public class Boat : DropLayers
{
    [SerializeField]
    private float boatMinPos = 3.0f;
    [SerializeField]
    private float boatAcceptanceRadius = 0.5f;
    private Vector3 boatStartPosition = Vector3.zero;

    private bool bMoving = false;

    private bool bReturning = false;

    [SerializeField]
    private float speed;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        boatStartPosition = transform.position;
    }

    protected override void StopMoveToDescent()
    {
        base.StopMoveToDescent();
        gameObject.transform.SetParent(PlayerMovement.Instance.gameObject.transform, true);
        bMoving = true;
    }

    private void FixedUpdate()
    {
        if (bMoving && transform.position.y <= boatMinPos)
        {
            gameObject.transform.SetParent(null);
            bMoving = false;
            bReturning = true;
        }
        else if (bReturning)
        {
            transform.position = Vector3.Lerp(transform.position, boatStartPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, boatStartPosition) < boatAcceptanceRadius)
            {
                bReturning = false;
                transform.position = boatStartPosition;
                bCanDescend = true;
            }
        }
    }

    public override void StopDescent()
    {
        foreach (Collider2D col in disableColliders)
        {
            col.enabled = true;
        }
    }
}
