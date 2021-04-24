using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnTriggerEnter : MonoBehaviour
{

    public string tagToCompare = "Player";

    public List<GameObject> gameObjects;
    public List<Animator> animators;
    public List<Light> lights;
    public List<AudioSource> audioSources;

    public bool bValue = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCompare))
        {
            enableGameObjects();
            enableAnimators();
            enableLights();
            enableAudioSources();
        }
    }


    void enableGameObjects()
    {
        foreach (var gObj in gameObjects)
        {
            gObj.SetActive(bValue);
        }
    }

    void enableAnimators()
    {
        foreach (var gObj in animators)
        {
            gObj.enabled = (bValue);
        }
    }

    void enableLights()
    {
        foreach (var gObj in lights)
        {
            gObj.enabled = (bValue);
        }
    }

    private void enableAudioSources()
    {
        foreach (var gObj in audioSources)
        {
            gObj.enabled = (bValue);
        }

    }


}
