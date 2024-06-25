using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyExplosive : MonoBehaviour
{
    public float timeTillExplode;
    public ParticleSystem explosion;

    public GameObject player;

    public TMP_Text timeText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if(distance < 2f && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(gameObject);
        }

        if(timeTillExplode <= 0)
        {
            Explode();
        }
        timeTillExplode -= Time.deltaTime;

        float roundedTime = Mathf.Ceil(timeTillExplode);
        int displayTime = (int)roundedTime;

        if(displayTime == 10)
        {
            timeText.text = ":10";

        }
        else
        {
            timeText.text = ":0" + displayTime;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "SuicideRover" || tagName == "ArtilleryRover" || tagName == "Player" || tagName == "Explosion" || tagName == "PlayerBullet" || tagName == "PlayerMissile" || tagName == "EnemyBullet")
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
