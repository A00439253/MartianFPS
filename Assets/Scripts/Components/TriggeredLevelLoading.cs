using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredLevelLoading : MonoBehaviour
{

    public string sceneToLoad;
    public string sceneToUnLoad;
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            if (sceneToLoad != "")
            {
                LevelManager.Instance.LoadScene(sceneToLoad);
            }
            if (sceneToUnLoad != "")
            {
                StartCoroutine("UnloadScene");
            }
        }
    }


    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(0.10f);
        LevelManager.Instance.UnLoadScene(sceneToUnLoad);
    }
}
