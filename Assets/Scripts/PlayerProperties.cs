using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{

    private static PlayerProperties instance;

    public static PlayerProperties Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
           // DontDestroyOnLoad(this.gameObject);
        }
    }


    public int health = 100;
    public int bullets = 99;
    public int goodies = 0;
    public bool bHasGameWon = false;
    public bool bGameHasRestarted = false;
    public bool bHasGun = false;
    public PlayerController PlayerControllerInstance;

}
