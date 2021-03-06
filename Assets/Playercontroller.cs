﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public static Playercontroller player;
    Rigidbody2D rb;
    public Transform[] raypoints;
    public bool grounded;
    public float movespeed, jumpamount;
    public Collider2D playercollider;
    public int supressGroundedTicks = 0;
    int coyote = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector2.up * jumpamount, ForceMode2D.Impulse);
            Debug.Log("jumped");
           
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.AddForce(new Vector2(0, -rb.velocity.y / 2), ForceMode2D.Impulse);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = 5;
        }
        if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0 && grounded)
        {
            rb.AddForce(new Vector2(0, rb.velocity.y / 10), ForceMode2D.Force);
        }
    }
    private void FixedUpdate()
    {
        GetGrounded();

        float _y = rb.velocity.y;
        if (grounded && Mathf.Abs(rb.velocity.x) < movespeed * 1.1f)
        {
            rb.velocity = Vector2.zero;
            if (Input.GetKey(KeyCode.D))
            {

                rb.velocity = Vector2.right * movespeed;

            }
            if (Input.GetKey(KeyCode.A))
            {


                rb.velocity = Vector2.left * movespeed;
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.D) && rb.velocity.x < movespeed)
            {
                rb.velocity += Vector2.right * movespeed / 2;

            }
            if (Input.GetKey(KeyCode.A) && rb.velocity.x > -1 * movespeed)
            {
                rb.velocity += Vector2.left * movespeed / 2;
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                //rb.AddForce(new Vector2(rb.velocity.x, 0f)/3);
            }
        }
        rb.velocity = new Vector2(rb.velocity.x, _y);
    }
    void GetGrounded()
    {
        LayerMask layerMask;
        layerMask = LayerMask.GetMask("Default");
        coyote--;
        foreach (Transform transforms in raypoints)
        {
            Debug.DrawRay(transforms.position, Vector2.down * 0.3f, Color.red, .03f);
            RaycastHit2D _raycasthit = Physics2D.Raycast(transforms.position, Vector2.down, .2f, layerMask: layerMask);
            if (_raycasthit.collider != playercollider && _raycasthit.collider != null)
            {
                coyote = 5;
                break;
            }
        }
        if (coyote > 0)
        {
            grounded = true;
            rb.gravityScale = 3;
        }
        else
        {
            grounded = false;
        }
        if (supressGroundedTicks > 0)
        {
            supressGroundedTicks--;
            grounded = false;
        }
    }
}
