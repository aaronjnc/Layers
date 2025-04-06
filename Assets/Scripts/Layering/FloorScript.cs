using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlatformEffector2D))]
public class FloorScript : MonoBehaviour
{
    private BoxCollider2D col;
    private PlatformEffector2D platform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        platform = GetComponent<PlatformEffector2D>();
    }

    private void Start()
    {
        PlayerMovement.Instance.Descending += PlayerDescending;
        if (PlayerMovement.Instance.GetPlayerLow() <= transform.position.y)
        {
            col.enabled = false;
        }
    }

    private void PlayerDescending(bool bDescending)
    {
        if (bDescending)
        {
            if (PlayerMovement.Instance.GetPlayerLow() >= transform.position.y)
            {
                col.enabled = true;
            }
        }
        else
        {
            if (PlayerMovement.Instance.GetPlayerLow() <= transform.position.y)
            {
                col.enabled = false;
            }
        }
    }
}
