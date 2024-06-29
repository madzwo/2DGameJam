using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool upgraded = false;

    public float projectileLife;
    private float timeSinceFire;

    public ParticleSystem explosion;


    void Start()
    {
        if (upgraded)
        {
            speed = 4.0f;
        }
        else
        {
            speed = 2.5f;
        }
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
        string tagName = collision.gameObject.tag;
        if(tagName == "Explosion" || tagName == "BomberRover" || tagName == "SuicideRover" || tagName == "BomberDrone" || tagName == "SuicideDrone" || tagName == "ArtilleryRover")
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
