using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10;
    public float Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject); //destroys the bullet
        }
        else if (collision.CompareTag("Enemy"))
        {
            //damage enemy
            Vector2 normal = collision.transform.position - transform.position;
            normal = normal.normalized;
            normal *= Damage; // normal = normal * Damage
            collision.gameObject.SendMessage("TakeDamage", normal);

            Destroy(gameObject); //destroys the bullet
        }
    }
}
