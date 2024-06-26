using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;
    public float turnSpeed;
    private float rotationAmount;

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

        rb.rotation += rotationAmount;

        if (timeTillDrop < 0 && transform.position.x > -7f && transform.position.x < 7f)
        {
            explosive = Instantiate(explosivePrefab, dropPoint.position, dropPoint.rotation);
            timeTillDrop = dropRate;
        }
        timeTillDrop -= Time.deltaTime;

        if(transform.position.x < -maxStartDistance || transform.position.x > maxStartDistance || transform.position.y < -8 || transform.position.y > 8)
        {
            Respawn();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "Explosion" || tagName == "PlayerMissile" || tagName == "Player" || tagName == "EnemyExplosive" || tagName == "SuicideRover" || tagName == "ArtilleryRover")
        {
            Debug.Log(collision.gameObject.tag);
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(metal, transform.position, transform.rotation);
        Respawn();
    }

    public void Respawn()
    {
        float rand = Random.Range(0.0f,2.0f);
        if(rand > 1.0f)
        {
            float x = Random.Range(-minStartDistance, -maxStartDistance);
            float y = Random.Range(-3.0f,3.0f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-20.0f,20.0f);
            float z = -90.0f + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            dropPoint.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }
        else
        {
            float x = Random.Range(minStartDistance, maxStartDistance);
            float y = Random.Range(-3.0f,3.0f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-20.0f,20.0f);
            float z = 90.0f + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            dropPoint.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        }

        float turn = Random.Range(0.0f,2.0f);
        if(turn > 1.0f)
        {
            rotationAmount = turnSpeed * Time.deltaTime;
        }
        else
        {
            rotationAmount = -turnSpeed * Time.deltaTime;
        }
    }
}
