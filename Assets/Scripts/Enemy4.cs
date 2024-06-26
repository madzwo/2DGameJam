using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    public float speed;

    public ParticleSystem explosion;

    public GameObject player;


    void Start()
    {
        // player = GameObject.FindGameObjectWithTag("Player");
        Respawn();
    }

    void Update()
    {   
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

    }

    public void Respawn()
    {
        float rand = Random.Range(0.0f,2.0f);
        if(rand > 1.0f)
        {
            float x = Random.Range(-80.0f,-85.0f);
            float y = Random.Range(-1.0f,1.0f);
            transform.position = new Vector3(x, y, transform.position.z);
        }
        else
        {
            float x = Random.Range(80.0f,85.0f);
            float y = Random.Range(-1.0f,1.0f);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "PlayerBullet" || tagName == "PlayerMissile" || tagName == "EnemyBullet" || tagName == "Player" || tagName == "SuicideDrone")
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Respawn();
    }
}
