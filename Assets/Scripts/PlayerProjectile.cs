using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public float projectileLife;
    private float timeSinceFire;

    public ParticleSystem explosion;


    void Start()
    {
        
    }

    void Update()
    {
        Vector2 moveDirection = transform.up * speed;
        rb.velocity = moveDirection;

        timeSinceFire += Time.deltaTime;
        if(timeSinceFire > projectileLife)
        {
            Explode();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Drone" || collision.gameObject.tag == "Drone2")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
