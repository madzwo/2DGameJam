using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScreen : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject playButton;
    public GameObject controlsButton;
    public GameObject quitButton;

    public GameObject game;

    public GameObject controls;
    public GameObject backButton;

    private float clickRange;
    private float clickDistance;

    public GameObject player;
    public Player playerScript;

    public GameObject endButtons;
    public GameObject homeButton;



    void Start()
    {
        clickRange = .5f;
        playerScript = player.GetComponent<Player>();
        startCanvas.SetActive(true);
        controls.SetActive(false);
        game.SetActive(false);
    }

    void Update()
    {
        if(startCanvas.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickDistance = Vector2.Distance(mousePosition, playButton.transform.position);
                if (clickDistance <= clickRange)
                {
                    startCanvas.gameObject.SetActive(false);
                    game.gameObject.SetActive(true);
                }
                clickDistance = Vector2.Distance(mousePosition, controlsButton.transform.position);
                if (clickDistance <= clickRange)
                {
                    startCanvas.gameObject.SetActive(false);
                    controls.gameObject.SetActive(true);
                }
                clickDistance = Vector2.Distance(mousePosition, quitButton.transform.position);
                if (clickDistance <= clickRange)
                {
                    Debug.Log("Quit");
                }
            }
        }
        else if(controls.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {   
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                clickDistance = Vector2.Distance(mousePosition, backButton.transform.position);
                if (clickDistance <= clickRange)
                {
                    startCanvas.gameObject.SetActive(true);
                    controls.gameObject.SetActive(false);
                }
            }
        }
        else if(game.activeSelf)
        {
            if(playerScript.gameOver)
            {
                endButtons.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                { 
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    clickDistance = Vector2.Distance(mousePosition, homeButton.transform.position);
                    if (clickDistance <= clickRange)
                    {
                        SceneManager.LoadScene(0);
                    }
                }

            }
        }
    }
}
