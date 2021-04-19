using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public float destroyAfterDistance = -15f;
    public bool bIsGameOver = false;
    public bool bIsPaused = false;

    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void TogglePause()
    {
        bIsPaused = !bIsPaused;
        Time.timeScale = bIsPaused ? 0 : 1;
    }

}
