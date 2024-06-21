using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionTime;

    void Start()
    {
        
    }

    void Update()
    {
        if(explosionTime < 0f)
        {
            Destroy(gameObject);
        }
        explosionTime -= Time.deltaTime;
    }
}
