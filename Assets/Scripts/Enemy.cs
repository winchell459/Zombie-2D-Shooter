using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    public float Damage = 1;
    public float Health = 5.0f; //0 0.1 1000.596 100.0
    int myint = 5; //1 -70 1000000
    

    float damagedTime;
    public float DamagedDuration = 1f;
    private Vector2 damagedNormal;

    private Transform target;

    public Transform LeftGroundChecker, RightGroundChecker;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(target.position.x - transform.position.x < 0)//moving left
        {
            if(Physics2D.OverlapCircle(LeftGroundChecker.position, 0.01f, groundLayer))
            {
                rb.velocity = new Vector2(-Speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else //moving right
        {
            if (Physics2D.OverlapCircle(RightGroundChecker.position, 0.01f, groundLayer))
            {
                rb.velocity = new Vector2(Speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        
    }

    public void TakeDamage(Vector2 normalDamage)
    {
        float damage = normalDamage.magnitude; //pull out damage from normalDamage
        Health -= damage;

        if (Health <= 0) Destroy(gameObject);

        damagedTime = Time.time;
        damagedNormal = normalDamage;
    }

    private void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
