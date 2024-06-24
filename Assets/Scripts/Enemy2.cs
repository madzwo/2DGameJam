using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;

    public float explodeTime;
    private float timeTillExplode;
    public ParticleSystem explosion;

    void Start()
    {
        Respawn();
        timeTillExplode = explodeTime;
    }

    void Update()
    {
        rb.velocity = moveDirection;

        if(transform.position.x < -30.0f || transform.position.x > 30.0f)
        {
            Respawn();
        }

        if(timeTillExplode < 0f)
        {
            Explode();
            timeTillExplode = explodeTime;
        }
        timeTillExplode -= Time.deltaTime;


    }

    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "PlayerExplosive" || collision.gameObject.tag == "EnemyExplosive" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
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
        float rand = Random.Range(0.0f,2.0f);
        if(rand > 1.0f)
        {
            float x = Random.Range(-25.0f,-30.0f);
            float y = Random.Range(-1.5f,1.5f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-10.0f,10.0f);
            float z = 90 + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            moveDirection = -transform.up * speed;
        }
        else
        {
            float x = Random.Range(25.0f,30.0f);
            float y = Random.Range(-1.5f,1.5f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-10.0f,10.0f);
            float z = 90 + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            moveDirection = transform.up * speed;
        }
    }


}
