using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1;
    public float Damage = 1;
    public float Health = 5;

    float damagedTime;
    public float DamagedDuration = 1f;
    private Vector2 damagedNormal;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
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
