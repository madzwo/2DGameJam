using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start()
    {
        clickRange = .5f;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(startCanvas.activeSelf)
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
            else if(controls.activeSelf)
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
    }
}
