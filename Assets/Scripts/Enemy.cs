using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    public float Damage = 1;
    //public float Health = 5.0f; //0 0.1 1000.596 100.0
    public Health Health;
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

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(Vector2 normalDamage)
    {
        float damage = normalDamage.magnitude; //pull out damage from normalDamage
        Health.Hurt(damage, gameObject);

        //if (Health <= 0) Destroy(gameObject);

        damagedTime = Time.time;
        damagedNormal = normalDamage;
    }

    private void FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    float attackStartTime;
    int attackSpree;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            attackStartTime = Time.time;
            attackSpree = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if(attackStartTime + attackSpree < Time.time)
            {
                collision.transform.SendMessage("TakeDamage", Damage);
                attackSpree += 1;
            }
        }
    }
}

[System.Serializable]
public class Health
{
    public GameObject HealthBar;
    private bool healthSetup;
    private float healthBarLength;
    public float CurrentHealth;
    public float MaxHealth;

    public void Hurt(float damage, GameObject gameObject)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        setHealthBar();
        if (CurrentHealth <= 0) gameObject.SendMessage("Die");
    }

    private void setHealthBar()
    {
        if (!healthSetup)
        {
            healthBarLength = HealthBar.transform.localScale.x;
            healthSetup = true;
        }
        float healthPercent = CurrentHealth / MaxHealth;
        HealthBar.transform.localScale = new Vector3( healthPercent * healthBarLength, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
        HealthBar.SetActive(true);
    }
}
