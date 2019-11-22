using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float Speed = 5f;
    [SerializeField] float Jumpforce = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(9, 8);
        Physics2D.IgnoreLayerCollision(9, 10);
    }

    void Update()
    {
        rb.velocity = new Vector2(TCKInput.GetAxis("Joystick", EAxisType.Horizontal) * Speed, rb.velocity.y); ;

        if (TCKInput.GetAction("jumpBtn", EActionEvent.Down))
            rb.AddForce(Vector2.up * Jumpforce, ForceMode2D.Impulse);
    }
}
