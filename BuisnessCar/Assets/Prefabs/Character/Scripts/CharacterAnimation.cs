using System.Collections;
using System.Collections.Generic;
using TouchControlsKit;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    Rigidbody2D rb;
    Animator Anim;
    Vector3 scale;

    [SerializeField] bool isWalking;
    [SerializeField] bool isFlying;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        scale = transform.localScale;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float horizontal = TCKInput.GetAxis("Joystick", EAxisType.Horizontal);
        if (horizontal < 0)
            transform.localScale = new Vector3(-scale.x, transform.localScale.y, transform.localScale.z);
        else if (horizontal > 0)
            transform.localScale = new Vector3(scale.x, transform.localScale.y, transform.localScale.z);

        if (horizontal != 0 && isWalking == false && !isFlying)
        {
            Anim.SetTrigger("4Walk");
            isWalking = true;
        }
        if (horizontal == 0 && isWalking == true)
        {
            Anim.SetTrigger("4Idle");
            isWalking = false;
        }

        if (TCKInput.GetAction("jumpBtn", EActionEvent.Down))
        {
            Anim.SetTrigger("4Jump");
            isFlying = true;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Road")
        {
            isWalking = false;
            isFlying = false;
            Anim.SetTrigger("4JumpLanding");
        }
    }
}
