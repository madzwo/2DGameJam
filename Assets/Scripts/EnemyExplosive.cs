using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosive : MonoBehaviour
{
    public float timeTillExplode;
    public ParticleSystem explosion;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(timeTillExplode <= 0)
        {
            Explode();
        }
        timeTillExplode -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distance);
        if(distance < 2f && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player")
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
