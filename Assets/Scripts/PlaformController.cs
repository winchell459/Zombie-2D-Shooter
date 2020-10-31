using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaformController : MonoBehaviour
{
    Rigidbody2D rb;
    float speed = 4; //float 9.0 or 6.7 ... decimal number
    float jumpForce = 6;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        
    }

    void Jump()
    {
        bool jumpButtonDown = Input.GetKeyDown(KeyCode.Space);
        if (jumpButtonDown)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
