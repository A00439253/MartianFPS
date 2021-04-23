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

    public bool bIsGameOver = false;
    public bool bIsPaused = false;

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            TogglePause();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        //PlayerProperties.Instance.bGameHasRestarted = true;
        if (PlayerProperties.Instance != null && PlayerProperties.Instance.gameObject != null)
        {
            DestroyImmediate(PlayerProperties.Instance.gameObject);
        }
        LevelManager.Instance.GameRestart();
        Physics.autoSimulation = true;
    }

    public void TogglePause()
    {

        if (UI_Manager.Instance.CurrentUIState() == UI_Manager.UI_States.GameStart) return;

        bIsPaused = !bIsPaused;
        Cursor.lockState = bIsPaused ? CursorLockMode.None : CursorLockMode.Locked;

        AudioManager.Instance.PlaySfx(bIsPaused ? AudioManager.SFX_Enums.Pause : AudioManager.SFX_Enums.Resume);
        UI_Manager.Instance.UpdateUI_State(bIsPaused ? UI_Manager.UI_States.Settings : UI_Manager.UI_States.Gameplay);

        Time.timeScale = bIsPaused ? 0 : 1;
    }

}
