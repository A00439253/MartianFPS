using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goodie : MonoBehaviour
{

    public float timeToPlayVFX = 2;
    public GameObject goodieObj;
    public GameObject vfxObj;

    public bool bFinalGoodie = false;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            goodieObj.SetActive(false);
            StartCoroutine("DestroyGoodie");
            vfxObj.SetActive(true);
            CustomProperty.customProperties[EnumProperties.IncreaseGoodies].UpdateProperty();
        }
    }


    IEnumerator DestroyGoodie()
    {
        yield return new WaitForSeconds(timeToPlayVFX);

        if (bFinalGoodie)
        {
            CustomProperty.customProperties[EnumProperties.PlayerWins].UpdateProperty();
        }

        DestroyImmediate(gameObject);
    }
}
