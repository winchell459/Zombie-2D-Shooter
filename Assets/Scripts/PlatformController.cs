using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 4; //float 9.0 or 6.7 ... decimal number
    [SerializeField] float jumpForce = 6;

    bool isGrounded = false;
    public Transform[] isGroundCheckers;
    private float checkGroundRadius = 0.05f;
    public LayerMask groundLayer;


    public int Health = 30;
    private int maxHealth;
    public Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = Health;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
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
        if (jumpButtonDown && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = CheckForCollision(isGroundCheckers, groundLayer);
    }

    bool CheckForCollision(Vector2 checkerPos, LayerMask mask)
    {
        if(Physics2D.OverlapCircle(checkerPos, checkGroundRadius, mask) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool CheckForCollision(Transform[] checkers, LayerMask mask)
    {
        
        foreach(Transform checker in checkers)
        {
            if(CheckForCollision(checker.position, mask))
            {
                return true;
            }
        }
        return false;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        HealthText.text = Health.ToString();
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int health)
    {
        Health += health;
        Health = Mathf.Clamp(Health, 0, maxHealth);
    }
}
