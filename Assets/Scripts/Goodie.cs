using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodie : MonoBehaviour
{

    public float timeToPlayVFX = 2;
    public GameObject goodieObj;
    public GameObject vfxObj;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            goodieObj.SetActive(false);
            StartCoroutine("DestroyGoodie");
            vfxObj.SetActive(true);
            CustomerProperty.customProperties[EnumProperties.IncreaseGoodies].UpdateProperty();
        }
    }


    IEnumerator DestroyGoodie()
    {
        yield return new WaitForSeconds(timeToPlayVFX);
        DestroyImmediate(gameObject);
    }
}
