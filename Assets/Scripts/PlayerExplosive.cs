using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosive : MonoBehaviour
{
    public float timeTillExplode;
    public ParticleSystem explosion;

    void Start()
    {
        
    }

    void Update()
    {
        if(timeTillExplode <= 0)
        {
            Explode();
        }
        timeTillExplode -= Time.deltaTime;
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
