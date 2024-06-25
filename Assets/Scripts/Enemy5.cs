using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;
    public float turnSpeed;
    private float rotationAmount;    

    public ParticleSystem explosion;

    public GameObject player;

    public GameObject gun;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject bullet;
    public float fireRate;
    private float timeTillFire;
    private Vector3 aimDirection;

    private bool stopped = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Respawn();

        timeTillFire = fireRate;
    }

    void Update()
    {
        if(transform.position.y < 3.0f)
        {
            stopped = true;
            rb.velocity = transform.up * 0.0f;

            if(timeTillFire < 0f)
            {
                bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                timeTillFire = fireRate;
            }
            timeTillFire -= Time.deltaTime;
        }
        else if (!stopped)
        {  
            moveDirection = transform.up * speed;
            rb.velocity = moveDirection;
            rb.rotation += rotationAmount;
        }

        aimDirection = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, aimDirection);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        gun.transform.rotation = Quaternion.Euler(0f, 0f, eulerAngles.z);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "PlayerMissile" || tagName == "Player" || tagName == "BomberRover" || tagName == "SuicideRover" || tagName == "PlayerExplosive" || tagName == "EnemyExplosive")
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
        stopped = false;
        float x = 0f;
        float y = Random.Range(55.0f,65.0f);
        transform.position = new Vector3(x, y, transform.position.z);
        float rot = Random.Range(-3.0f,3.0f);
        float z = 180f + rot;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
    

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
