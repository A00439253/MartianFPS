using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    private static UI_Manager instance;

    public static UI_Manager Instance { get { return instance; } }

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


    public enum UI_States
    {
        None = 0,
        GameStart,
        Gameplay,
        Settings,
        GameExit,

    }


    [System.Serializable]
    public class UI_Mapping
    {
        public UI_States index;
        public GameObject gameObject;
    }

    public UI_Mapping[] uiMap;

    UI_States currentUI_State = UI_States.GameStart;
    //UI_States nextUI_State = UI_States.GameStart;


    public UI_States CurrentUIState()
    {
        return currentUI_State;
    }

    private void Start()
    {
        UpdateUI_State(UI_States.GameStart);
    }

    public void UpdateUI_State(UI_States uI_State)
    {

        foreach (var pair in uiMap)
        {
            if (pair.index == currentUI_State) pair.gameObject.SetActive(false);
            if (pair.index == uI_State) pair.gameObject.SetActive(true);
        }

        currentUI_State = uI_State;
    }

}
