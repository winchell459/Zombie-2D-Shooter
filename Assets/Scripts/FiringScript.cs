using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Camera MainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 mouseWorldPos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos + " " + mouseWorldPos);

            Vector2 fireDirection = (mouseWorldPos - (Vector2)transform.position).normalized;


            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.up = fireDirection;
            bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bullet.GetComponent<Bullet>().Speed;
            Destroy(bullet, 5);
        }
        

    }
}
