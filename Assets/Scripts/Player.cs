using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float turnSpeed;
    private bool isMoving;

    public GameObject gun;
    private Vector3 aimDirection;
    
    public GameObject explosivePrefab;
    public Transform dropPoint;
    public GameObject explosive;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public GameObject projectile;

    public ParticleSystem explosion;


    void Start()
    {
        
    }

    void Update()
    {
        float speed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.5f;
        }
        if(Input.GetKey(KeyCode.W))
        {
            Vector2 moveDirection = transform.up * speed;
            rb.AddForce(moveDirection);
            isMoving = true;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            Vector2 moveDirection = -transform.up * speed;
            rb.AddForce(moveDirection);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(isMoving)
        {
            if (Input.GetKey(KeyCode.A))
            {
                float rotationAmount = turnSpeed * Time.deltaTime;
                rb.rotation += rotationAmount;
            }
            if (Input.GetKey(KeyCode.D))
            {
                float rotationAmount = turnSpeed * Time.deltaTime;
                rb.rotation -= rotationAmount;
            }
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space) && (explosive == null))
        {
            explosive = Instantiate(explosivePrefab, dropPoint.position, dropPoint.rotation);
        }

        if (Input.GetMouseButtonDown(0) && (projectile == null))
        {
            projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "PlayerExplosive" || collision.gameObject.tag == "EnemyExplosive" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Drone")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Respawn();
    }

    public void Respawn()
    {
        transform.position = new Vector3(0f, 0f, transform.position.z);
        transform.position = new Vector3(0f, 0f, 0f);
    }
}