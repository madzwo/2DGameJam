using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float boostSpeed;
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
    public ParticleSystem smoke;

    public float health;
    private float maxHealth;

    public Image healthBar;

    public float time;
    public TMP_Text timeText;

    int mins;
    int tens;
    int ones;

    public bool gameOver = false;

    private int metal;
    public TMP_Text metalText;


    //upgrades
    private float clickRange;
    private float clickDistance;

    public GameObject roverUpgradeButton;
    public GameObject gunUpgradeButton;
    public GameObject missileUpgradeButton;

    public TMP_Text roverUpgradeText;
    public TMP_Text gunUpgradeText;
    public TMP_Text missileUpgradeText;

    public TMP_Text roverCostText;
    public TMP_Text gunCostText;
    public TMP_Text missileCostText;

    public GameObject roverBoxOne;
    public GameObject roverBoxTwo;
    public GameObject roverBoxThree;

    public GameObject gunBoxOne;
    public GameObject gunBoxTwo;
    public GameObject gunBoxThree;

    public GameObject missileBoxOne;
    public GameObject missileBoxTwo;
    public GameObject missileBoxThree;

    private int roverLevel;
    private int gunLevel;
    private int missileLevel;

    public Bullet bulletScript;
    public Missile missileScript;


    void Start()
    {
        // build version
        // moveSpeed = 15.0f;

        //editor verson
        moveSpeed = 1.0f;

        boostSpeed = moveSpeed * 1.5f;
        bulletTimeTillFire = bulletFireRate;
        missileTimeTillFire = missileFireRate;

        maxHealth = 1.0f;
        health = maxHealth;

        clickRange = .5f;

        bulletScript = bulletPrefab.GetComponent<Bullet>();
        missileScript = missilePrefab.GetComponent<Missile>();

        roverLevel = 0;
        gunLevel = 0;
        missileLevel = 0;

    }

    void Update()
    {
        if(gameOver)
        {
        }
        else
        {
            UI();

            float speed = moveSpeed;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                speed = boostSpeed;
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

        Upgrades();
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string tagName = collision.gameObject.tag;
        if(tagName == "Explosion" || tagName == "PlayerExplosive" || tagName == "SuicideDrone" || tagName == "EnemyBullet")
        {
            health -= 0.2f;
        }
        if(tagName == "EnemyExplosive" || tagName == "BomberRover" || tagName == "SuicideRover")
        {
            health -= 0.3f;
        }
        if(tagName == "Metal")
        {
            Destroy(collision.gameObject);
            metal += 1;
        }
        
        if(health <= 0.1f)
        {
            Explode();
            smoke.Play();
        }

    }

    public void Explode()
    {
        healthBar.fillAmount = health / maxHealth;
        Instantiate(explosion, transform.position, transform.rotation);
        gameOver = true;
    }

    public void Respawn()
    {
        health = maxHealth;
        transform.position = new Vector3(0f, 0f, transform.position.z);
        transform.position = new Vector3(0f, 0f, 0f);
    }

    public void UI()
    {
        healthBar.fillAmount = health / maxHealth;

        time += Time.deltaTime;
        float roundedTime = Mathf.Floor(time);
        int displayTime = (int)roundedTime;
        

        if(displayTime % 60 == 0)
        {
            mins = displayTime / 60;
            tens = 0;
            ones = 0;
        }
        if(displayTime % 10 == 0)
        {
            tens = (displayTime - (mins * 60)) / 10;
        }
        ones = displayTime % 10;

        timeText.text = "" + mins + ":" + tens + "" + ones;

        metalText.text = "" + metal;

    }

    public void Upgrades()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickDistance = Vector2.Distance(mousePosition, roverUpgradeButton.transform.position);
            if (clickDistance <= clickRange)
            {
                if (roverLevel == 0 && metal >= 3)
                {
                    metal -= 3;
                    moveSpeed *= 1.5f;
                    boostSpeed = moveSpeed * 1.5f;
                    roverBoxOne.gameObject.SetActive(true);
                    roverUpgradeText.text = "+Turn Speed";
                    roverCostText.text = "5";
                }
                else if (roverLevel == 1 && metal >= 5)
                {
                    metal -= 5;
                    turnSpeed *= 1.5f;
                    roverBoxTwo.gameObject.SetActive(true);
                    roverUpgradeText.text = "+Boost Speed";
                    roverCostText.text = "10";

                }
                else if (roverLevel == 2 && metal >= 10)
                {
                    metal -= 10;
                    boostSpeed *= 1.5f;
                    roverBoxThree.gameObject.SetActive(true);
                    roverUpgradeText.text = "MAX";
                    roverCostText.text = " ";
                }
                roverLevel++;
            }
            clickDistance = Vector2.Distance(mousePosition, gunUpgradeButton.transform.position);
            if (clickDistance <= clickRange)
            {
                if (gunLevel == 0 && metal >= 3)
                {
                    metal -= 3;
                    roverCostText.text = "5";
                    bulletScript.upgraded = true;
                    gunBoxOne.gameObject.SetActive(true);
                    gunUpgradeText.text = "+Gun Reload";
                }
                else if (gunLevel == 1 && metal >= 5)
                {
                    metal -= 5;
                    roverCostText.text = "10";
                    bulletFireRate *= .5f;
                    gunBoxTwo.gameObject.SetActive(true);
                    gunUpgradeText.text = "++Gun Reload";
                }
                else if (gunLevel == 2 && metal >= 10)
                {
                    metal -= 10;
                    roverCostText.text = " ";
                    bulletFireRate *= .5f;
                    gunBoxThree.gameObject.SetActive(true);
                    gunUpgradeText.text = "MAX";
                }
                gunLevel++;
            }
            clickDistance = Vector2.Distance(mousePosition, missileUpgradeButton.transform.position);
            if (clickDistance <= clickRange)
            {
                if (missileLevel == 0 && metal >= 3)
                {
                    metal -= 3;
                    roverCostText.text = "5";
                    missileScript.upgraded = true;
                    missileBoxOne.gameObject.SetActive(true);
                    missileUpgradeText.text = "+Missile Reload";
                }
                else if (missileLevel == 1&& metal >= 5)
                {
                    metal -= 5;
                    roverCostText.text = "10";
                    missileFireRate *= .5f;
                    missileBoxTwo.gameObject.SetActive(true);
                    missileUpgradeText.text = "++Missile Reload";
                }
                else if (missileLevel == 2 && metal >= 10)
                {
                    metal -= 10;
                    roverCostText.text = " ";
                    missileBoxThree.gameObject.SetActive(true);
                    missileUpgradeText.text = "MAX";
                    missileFireRate *= .5f;
                }
                missileLevel++;
            }
        }
    }
}