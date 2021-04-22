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

    List<string> currentlyLoadedScenes;

    public Transform toBeDeletedContainer;

    public string exitScene;
    public string startScene;


    public void StartGame()
    {
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.None);
        currentlyLoadedScenes = new List<string>();

        foreach (string levelName in defaultScenesToLoad)
        {
            LoadScene(levelName);
        }

       // StartCoroutine("DisableExtraGameObjects");
            
    }

    public void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            currentlyLoadedScenes.Add(sceneName);
        }
    }

    public void UnLoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            currentlyLoadedScenes.Remove(sceneName);
        }
    }

    public void GameExitScene()
    {
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.None);
        UnloadAllNewlyLoadedScenes();
        LoadScene(exitScene);
    }


    public void GameRestart()
    {
        if (GameManager.Instance.bIsPaused) GameManager.Instance.TogglePause();
        Cursor.lockState = CursorLockMode.None;

        StartCoroutine("ReEnableExtraGameObjects");
        DestroyContainerContents();
        UnloadAllNewlyLoadedScenes();
        UI_Manager.Instance.UpdateUI_State(UI_Manager.UI_States.GameStart);
    }

    void DestroyContainerContents()
    {
        foreach (Transform child in toBeDeletedContainer)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void UnloadAllNewlyLoadedScenes()
    {
        List<string> cacheSceneNames = new List<string>(currentlyLoadedScenes);
        foreach (var sceneName in cacheSceneNames)
        {
            UnLoadScene(sceneName);
        }
        currentlyLoadedScenes.Clear();
        cacheSceneNames.Clear();
    }



    public void DisableExtraGameObjects()
    {
        foreach (var gObj in gameObjectsToDeleteOnStartGame)
        {
            gObj.SetActive(false);
            //DestroyImmediate(gObj);
        }
        //gameObjectsToDeleteOnStartGame.Clear();

    }

    IEnumerator ReEnableExtraGameObjects()
    {
        yield return new WaitForSeconds(0);


        foreach (var gObj in gameObjectsToDeleteOnStartGame)
        {
            gObj.SetActive(true);
            //DestroyImmediate(gObj);
        }
        //gameObjectsToDeleteOnStartGame.Clear();

    }

}
