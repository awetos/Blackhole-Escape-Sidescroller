using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    bool isPaused;
    GameObject returnButton;
    GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        returnButton = GameObject.Find("ReturnButton");
        returnButton.transform.SetParent(this.transform);
        returnButton.SetActive(false);

        quitButton = GameObject.Find("Quit Game");
        quitButton.transform.SetParent(this.transform);
        quitButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        if (isPaused == false)
        {
            
            Time.timeScale = 0;
            returnButton.SetActive(true);
            quitButton.SetActive(true);
            isPaused = true;
        }
        else
        {
           
            Time.timeScale = 1;
            returnButton.SetActive(false);
            quitButton.SetActive(false);
            isPaused = false;
        }
    }
}
