using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;

    public static LevelManager Instance { get { return instance; } }


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


    public List<string> defaultScenesToLoad;
    public List<GameObject> gameObjectsToDeleteOnStartGame;


    public void StartGame()
    {
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.None);


        foreach (string levelName in defaultScenesToLoad)
        {
            LoadScene(levelName);
        }

        foreach (var gObj in gameObjectsToDeleteOnStartGame)
        {
            DestroyImmediate(gObj);
        }

        gameObjectsToDeleteOnStartGame.Clear();

        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.Gameplay);
    }

    public void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void UnLoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }
    }

}
