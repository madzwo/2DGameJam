using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy2 : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;

    public float explodeTime;
    private float timeTillExplode;
    public ParticleSystem explosion;

    public float maxStartDistance;
    public float minStartDistance;

    public TMP_Text timeText;


    void Start()
    {
        Respawn();
        timeTillExplode = explodeTime;
    }

    void Update()
    {
        rb.velocity = moveDirection;

        if(transform.position.x < -maxStartDistance || transform.position.x > maxStartDistance)
        {
            Respawn();
        }

        if(timeTillExplode < 0f)
        {
            Explode();
            timeTillExplode = explodeTime;
        }
        timeTillExplode -= Time.deltaTime;

        float roundedTime = Mathf.Ceil(timeTillExplode);
        int displayTime = (int)roundedTime;
        timeText.text = ":0" + displayTime;

    }
    

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "Explosion" || tagName == "PlayerBullet" || tagName == "PlayerMissile" || tagName == "PlayerExplosive" || tagName == "EnemyExplosive" || tagName == "Player" || tagName == "BomberRover" || tagName == "ArtilleryRover")
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
            float x = Random.Range(-minStartDistance,-maxStartDistance);
            float y = Random.Range(-1.5f,1.5f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-10.0f,10.0f);
            float z = 90 + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            moveDirection = -transform.up * speed;
        }
        else
        {
            float x = Random.Range(minStartDistance,maxStartDistance);
            float y = Random.Range(-1.5f,1.5f);
            transform.position = new Vector3(x, y, transform.position.z);
            float rot = Random.Range(-10.0f,10.0f);
            float z = 90 + rot;
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
            moveDirection = transform.up * speed;
        }
    }


}
