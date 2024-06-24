using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExplosive : MonoBehaviour
{
    public float timeTillExplode;
    public ParticleSystem explosion;

    public GameObject player;

    public TMP_Text timeText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Debug.Log(player.transform.rotation.z);
        if(player.transform.rotation.z > .9f || player.transform.rotation.z < -.9f)
        {
            Quaternion currentRotation = transform.rotation;
            Quaternion newRotation = currentRotation * Quaternion.Euler(0, 0, 180);
            transform.rotation = newRotation;
        }
    }

    void Update()
    {
        if(timeTillExplode <= 0)
        {
            Explode();
        }
        timeTillExplode -= Time.deltaTime;

        float roundedTime = Mathf.Ceil(timeTillExplode);
        int displayTime = (int)roundedTime;

        timeText.text = ":0" + displayTime;


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
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
