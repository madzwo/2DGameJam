using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;

    public ParticleSystem explosion;

    public GameObject explosivePrefab;
    public Transform dropPoint;
    public float dropRate;
    private float timeTillDrop;
    public GameObject explosive;

    public GameObject metal;

    public float maxStartDistance;
    public float minStartDistance;


    void Start()
    {
        Respawn();
        timeTillDrop = dropRate;
    }

    void Update()
    {
        moveDirection = transform.up * speed;
        rb.velocity = moveDirection;

        if(transform.position.y < -maxStartDistance || transform.position.y > maxStartDistance)
        {
            Respawn();
        }

        if (timeTillDrop < 0)
        {
            explosive = Instantiate(explosivePrefab, dropPoint.position, dropPoint.rotation);
            timeTillDrop = dropRate;
        }
        timeTillDrop -= Time.deltaTime;
    }

    public void Respawn()
    {
        float rand = Random.Range(0.0f,2.0f);
        if(rand > 1.0f)
        {
            float x = Random.Range(-4.0f,4.0f);
            float y = Random.Range(minStartDistance,maxStartDistance);
            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f);
            dropPoint.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        else
        {
            float x = Random.Range(-4.0f,4.0f);
            float y = Random.Range(-minStartDistance,-maxStartDistance);
            transform.position = new Vector3(x, y, transform.position.z);
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f);
            dropPoint.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "PlayerBullet" || tagName == "PlayerMissile" || tagName == "EnemyBullet" || tagName == "BomberDrone")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(metal, transform.position, transform.rotation);
        Respawn();
    }
}
