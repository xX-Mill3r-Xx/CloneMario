using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    private bool isJumping;
    private bool isGround;

    public float speed;
    public float jump;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveDirection = Input.GetAxis("Horizontal");

        if (moveDirection > 0 && !isJumping)
        {
            anim.SetInteger("transicao", 1);
            rig.velocity = new Vector2(speed, rig.velocity.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
            if (Input.GetButton("Fire2"))
            {
                rig.velocity = new Vector2(speed * 2, rig.velocity.y);
                anim.SetInteger("transicao", 3);
            }

        }

        if (moveDirection < 0 && !isJumping)
        {
            anim.SetInteger("transicao", 1);
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            if (Input.GetButton("Fire2"))
            {
                rig.velocity = new Vector2(-speed * 2, rig.velocity.y);
                anim.SetInteger("transicao", 3);
            }
        }

        if (moveDirection == 0 && !isJumping)
        {
            anim.SetInteger("transicao", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround && !isJumping)
        {
            anim.SetInteger("transicao", 2);
            rig.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isJumping = true;
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 7)
        {
            isGround = true;
            isJumping = false;

        }
    }
}

