using UnityEngine;

[RequireComponent(typeof(LayerTraveler))]
[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour
{
    [SerializeField]
    private Vector3 heldPosition = Vector3.zero;
    [SerializeField]
    private Vector3 heldRotation = Vector3.zero;

    private Rigidbody2D rb;

    public void Pickup()
    {
        rb.gravityScale = 0.0f;
        transform.localPosition = heldPosition;
        transform.localEulerAngles = heldRotation;
    }

    public void Drop()
    {
        rb.gravityScale = 1.0f;
    }

    public void Toss()
    {
        rb.gravityScale = 1.0f;
        rb.AddForce(new Vector3(500, 0, 0));
    }
}
