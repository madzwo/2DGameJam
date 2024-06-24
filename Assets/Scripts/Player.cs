using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float turnSpeed;
    private bool isMoving;

    public ParticleSystem smallJet1;
    public ParticleSystem smallJet2;
    public ParticleSystem bigJet;

    public GameObject gun;
    public GameObject missileGun;
    private Vector3 aimDirection;
    
    public GameObject explosivePrefab;
    public Transform dropPoint;
    public GameObject explosive;

    public GameObject bulletPrefab;
    public Transform bulletFirePoint;
    public GameObject bullet;
    public float bulletFireRate;
    private float bulletTimeTillFire;

    public GameObject missilePrefab;
    public Transform missileFirePoint;
    public GameObject missile;
    public float missileFireRate;
    private float missileTimeTillFire;

    public ParticleSystem explosion;


    void Start()
    {
        bulletTimeTillFire = bulletFireRate;
        missileTimeTillFire = missileFireRate;

    }

    void Update()
    {
        float speed = moveSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.5f;
            bigJet.Play();
        } 
        else
        {
            bigJet.Stop();
        }
        if(Input.GetKey(KeyCode.W))
        {
            Vector2 moveDirection = transform.up * speed;
            rb.AddForce(moveDirection);
            isMoving = true;
            smallJet1.Play();
            smallJet2.Play();
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
            smallJet1.Stop();
            smallJet2.Stop();
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
        missileGun.transform.rotation = Quaternion.Euler(0f, 0f, eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space) && (explosive == null))
        {
            explosive = Instantiate(explosivePrefab, dropPoint.position, dropPoint.rotation);
        }

        if (Input.GetMouseButtonDown(0) && (bulletTimeTillFire < 0f))
        {
            bullet = Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
            bulletTimeTillFire = bulletFireRate;
        }
        bulletTimeTillFire -= Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && (missileTimeTillFire < 0f))
        {
            missile = Instantiate(missilePrefab, missileFirePoint.position, missileFirePoint.rotation);
            missileTimeTillFire = missileFireRate;
        }
        missileTimeTillFire -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "PlayerExplosive" || collision.gameObject.tag == "EnemyExplosive" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Drone2")
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